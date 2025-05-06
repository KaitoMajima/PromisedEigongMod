using System.Collections.Generic;
using NineSolsAPI.Preload;
using PromisedEigong.Core;
using PromisedEigong.SpeedChangers;
using PromisedEigong.WeightChanges;
using UnityEngine;

namespace PromisedEigong;

using BepInEx;
using HarmonyLib;
using NineSolsAPI;
using UnityEngine.SceneManagement;
using static HarmonyLib.AccessTools;
using static PromisedEigongModGlobalSettings.EigongRefs;
using static PromisedEigongModGlobalSettings.EigongHealth;
using static PromisedEigongModGlobalSettings.EigongColors;
using static PromisedEigongModGlobalSettings.EigongLogging;

[BepInDependency(NineSolsAPICore.PluginGUID)]
[BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
public class PromisedEigongMain : BaseUnityPlugin
{
    MonsterBase EigongBase => SingletonBehaviour<MonsterManager>.Instance.ClosetMonster;
    bool IsInBossMemoryMode => ApplicationCore.IsInBossMemoryMode;

    Harmony harmony = null!;
    
    FieldRef<MonsterStat, float> HealthFieldRef => FieldRefAccess<MonsterStat, float>("BaseHealthValue");
    
    [Preload("結局演出_大爆炸 P2", "GameLevel/Room/3DBG Master 背景/BIGBAD")]
    GameObject? preloadedObject;
    
    GameObject instantiatedObject;
    
    BossGeneralState currentBossState;
    bool hasInitialized;
    bool hasFinishedInitializing;
    bool hasAlreadyPreloaded;
    KCore kCore;

    void Awake () 
    {
        KLog.Init(Logger);
        RCGLifeCycle.DontDestroyForever(gameObject);
        harmony = Harmony.CreateAndPatchAll(typeof(PromisedEigongMain).Assembly);
        KLog.Info($"{MyPluginInfo.PLUGIN_NAME}: Loaded.");
        KLog.Info($"Version: {MyPluginInfo.PLUGIN_VERSION}");
        ToastManager.Toast($"TOAST! {MyPluginInfo.PLUGIN_NAME}: Loaded.");
        kCore = new KCore();
        kCore.Setup(this);
        kCore.MainAwake();
        kCore.Preloader.AddPreloadClass(this);
        AddListeners();
    }

    void Update ()
    {
        Scene activeScene = SceneManager.GetActiveScene();

        if (activeScene.name is not (SCENE_NORMAL_ENDING_EIGONG or SCENE_TRUE_ENDING_EIGONG))
            return;
        
        if (preloadedObject != null && instantiatedObject == null)
        {
            Transform bgMasterTransform = GameObject.Find("GameLevel/Room/3DBG Master 背景").transform;
            instantiatedObject = Instantiate(preloadedObject, bgMasterTransform);
            instantiatedObject.SetActive(true);
        }
        
        StartInitialization();
        ChangeEigongHealth();
        ChangeAttackWeights();
        ChangeAttackSpeeds();
        ChangeEigongColors();
        FinishInitialization();
        LogStates();
    }

    void StartInitialization ()
    {
        hasInitialized = EigongBase != null;
        if (!hasInitialized)
            hasFinishedInitializing = false;
    }

    void FinishInitialization ()
    {
        if (hasInitialized)
            hasFinishedInitializing = true;
    }
    
    void AddListeners ()
    {
        Patches.OnTitleScreenMenuLoaded += HandleTitleScreenMenuLoaded;
        Patches.OnFoundTaiDanger += HandleTaiDangerFound;
        Patches.OnFoundJieChuanFireParticles += HandleJieChuanFireParticlesFound;
        Patches.OnFoundJieChuanFireImage += HandleJieChuanFireImageFound;
        Patches.OnFoundFooExplosionSprite += HandleFooExplosionSpriteFound;
        Patches.OnFoundFooSprite += HandleFooSpriteFound;
        Patches.OnFoundCrimsonGeyserSprite += HandleCrimsonGeyserSpriteFound;
        Patches.OnFoundCrimsonPillarSprite += HandleCrimsonPillarSpriteFound;
    }

    void ChangeEigongHealth ()
    {
        if (!hasInitialized) 
            return;

        if (hasFinishedInitializing)
            return;
        
        ToastManager.Toast("Initializing...");
        
        HealthFieldRef.Invoke(EigongBase.monsterStat) = EIGONG_PHASE_1_HEALTH_VALUE * DEBUG_PHASE_1_MULTIPLIER;
        
        if (IsInBossMemoryMode)
            EigongBase.postureSystem.CurrentHealthValue = EIGONG_PHASE_1_HEALTH_VALUE * EigongBase.monsterStat.BossMemoryHealthScale * DEBUG_PHASE_1_MULTIPLIER;
        else
            EigongBase.postureSystem.CurrentHealthValue = EIGONG_PHASE_1_HEALTH_VALUE * DEBUG_PHASE_1_MULTIPLIER;
        
        EigongBase.monsterStat.Phase2HealthRatio = (float)EIGONG_PHASE_2_HEALTH_VALUE / EIGONG_PHASE_1_HEALTH_VALUE * DEBUG_PHASE_2_MULTIPLIER;
        EigongBase.monsterStat.Phase3HealthRatio = (float)EIGONG_PHASE_3_HEALTH_VALUE / EIGONG_PHASE_1_HEALTH_VALUE * DEBUG_PHASE_3_MULTIPLIER;
    }

    void ChangeAttackWeights ()
    {
        if (!hasInitialized)
            return;
        
        if (hasFinishedInitializing)
            return;
        
        var weightChangers = new List<BaseWeightChanger>
        {
            new _1SlowStarterWeightChanger(),
            new _3ThrustDelayWeightChanger(),
            new _5TeleportToBackWeightChanger(),
            new _7TeleportForwardWeightChanger(),
            new _8LongChargeWeightChanger(),
            new _9StarterWeightChanger(),
            new _11ChargeWaveWeightChanger(),
            new _12SlashUpCrimsonWeightChanger(),
            new _13TriplePokeWeightChanger(),
            new _15TurnAroundWeightChanger(),
            new _17CrimsonSlamWeightChanger(),
            new _X1PostureBreakWeightChanger(),
            new _X3AttackParryingWeightChanger()
        };
        foreach (var weightChanger in weightChangers)
            weightChanger.ChangeAttackWeight();
    }
    
    void ChangeAttackSpeeds ()
    {
        if (!hasInitialized)
            return;
        
        if (hasFinishedInitializing)
            return;

        BaseSpeedChanger[] speedChangers = [
            new _1SlowStarterSpeedChanger(), 
            new _2TeleportToTopSpeedChanger(),
            new _3ThrustDelaySpeedChanger(),
            new _4SlashUpSpeedChanger(),
            new _5TeleportToBackSpeedChanger(),
            new _6DoubleAttackSpeedChanger(),
            new _7TeleportForwardSpeedChanger(),
            new _8LongChargeSpeedChanger(),
            new _9StarterSpeedChanger(),
            new _10FooSpeedChanger(),
            new _11ChargeWaveSpeedChanger(),
            new _12SlashUpCrimsonSpeedChanger(),
            new _13TriplePokeSpeedChanger(),
            new _14CrimsonBallSpeedChanger(),
            new _15TurnAroundSpeedChanger(),
            new _16QuickFooSpeedChanger(),
            new _17CrimsonSlamSpeedChanger(),
            new _18TeleportToBackComboSpeedChanger(),
            new _X2JumpBackSpeedChanger()
        ];
        
        BossGeneralState[] allBossStates = FindObjectsOfType<BossGeneralState>();
        
        foreach (var bossState in allBossStates)
        {
            bool foundAlteredSpeed = false;
            bossState.OverideAnimationSpeed = true;

            foreach (var speedChanger in speedChangers)
            {
                if (!speedChanger.IsSpecifiedAttack(bossState.name)) 
                    continue;
                
                var newSpeed = speedChanger.GetSpeed(bossState);
                bossState.AnimationSpeed = newSpeed;
                foundAlteredSpeed = true;
                break;
            }
            
            if (!foundAlteredSpeed)
                bossState.AnimationSpeed = 1;
        }
    }
    
    void ChangeEigongColors ()
    {
        if (!hasInitialized)
            return;
        
        if (hasFinishedInitializing)
            return;
        
        var eigongBody = GameObject.Find(EIGONG_CHARACTER_BODY);
        var burningFx = eigongBody.AddComponent<_2dxFX_BurningFX>();
        burningFx.Colors = EIGONG_BURNING_FX_STRENGTH;
        
        var eigongBodyShadow = GameObject.Find(EIGONG_CHARACTER_BODY_SHADOW);
        var burningShadowFx = eigongBodyShadow.AddComponent<_2dxFX_BurningFX>();
        burningShadowFx.Colors = EIGONG_BURNING_FX_STRENGTH;

        var eigongSword = GameObject.Find(EIGONG_CHARACTER_SWORD);
        var swordRGBfx = eigongSword.AddComponent<_2dxFX_ColorChange>();
        swordRGBfx._HueShift = EIGONG_CHARACTER_SWORD_EFFECT_SHIFT;
        swordRGBfx._ValueBrightness = EIGONG_CHARACTER_SWORD_VALUE_BRIGHTNESS;
        swordRGBfx._Saturation = EIGONG_SWORD_SATURATION;

        var eigongFoo = GameObject.Find(EIGONG_CHARACTER_FOO);
        var fooRGBfx = eigongFoo.AddComponent<_2dxFX_ColorChange>();
        fooRGBfx._HueShift = EIGONG_CHARACTER_SWORD_EFFECT_SHIFT;
        fooRGBfx._ValueBrightness = EIGONG_CHARACTER_SWORD_VALUE_BRIGHTNESS;
        
        var eigongSwordEffect = GameObject.Find(EIGONG_CHARACTER_SWORD_EFFECT);
        var swordEffectShiftFx = eigongSwordEffect.AddComponent<_2dxFX_ColorChange>();
        swordEffectShiftFx._HueShift = EIGONG_CHARACTER_SWORD_EFFECT_SHIFT;
        swordEffectShiftFx._ValueBrightness = EIGONG_CHARACTER_SWORD_VALUE_BRIGHTNESS;
        
        var eigongCrimsonSlamEffect = GameObject.Find(EIGONG_CHARACTER_CRIMSON_SLAM_EFFECT);
        var crimsonSlamEffectShiftFx = eigongCrimsonSlamEffect.AddComponent<_2dxFX_ColorChange>();
        crimsonSlamEffectShiftFx._HueShift = EIGONG_CHARACTER_SWORD_EFFECT_SHIFT;
        crimsonSlamEffectShiftFx._ValueBrightness = EIGONG_CHARACTER_SWORD_VALUE_BRIGHTNESS;
        
        var eigongCrimsonGeyserEffect = GameObject.Find(EIGONG_CHARACTER_CRIMSON_GEYSER_EFFECT);
        var crimsonGeyserEffectShiftFx = eigongCrimsonGeyserEffect.AddComponent<_2dxFX_ColorChange>();
        crimsonGeyserEffectShiftFx._HueShift = EIGONG_CHARACTER_SWORD_EFFECT_SHIFT;
        crimsonGeyserEffectShiftFx._ValueBrightness = EIGONG_CHARACTER_SWORD_VALUE_BRIGHTNESS;

        var eigongFireEffect = GameObject.Find(FIRE_TRAIL_PATH);
        var fireEffectShiftFx = eigongFireEffect.AddComponent<_2dxFX_ColorChange>();
        fireEffectShiftFx._HueShift = EIGONG_CHARACTER_SWORD_EFFECT_SHIFT;
        fireEffectShiftFx._ValueBrightness = EIGONG_CHARACTER_SWORD_VALUE_BRIGHTNESS;
        
        var eigongFooEffect1 = GameObject.Find(EIGONG_CHARACTER_FOO_EFFECT_1);
        var fooEffect1Main = eigongFooEffect1.GetComponent<ParticleSystem>().main;
        fooEffect1Main.startColor = FIRE_COLOR;
        var colorOverLifetime1 = eigongFooEffect1.GetComponent<ParticleSystem>().colorOverLifetime;
        colorOverLifetime1.enabled = false;
        
        var eigongFooEffect2 = GameObject.Find(EIGONG_CHARACTER_FOO_EFFECT_2);
        var fooEffect2Main = eigongFooEffect2.GetComponent<ParticleSystem>().main;
        var colorOverLifetime2 = eigongFooEffect2.GetComponent<ParticleSystem>().colorOverLifetime;
        colorOverLifetime2.enabled = false;
        fooEffect2Main.startColor = FIRE_COLOR;
        
        var chargeWaveEffect = GameObject.Find(EIGONG_CHARGE_WAVE_CRIMSON);
        var chargeWaveEffectShiftFx = chargeWaveEffect.AddComponent<_2dxFX_ColorChange>();
        chargeWaveEffectShiftFx._HueShift = EIGONG_CHARACTER_SWORD_EFFECT_SHIFT;
        chargeWaveEffectShiftFx._ValueBrightness = EIGONG_CHARACTER_SWORD_VALUE_BRIGHTNESS;
        
        var chargeWaveGlowEffect = GameObject.Find(EIGONG_CHARGE_WAVE_CRIMSON_GLOW);
        var chargeWaveGlowEffectShiftFx = chargeWaveGlowEffect.AddComponent<_2dxFX_ColorChange>();
        chargeWaveGlowEffectShiftFx._HueShift = EIGONG_CHARACTER_SWORD_EFFECT_SHIFT;
        chargeWaveGlowEffectShiftFx._ValueBrightness = EIGONG_CHARACTER_SWORD_VALUE_BRIGHTNESS;
        
        var chargeWave2Effect = GameObject.Find(EIGONG_CHARGE_WAVE_CRIMSON_2);
        var chargeWave2EffectShiftFx = chargeWave2Effect.AddComponent<_2dxFX_ColorChange>();
        chargeWave2EffectShiftFx._HueShift = EIGONG_CHARACTER_SWORD_EFFECT_SHIFT;
        chargeWave2EffectShiftFx._ValueBrightness = EIGONG_CHARACTER_SWORD_VALUE_BRIGHTNESS;
        
        var chargeWaveGlow2Effect = GameObject.Find(EIGONG_CHARGE_WAVE_CRIMSON_2_GLOW);
        var chargeWaveGlow2EffectShiftFx = chargeWaveGlow2Effect.AddComponent<_2dxFX_ColorChange>();
        chargeWaveGlow2EffectShiftFx._HueShift = EIGONG_CHARACTER_SWORD_EFFECT_SHIFT;
        chargeWaveGlow2EffectShiftFx._ValueBrightness = EIGONG_CHARACTER_SWORD_VALUE_BRIGHTNESS;
    }
    
    void HandleTitleScreenMenuLoaded ()
    {
        if (hasAlreadyPreloaded)
            return;
        
        kCore.StartPreloading();
        hasAlreadyPreloaded = true;
    }
    
    void HandleTaiDangerFound (SpriteRenderer sprite)
    {
        var taiDangerShiftFx = sprite.gameObject.AddComponent<_2dxFX_AL_ColorChange>();
        taiDangerShiftFx._HueShift = EIGONG_CHARACTER_SWORD_EFFECT_SHIFT;
        taiDangerShiftFx._ValueBrightness = EIGONG_TAI_DANGER_VALUE_BRIGHTNESS;
    }
    
    void HandleJieChuanFireParticlesFound (ParticleSystem particleSystem)
    {
        var pfxMain = particleSystem.main;
        pfxMain.startColor = FIRE_COLOR;
    }
    
    void HandleJieChuanFireImageFound (SpriteRenderer sprite)
    {
        var jieChuanFire = sprite.gameObject.AddComponent<_2dxFX_ColorRGB>();
        jieChuanFire._ColorR = FIRE_LIGHT_COLOR.r;
        jieChuanFire._ColorG = FIRE_LIGHT_COLOR.g;
        jieChuanFire._ColorB = FIRE_LIGHT_COLOR.b;
    }

    void HandleFooExplosionSpriteFound (SpriteRenderer sprite)
    {
        var fooExplosionShiftFx = sprite.gameObject.AddComponent<_2dxFX_AL_ColorChange>();
        fooExplosionShiftFx._HueShift = EIGONG_CHARACTER_SWORD_EFFECT_SHIFT;
        fooExplosionShiftFx._ValueBrightness = EIGONG_TAI_DANGER_VALUE_BRIGHTNESS;
    }
    
    void HandleFooSpriteFound (SpriteRenderer sprite)
    {
        var fooFx = sprite.gameObject.AddComponent<_2dxFX_AL_ColorChange>();
        fooFx._HueShift = EIGONG_CHARACTER_SWORD_EFFECT_SHIFT;
        fooFx._ValueBrightness = EIGONG_TAI_DANGER_VALUE_BRIGHTNESS;
    }
    
    void HandleCrimsonGeyserSpriteFound (SpriteRenderer sprite)
    {
        var crimsonGeyserFx = sprite.gameObject.AddComponent<_2dxFX_AL_ColorChange>();
        crimsonGeyserFx._HueShift = EIGONG_CHARACTER_SWORD_EFFECT_SHIFT;
        crimsonGeyserFx._ValueBrightness = EIGONG_TAI_DANGER_VALUE_BRIGHTNESS;
    }
    
    void HandleCrimsonPillarSpriteFound (SpriteRenderer sprite)
    {
        var crimsonPillarFx = sprite.gameObject.AddComponent<_2dxFX_AL_ColorChange>();
        crimsonPillarFx._HueShift = EIGONG_CHARACTER_SWORD_EFFECT_SHIFT;
        crimsonPillarFx._ValueBrightness = EIGONG_TAI_DANGER_VALUE_BRIGHTNESS;
    }
    
    void LogStates ()
    {
        if (!EIGONG_STATE_LOG)
            return;
        
        if (currentBossState == (BossGeneralState)EigongBase.currentMonsterState)
            return;
        currentBossState = (BossGeneralState)EigongBase.currentMonsterState;
        ToastManager.Toast($"Next state: [{currentBossState.name}]");
    }

    void OnDestroy () 
    {
        harmony.UnpatchSelf();
    }
}