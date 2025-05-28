using PromisedEigong.LevelChangers;

namespace PromisedEigong.ModSystem;
#nullable disable

using Effects.GameplayEffects;
using Effects.MusicPlayers;
using Gameplay;
using Gameplay.AttackFactories;
using Gameplay.HealthChangers;

using System;
using TMPro;

using NineSolsAPI;
using Effects;
using System.Collections;
using SpeedChangers;
using WeightChanges;
using UnityEngine;
using static PromisedEigongModGlobalSettings.EigongRefs;
using static PromisedEigongModGlobalSettings.EigongHealth;
using static PromisedEigongModGlobalSettings.EigongOST;
using static PromisedEigongModGlobalSettings.EigongDebug;
using static PromisedEigongModGlobalSettings.EigongTitle;
using static PromisedEigongModGlobalSettings.EigongAttacks;
using static PromisedEigongModGlobalSettings.EigongVFX;

public class EigongWrapper : MonoBehaviour, ICoroutineRunner
{
    public event Action<int> OnCurrentEigongPhaseChangedPreAnimation;
    public event Action<int> OnCurrentEigongPhaseChangedPostAnimation;
    public event Action OnEigongTransformed;
    public MonsterBase LoadedEigong => SingletonBehaviour<MonsterManager>.Instance.ClosetMonster;
    
    PromisedEigongMain MainInstance => PromisedEigongMain.Instance;
    EffectsManager EffectsManager => MainInstance.EffectsManager;
    WhiteScreen WhiteScreen => MainInstance.WhiteScreen;
    
    bool hasInitialized;
    bool hasFinishedInitializing;
    bool hasEigongDied;
    AmbienceSource phasesOst;
    BossPhaseProvider bossPhaseProvider;
    GameplayEffectManager gameplayEffectManager;
    SpeedChangerManager speedChangerManager;
    AttackEventManager attackEventManager;
    SceneConnectionPoint cutsceneConnectionPoint;
   
    void Awake ()
    {
        MainInstance.SubscribeEigongWrapper(this);
        ChangeFixedEigongColors();
        ChangeEigongCutsceneTitle();
        bossPhaseProvider = new BossPhaseProvider();
        gameplayEffectManager = new GameplayEffectManager();
        attackEventManager = new AttackEventManager();
        AddListeners();
        
        //TODO: lvl 1 challenge logic
        // Player.i.mainAbilities.AirJumpAbility.IsAcquired = false;
        // Player.i.mainAbilities.ParryJumpKickAbility.IsAcquired = false;
        // Player.i.mainAbilities.ChargedAttackAbility.IsAcquired = false;
        // Player.i.mainAbilities.ArrowAbility.IsAcquired = false;
        // Player.i.mainAbilities.RollDodgeInAirUpgrade.IsAcquired = false;
        // Player.i.mainAbilities.ParryCounterAbility.IsAcquired = false;
        // Player.i.potion.potionMaxCountData.Stat.BaseValue = 2;
        // Player.i.potion.potionMaxCountData.Stat.Clear();
        // Player.i.chiContainer.MaxValueStatData.Stat.BaseValue = 1;
        // Player.i.chiContainer.MaxValueStatData.Stat.Clear();
        // var test = Player.i.potion.potionMaxCountData.Stat.Value;
    }

    void Update ()
    {
        WaitForEigongInitialization();
        bossPhaseProvider.HandleUpdateStep();
    }
    
    void AddListeners ()
    {
        BossPhaseProvider.OnPhaseChangePreAnimation += HandlePhaseChangePreAnimation;
        BossPhaseProvider.OnPhaseChangePostAnimation += HandlePhaseChangePostAnimation;
        GeneralGameplayPatches.OnAttackStartCalled += HandleEigongStateChanged;
    }

    void RemoveListeners ()
    {
        BossPhaseProvider.OnPhaseChangePreAnimation  -= HandlePhaseChangePreAnimation;
        BossPhaseProvider.OnPhaseChangePostAnimation -= HandlePhaseChangePostAnimation;
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
        cutsceneConnectionPoint = GameObject.Find(CUTSCENE_SCENE_CHANGER_PATH).GetComponent<SceneConnectionPoint>();
        ChangeCharacterEigongColors();
        ChangeOST();
        ChangeEigongHealth();
        BossGeneralState[] allBossStates = FindObjectsOfType<BossGeneralState>();
        ChangeAttackSpeedsPhase1(allBossStates);
        CreateNewAttacks(allBossStates);
        ModifiedBossGeneralState[] allModifiedBossStates = FindObjectsOfType<ModifiedBossGeneralState>();
        ChangeAttackWeights(allModifiedBossStates);
        AddAttackIdentifiers(allBossStates);
        bossPhaseProvider.Setup(LoadedEigong);
        gameplayEffectManager.Setup(LoadedEigong, this, this);
        attackEventManager.Setup(this, FindObjectOfType<JudgmentCutSpawners>());
        gameplayEffectManager.Initialize();
        attackEventManager.Initialize();
        LoadedEigong.postureSystem.OnPostureEmpty.AddListener(HandlePostureEmpty);
    }
    

    void ChangeOST ()
    {
        phasesOst = GameMusicPlayer.GetAmbienceSourceAtPath(BOSS_AMBIENCE_SOURCE_PATH);
        GameMusicPlayer.ChangeMusic(this, phasesOst, PHASE1_2_OST, 0);
    }

    void ChangeEigongHealth ()
    {
        var mainHealthChanger = new MainHealthChanger();
        mainHealthChanger.ChangeBaseHealthStat(LoadedEigong, EIGONG_PHASE_1_HEALTH_VALUE);
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
            new _4SlashUpWeightChanger(),
            new _5TeleportToBackWeightChanger(),
            new _6DoubleAttackWeightChanger(),
            new _7TeleportForwardWeightChanger(),
            new _8LongChargeWeightChanger(),
            new _9StarterWeightChanger(),
            new _10FooWeightChanger(),
            new _11ChargeWaveWeightChanger(),
            new _12SlashUpCrimsonWeightChanger(),
            new _13TriplePokeWeightChanger(),
            new _15TurnAroundWeightChanger(),
            new _16QuickFooWeightChanger(),
            new _17CrimsonSlamWeightChanger(),
            new _18TeleportToBackComboWeightChanger(),
            new _X1PostureBreakWeightChanger(),
            new _X2JumpBackWeightChanger(),
            new _X3AttackParryingWeightChanger()
        );
        
        foreach (var modifiedBossGeneralState in modifiedBossGeneralStates)
            modifiedBossGeneralState.ResetOriginalWeights();
    }
    
    void CreateNewAttacks (BossGeneralState[] allBossStates)
    {
        BaseAttackFactory[] factories = [
            new _21QuickCrimsonBallFactory(), 
            new _22SlowStarterPokeChainFactory(),
            new _23TriplePokeChainFactory(),
            new _24DoubleAttackPokeChainFactory(),
            new _25SlashUpCrimsonPokeChainFactory(),
            new _26TeleportToBackSecondPokeChainFactory(),
            new _27ChargeWavePokeChainFactory(),
            new _28TeleportToBackComboPokeChainFactory(),
            new _29TeleportToBackFirstPokeChainFactory(),
            new _30JumpBackPokeChainFactory(),
            new _32TeleportToBackSpecialCrimsonSlamPhase3Factory(),
            new _33SpecialCrimsonSlamPhase3Factory(),
            new _34TeleportToTopSafeguardPhase3Factory(),
            new _35JumpBackSafeguardPhase3Factory(),
            new _36AlternateSlashUpCrimsonPokeChainFactory(),
            new _37AlternateSlashUpCrimsonPokeChainFactory(),
            new _38SpecialDoubleAttackFactory(),
            new _39SpecialDAJumpBackPhase3Factory(),
            new _40TeleportToBackComboPhase3Changer()
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
        
        var attacksParent = GameObject.Find(STATES_PATH);
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
    
    void ChangeAttackSpeedsPhase1 (BossGeneralState[] allBossStates)
    {
        speedChangerManager = new SpeedChangerManager();
        speedChangerManager.SetBossStates(allBossStates);
        speedChangerManager.SetPhase(0);
        speedChangerManager.SetupSpeedChangers(
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
            new _X1PostureBreakSpeedChanger(),
            new _X2JumpBackSpeedChanger(),
            new _X3AttackParryingSpeedChanger(),
            new _X4RollThroughSpeedChanger()
        );
        speedChangerManager.ProcessSpeeds();
    }
    
    void ChangeAttackSpeedsPhase3 ()
    {
        speedChangerManager.SetPhase(2);
        speedChangerManager.ProcessSpeeds();
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
        LogStates(currentState);
    }
    
    void HandlePostureEmpty ()
    {
        if (hasEigongDied)
            return;

        if (BossPhaseProvider.CurrentPostAnimationPhase == 2)
        {
            MainInstance.StartCoroutine(BlinkWhiteScreenOnEigongDeath(!ApplicationCore.IsInBossMemoryMode));
            hasEigongDied = true;   
        }
    }
    
    void HandlePhaseChangePreAnimation (int phase)
    {
        if (phase == 1)
        {
            GameMusicPlayer.ChangeMusic(this, phasesOst, PHASE3_OST, 0);
            ChangeAttackSpeedsPhase3();
            StartCoroutine(WaitForEigongTransformation());
        }
        OnCurrentEigongPhaseChangedPreAnimation?.Invoke(phase);
    }

    void HandlePhaseChangePostAnimation (int phase)
    {
        OnCurrentEigongPhaseChangedPostAnimation?.Invoke(phase);
    }
    
    IEnumerator WaitForEigongTransformation ()
    {
        yield return new WaitForSeconds(EIGONG_TRANSFORM_PHASE_3_DELAY);
        OnEigongTransformed?.Invoke();
    }

    IEnumerator BlinkWhiteScreenOnEigongDeath (bool transitionIntoCutscene)
    {
        float timer = 2.5f;
        WhiteScreen.Show();
        yield return new WaitForSeconds(WhiteScreen.ShowDuration);
        if (transitionIntoCutscene)
            cutsceneConnectionPoint.ForceChangeScene();
        
        while (timer > 0)
        {
            timer -= Time.deltaTime;
            yield return null;
        }
        
        WhiteScreen.Hide();
    }
    
    void LogStates (BossStateIdentifier currentState)
    {
        if (EIGONG_STATE_LOG)
            ToastManager.Toast($"Next attack state: [{currentState.IdName}]");
    }

    void OnDestroy ()
    {
        RemoveListeners();
        gameplayEffectManager?.Dispose();
        bossPhaseProvider?.Dispose();
        attackEventManager?.Dispose();
    }
}