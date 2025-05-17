using PromisedEigong.Effects.MusicPlayers;
using PromisedEigong.Gameplay;
using PromisedEigong.Gameplay.AttackFactories;
using PromisedEigong.Gameplay.HealthChangers;

namespace PromisedEigong.ModSystem;

using System;
using TMPro;

using NineSolsAPI;
using Effects;
using System.Collections;
using BepInEx.Configuration;
using SpeedChangers;
using WeightChanges;
using UnityEngine;
using static PromisedEigongModGlobalSettings.EigongRefs;
using static PromisedEigongModGlobalSettings.EigongHealth;
using static PromisedEigongModGlobalSettings.EigongOST;
using static PromisedEigongModGlobalSettings.EigongDebug;
using static PromisedEigongModGlobalSettings.EigongTitle;
using static PromisedEigongModGlobalSettings.EigongAttacks;
using static PromisedEigongModGlobalSettings.EigongSFX;
using static PromisedEigongModGlobalSettings.EigongVFX;

public class EigongWrapper : MonoBehaviour, ICoroutineRunner
{
    public event Action<int>? OnCurrentEigongPhaseChangedPreAnimation;
    public event Action<int>? OnCurrentEigongPhaseChangedPostAnimation;
    
    PromisedEigongMain MainInstance => PromisedEigongMain.Instance;
    
    MonsterBase LoadedEigong => SingletonBehaviour<MonsterManager>.Instance.ClosetMonster;
    EffectsManager EffectsManager => MainInstance.EffectsManager;
    
    bool hasInitialized;
    bool hasFinishedInitializing;
    ConfigEntry<bool> isUsingHotReload;
    bool hasAlreadyPreloaded;
    AmbienceSource phasesOst;
    BossPhaseProvider bossPhaseProvider;
   
    void Awake ()
    {
        MainInstance.SubscribeEigongWrapper(this);
        ChangeFixedEigongColors();
        ChangeEigongCutsceneTitle();
        bossPhaseProvider = new BossPhaseProvider();
        AddListeners();
    }

    void Update ()
    {
        WaitForEigongInitialization();
        bossPhaseProvider.HandleUpdateStep();
    }
    
    void AddListeners ()
    {
        bossPhaseProvider.OnPhaseChangePreAnimation += HandlePhaseChangePreAnimation;
        bossPhaseProvider.OnPhaseChangePostAnimation += HandlePhaseChangePostAnimation;
        GeneralGameplayPatches.OnAttackStartCalled += HandleEigongStateChanged;
    }

    void RemoveListeners ()
    {
        bossPhaseProvider.OnPhaseChangePreAnimation  -= HandlePhaseChangePreAnimation;
        bossPhaseProvider.OnPhaseChangePostAnimation -= HandlePhaseChangePostAnimation;
        GeneralGameplayPatches.OnAttackStartCalled -= HandleEigongStateChanged;
    }
    
    void WaitForEigongInitialization ()
    {
        hasInitialized = LoadedEigong != null;
        
        if (!hasInitialized)
        {
            hasFinishedInitializing = false;
            return;
        }

        if (hasFinishedInitializing)
            return;
        
        HandleEigongInitialization();
        
        hasFinishedInitializing = true;
    }

    void HandleEigongInitialization ()
    {
        BossGeneralState[] allBossStates = FindObjectsOfType<BossGeneralState>();
        ChangeAttackSpeeds(allBossStates);
        CreateNewAttacks(allBossStates);
        ModifiedBossGeneralState[] allModifiedBossStates = FindObjectsOfType<ModifiedBossGeneralState>();
        ChangeAttackWeights(allModifiedBossStates);
        AddAttackIdentifiers(allBossStates);
        ChangeCharacterEigongColors();
        ChangeOST();
        ChangeEigongHealth();
        bossPhaseProvider.Setup(LoadedEigong);
    }
    
    void ChangeOST ()
    {
        phasesOst = GameMusicPlayer.GetAmbienceSourceAtPath(BOSS_AMBIENCE_SOURCE_PATH);
        GameMusicPlayer.ChangeMusic(this, phasesOst, PHASE1_2_OST, 0);
    }

    void ChangeEigongHealth ()
    {
        var mainHealthChanger = new MainHealthChanger();
        mainHealthChanger.ChangeBaseHealthStat(LoadedEigong, EIGONG_PHASE_1_HEALTH_VALUE * PHASE_1_HEALTH_MULTIPLIER);
        mainHealthChanger.ChangeHealthInPhase1(LoadedEigong, EIGONG_PHASE_1_HEALTH_VALUE * PHASE_1_HEALTH_MULTIPLIER);
        mainHealthChanger.ChangeHealthInRemainingPhases(LoadedEigong, 1, 
            EIGONG_PHASE_2_HEALTH_VALUE * PHASE_2_HEALTH_MULTIPLIER,
            EIGONG_PHASE_1_HEALTH_VALUE
        );
        mainHealthChanger.ChangeHealthInRemainingPhases(LoadedEigong, 2, 
            EIGONG_PHASE_3_HEALTH_VALUE * PHASE_3_HEALTH_MULTIPLIER, 
            EIGONG_PHASE_1_HEALTH_VALUE
        );
    }

    void ChangeAttackWeights (ModifiedBossGeneralState[] modifiedBossGeneralStates)
    {
        var weightChangerManager = new WeightChangerManager();
        weightChangerManager.ChangeWeights(
            new _1SlowStarterWeightChanger(),
            new _2TeleportToTopWeightChanger(),
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
            new _18TeleportToBackComboWeightChanger(),
            new _X1PostureBreakWeightChanger(),
            new _X3AttackParryingWeightChanger()
        );
        
        foreach (var modifiedBossGeneralState in modifiedBossGeneralStates)
            modifiedBossGeneralState.ResetOriginalWeights();
    }
    
    void CreateNewAttacks (BossGeneralState[] allBossStates)
    {
        BaseAttackFactory[] factories = [
            new InstantCrimsonBallFactory(), 
            new SlowStarterPokeChainFactory(),
            new TriplePokeChainFactory(),
            new DoubleAttackPokeChainFactory(),
            new TeleportToBackFirstPokeChainFactory(),
            new SlashUpCrimsonPokeChainFactory(),
            new TeleportToBackSecondPokeChainFactory(),
            new ChargeWavePokeChainFactory(),
            new TeleportToBackComboPokeChainFactory()
        ];
        
        foreach (var factory in factories)
        {
            foreach (var bossState in allBossStates)
            {
                if (bossState.name != factory.AttackToBeCopied) 
                    continue;
                
                factory.CopyAttack(bossState);
                break;
            }
        }
        
        var attacksParent = GameObject.Find(ATTACK_PATH);
        attacksParent.AddComponent<ModifiedBossGeneralStateManager>();
    }

    void AddAttackIdentifiers (BossGeneralState[] allBossStates)
    {
        foreach (var bossState in allBossStates)
        {
            var bossStateIdentifier = bossState.gameObject.AddComponent<BossStateIdentifier>();
            bossStateIdentifier.IdName = bossState.name;
        }
    }
    
    void ChangeAttackSpeeds (BossGeneralState[] allBossStates)
    {
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
    
    void ChangeFixedEigongColors ()
    {
        EffectsManager.ChangeFixedEigongColors();
    }
    
    void ChangeCharacterEigongColors ()
    {
        EffectsManager.ChangeCharacterEigongColors();
    }
    
    void ChangeEigongCutsceneTitle ()
    {
        var cutsceneTitle = GameObject.Find(EIGONG_CUTSCENE_TITLE_NAME_PATH);
        cutsceneTitle.GetComponent<TextMeshPro>().text = EIGONG_TITLE;
    }
    
    void HandleEigongStateChanged (BossStateIdentifier currentState)
    {
        if (currentState.IdName == ATTACK9_STARTER)
        {
            var simpleDangerSFX = GameObject.Find(SIMPLE_DANGER_SFX_PATH);
            var soundPlayer = simpleDangerSFX.GetComponent<SoundPlayer>();
            for (int i = 0; i < SIMPLE_DANGER_SFX_BOOST; i++)
                soundPlayer.SimplePlay();
            var dangerVFX = GameObject.Find(DANGER_VFX_PATH);
            var dangerVFXPoolObj = dangerVFX.GetComponent<FxPlayer>().EmitPoolObject;
            //TODO: Pool
            var isEigongFacingRight = LoadedEigong.Facing is Facings.Right;
            var dangerVFXInstance = Instantiate(dangerVFXPoolObj.gameObject,
                isEigongFacingRight
                    ? LoadedEigong.gameObject.transform.position + SIMPLE_DANGER_VFX_OFFSET_L
                    : LoadedEigong.gameObject.transform.position + SIMPLE_DANGER_VFX_OFFSET_R, 
                Quaternion.identity);
            Destroy(dangerVFXInstance.GetComponent<PoolObject>());
            var dangerVFXPoolObject = dangerVFXInstance.AddComponent<CustomPoolObject>();
            dangerVFXInstance.transform.localScale = SIMPLE_DANGER_VFX_SCALE;
            dangerVFXInstance.name = SIMPLE_DANGER_VFX_NAME;
            var spriteRenderers = dangerVFXInstance.GetComponentsInChildren<SpriteRenderer>();
            
            foreach (var spriteRenderer in spriteRenderers)
                spriteRenderer.forceRenderingOff = true;
            StartCoroutine(ShowAfterDelay(SIMPLE_DANGER_VFX_HIDE_TIME, dangerVFXPoolObject, spriteRenderers));
        }
        LogStates(currentState);
    }
    
    void HandlePhaseChangePreAnimation (int phase)
    {
        if (phase == 1)
            GameMusicPlayer.ChangeMusic(this, phasesOst, PHASE3_OST, 0);
        OnCurrentEigongPhaseChangedPreAnimation?.Invoke(phase);
    }
    
    void HandlePhaseChangePostAnimation (int phase)
    {
        OnCurrentEigongPhaseChangedPostAnimation?.Invoke(phase);
    }

    IEnumerator ShowAfterDelay (float delay, CustomPoolObject customPoolObj, SpriteRenderer[] spriteRenderers)
    {
        yield return new WaitForSeconds(delay);
        foreach (var spriteRenderer in spriteRenderers)
            spriteRenderer.forceRenderingOff = false;
        customPoolObj.EnterLevelAwake();
    }
    
    void LogStates (BossStateIdentifier currentState)
    {
        if (EIGONG_STATE_LOG)
            ToastManager.Toast($"Next attack state: [{currentState.IdName}]");
    }

    void OnDestroy ()
    {
        RemoveListeners();
    }
}