using System.Collections;
using BepInEx.Configuration;
using NineSolsAPI.Preload;
using PromisedEigong.AppearanceChangers;
using PromisedEigong.Core;
using PromisedEigong.ModSystem;
using PromisedEigong.PoolObjectWrapper;
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
using static PromisedEigongModGlobalSettings.EigongOST;
using static PromisedEigongModGlobalSettings.EigongPreloadRefs;

[BepInDependency(NineSolsAPICore.PluginGUID)]
[BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
public class PromisedEigongMain : BaseUnityPlugin
{
    MonsterBase EigongBase => SingletonBehaviour<MonsterManager>.Instance.ClosetMonster;
    bool IsInBossMemoryMode => ApplicationCore.IsInBossMemoryMode;

    Harmony harmony = null!;
    
    FieldRef<MonsterStat, float> HealthFieldRef => FieldRefAccess<MonsterStat, float>("BaseHealthValue");
    
    [Preload(BIG_BAD_SCENE, BIG_BAD_PATH)]
    GameObject? preloadedObject;
    GameObject instantiatedObject;
    
    BossGeneralState currentBossState;
    ConfigEntry<bool> isUsingHotReload;
    bool hasInitialized;
    bool hasFinishedInitializing;
    bool hasAlreadyPreloaded;
    AmbienceSource phasesOst;
    KCore kCore;

    void Awake () 
    {
        KLog.Init(Logger);
        RCGLifeCycle.DontDestroyForever(gameObject);
        harmony = Harmony.CreateAndPatchAll(typeof(PromisedEigongMain).Assembly);
        KLog.Info($"{MyPluginInfo.PLUGIN_NAME}: Loaded.");
        KLog.Info($"Version: {MyPluginInfo.PLUGIN_VERSION}");
        ToastManager.Toast($"{MyPluginInfo.PLUGIN_NAME}: Loaded.");
        kCore = new KCore();
        kCore.Setup(this);
        kCore.MainAwake();
        kCore.Preloader.AddPreloadClass(this);
        isUsingHotReload = Config.Bind("Debug", "IsUsingHotReload", false, "Only Enable this to true if you're developing this mod with Hot Reload.");
        if (isUsingHotReload.Value)
            HandleTitleScreenMenuLoaded();
        AddListeners();
    }

    void Update ()
    {
        Scene activeScene = SceneManager.GetActiveScene();

        if (activeScene.name is not (SCENE_NORMAL_ENDING_EIGONG or SCENE_TRUE_ENDING_EIGONG))
            return;
        
        StartInitialization();
        SpawnBigBad();
        ChangeOST();
        ChangeOSTPhase3();
        ChangeEigongHealth();
        ChangeAttackWeights();
        ChangeAttackSpeeds();
        ChangeEigongColors();
        FinishInitialization();
        LogStates();
    }
    
    void AddListeners ()
    {
        SystemPatches.OnTitleScreenMenuLoaded += HandleTitleScreenMenuLoaded;
        PoolObjectPatches.OnFoundTaiDanger += HandleTaiDangerFound;
        PoolObjectPatches.OnFoundJieChuanFireParticles += HandleJieChuanFireParticlesFound;
        PoolObjectPatches.OnFoundJieChuanFireImage += HandleJieChuanFireImageFound;
        PoolObjectPatches.OnFoundFooExplosionSprite += HandleFooExplosionSpriteFound;
        PoolObjectPatches.OnFoundFooSprite += HandleFooSpriteFound;
        PoolObjectPatches.OnFoundCrimsonGeyserSprite += HandleCrimsonGeyserSpriteFound;
        PoolObjectPatches.OnFoundCrimsonPillarSprite += HandleCrimsonPillarSpriteFound;
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
    
    void SpawnBigBad ()
    {
        if (preloadedObject == null || instantiatedObject != null)
            return;
        Transform bgMasterTransform = 
            GameObject.Find(BG_MASTER_TRANSFORM).transform;
        instantiatedObject = Instantiate(preloadedObject, bgMasterTransform);
        instantiatedObject.SetActive(true);
    }

    void ChangeOST ()
    {
        if (!hasInitialized) 
            return;

        if (hasFinishedInitializing)
            return;

        phasesOst = GameObject.Find(BOSS_AMBIENCE_SOURCE).GetComponent<AmbienceSource>();
        phasesOst.ambPair.sound = PHASE1_2_OST;
        StartCoroutine(PlayOST(0));
        if (Player.i.health.CurrentHealthValue <= 0f)
            StopAllCoroutines();
    }
    
    void ChangeOSTPhase3 ()
    {
        if (EigongBase.currentMonsterState !=
            EigongBase.GetState(MonsterBase.States.FooStunEnter) ||
            EigongBase.PhaseIndex != 1) 
            return;
        phasesOst.ambPair.sound = PHASE3_OST;
        StartCoroutine(PlayOST(0));
        if (Player.i.health.CurrentHealthValue <= 0f)
            StopAllCoroutines();
    }
    
    IEnumerator PlayOST(float delay)
    {
        yield return new WaitForSeconds(delay);
        phasesOst.Play();
    }

    void ChangeEigongHealth ()
    {
        if (!hasInitialized) 
            return;

        if (hasFinishedInitializing)
            return;
        
        ToastManager.Toast("Initializing...");
        
        HealthFieldRef.Invoke(EigongBase.monsterStat) = EIGONG_PHASE_1_HEALTH_VALUE * DEBUG_BASE_MULTIPLIER;
        
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

        var weightChangerManager = new WeightChangerManager();
        weightChangerManager.ChangeWeights(
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
        );
    }
    
    void ChangeAttackSpeeds ()
    {
        if (!hasInitialized)
            return;
        
        if (hasFinishedInitializing)
            return;
        
        BossGeneralState[] allBossStates = FindObjectsOfType<BossGeneralState>();
        
        var speedChangerManager = new SpeedChangerManager();
        speedChangerManager.SetBossStates(allBossStates);
        speedChangerManager.ChangeSpeedValues(
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
        );
    }
    
    void ChangeEigongColors ()
    {
        if (!hasInitialized)
            return;
        
        if (hasFinishedInitializing)
            return;
        
        BurningFXApplier.ApplyBurningEffect(EIGONG_CHARACTER_BODY, EIGONG_BURNING_FX_STRENGTH);
        BurningFXApplier.ApplyBurningEffect(EIGONG_CHARACTER_BODY_SHADOW, EIGONG_BURNING_FX_STRENGTH);
        ColorChanger.ChangeColors(
            EIGONG_CHARACTER_SWORD, 
            EIGONG_CHARACTER_SWORD_EFFECT_SHIFT, 
            EIGONG_SWORD_SATURATION,
            EIGONG_CHARACTER_SWORD_VALUE_BRIGHTNESS
        );
        ColorChanger.ChangeColors(
            EIGONG_CHARACTER_FOO, 
            EIGONG_CHARACTER_SWORD_EFFECT_SHIFT, 
            null,
            EIGONG_CHARACTER_SWORD_VALUE_BRIGHTNESS
        );
        ColorChanger.ChangeColors(
            EIGONG_CHARACTER_SWORD_EFFECT, 
            EIGONG_CHARACTER_SWORD_EFFECT_SHIFT, 
            null,
            EIGONG_CHARACTER_SWORD_VALUE_BRIGHTNESS
        );
        ColorChanger.ChangeColors(
            EIGONG_CHARACTER_CRIMSON_SLAM_EFFECT, 
            EIGONG_CHARACTER_SWORD_EFFECT_SHIFT, 
            null,
            EIGONG_CHARACTER_SWORD_VALUE_BRIGHTNESS
        );
        ColorChanger.ChangeColors(
            EIGONG_CHARACTER_CRIMSON_GEYSER_EFFECT, 
            EIGONG_CHARACTER_SWORD_EFFECT_SHIFT, 
            EIGONG_SWORD_SATURATION,
            EIGONG_CHARACTER_SWORD_VALUE_BRIGHTNESS
        );
        ColorChanger.ChangeColors(
            FIRE_TRAIL_PATH, 
            EIGONG_CHARACTER_SWORD_EFFECT_SHIFT, 
            EIGONG_SWORD_SATURATION,
            EIGONG_CHARACTER_SWORD_VALUE_BRIGHTNESS
        );
        ColorChanger.ChangeColors(
            EIGONG_CHARGE_WAVE_CRIMSON, 
            EIGONG_CHARACTER_SWORD_EFFECT_SHIFT, 
            null,
            EIGONG_CHARACTER_SWORD_VALUE_BRIGHTNESS
        );
        ColorChanger.ChangeColors(
            EIGONG_CHARGE_WAVE_CRIMSON_GLOW, 
            EIGONG_CHARACTER_SWORD_EFFECT_SHIFT, 
            null,
            EIGONG_CHARACTER_SWORD_VALUE_BRIGHTNESS
        );
        ColorChanger.ChangeColors(
            EIGONG_CHARGE_WAVE_CRIMSON_2, 
            EIGONG_CHARACTER_SWORD_EFFECT_SHIFT, 
            null,
            EIGONG_CHARACTER_SWORD_VALUE_BRIGHTNESS
        );
        ColorChanger.ChangeColors(
            EIGONG_CHARGE_WAVE_CRIMSON_2_GLOW, 
            EIGONG_CHARACTER_SWORD_EFFECT_SHIFT, 
            null,
            EIGONG_CHARACTER_SWORD_VALUE_BRIGHTNESS
        );
        ParticleColorSetter.ChangeColors(EIGONG_CHARACTER_FOO_EFFECT_1, FIRE_COLOR);
        ParticleColorSetter.ChangeColors(EIGONG_CHARACTER_FOO_EFFECT_2, FIRE_COLOR);
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
        ColorChanger.ChangeColors(
            sprite.gameObject, 
            EIGONG_CHARACTER_SWORD_EFFECT_SHIFT,
            null,
            EIGONG_TAI_DANGER_VALUE_BRIGHTNESS,
            isLit: false
        );
    }
    
    void HandleJieChuanFireParticlesFound (ParticleSystem particleSystem)
    {
        ParticleColorSetter.ChangeColors(particleSystem.gameObject, FIRE_COLOR);
    }
    
    void HandleJieChuanFireImageFound (SpriteRenderer sprite)
    {
        RGBColorSetter.SetRGB(sprite.gameObject, FIRE_LIGHT_COLOR);
    }

    void HandleFooExplosionSpriteFound (SpriteRenderer sprite)
    {
        ColorChanger.ChangeColors(
            sprite.gameObject, 
            EIGONG_CHARACTER_SWORD_EFFECT_SHIFT, 
            null,
            EIGONG_TAI_DANGER_VALUE_BRIGHTNESS,
            isLit: false
        );
    }
    
    void HandleFooSpriteFound (SpriteRenderer sprite)
    {
        ColorChanger.ChangeColors(
            sprite.gameObject, 
            EIGONG_CHARACTER_SWORD_EFFECT_SHIFT, 
            null,
            EIGONG_TAI_DANGER_VALUE_BRIGHTNESS,
            isLit: false
        );
    }
    
    void HandleCrimsonGeyserSpriteFound (SpriteRenderer sprite)
    {
        ColorChanger.ChangeColors(
            sprite.gameObject, 
            EIGONG_CHARACTER_SWORD_EFFECT_SHIFT, 
            null,
            EIGONG_TAI_DANGER_VALUE_BRIGHTNESS,
            isLit: false
        );
    }
    
    void HandleCrimsonPillarSpriteFound (SpriteRenderer sprite)
    {
        ColorChanger.ChangeColors(
            sprite.gameObject, 
            EIGONG_CHARACTER_SWORD_EFFECT_SHIFT, 
            null,
            EIGONG_TAI_DANGER_VALUE_BRIGHTNESS,
            isLit: false
        );
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
        kCore.OnDestroy();
    }
}