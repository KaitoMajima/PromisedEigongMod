using PromisedEigong.Gameplay.AttackFactories;

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
using static HarmonyLib.AccessTools;
using static PromisedEigongModGlobalSettings.EigongRefs;
using static PromisedEigongModGlobalSettings.EigongHealth;
using static PromisedEigongModGlobalSettings.EigongOST;
using static PromisedEigongModGlobalSettings.EigongDebug;
using static PromisedEigongModGlobalSettings.EigongTitle;

public class EigongWrapper : MonoBehaviour
{
    public event Action<int>? OnCurrentEigongPhaseChanged;
    
    PromisedEigongMain MainInstance => PromisedEigongMain.Instance;
    
    bool IsInBossMemoryMode => ApplicationCore.IsInBossMemoryMode;
    MonsterBase LoadedEigong => SingletonBehaviour<MonsterManager>.Instance.ClosetMonster;
    EffectsManager EffectsManager => MainInstance.EffectsManager;
    
    FieldRef<MonsterStat, float> HealthFieldRef => FieldRefAccess<MonsterStat, float>("BaseHealthValue");
    
    bool hasInitialized;
    bool hasFinishedInitializing;
    BossGeneralState currentBossState;
    ConfigEntry<bool> isUsingHotReload;
    bool hasAlreadyPreloaded;
    AmbienceSource phasesOst;
    int currentEigongPhase;
    
    void Awake ()
    {
        MainInstance.SubscribeEigongWrapper(this);
        ChangeFixedEigongColors();
        ChangeEigongCutsceneTitle();
        currentEigongPhase = 0;
    }

    void Update ()
    {
        WaitForEigongInitialization();
        ChangeOSTPhase3();
        CheckEigongPhase();
        LogStates();
    }

    void CheckEigongPhase ()
    {
        if (LoadedEigong == null)
            return;
        
        if (currentEigongPhase == LoadedEigong.PhaseIndex)
            return;
        
        currentEigongPhase = LoadedEigong.PhaseIndex;
        OnCurrentEigongPhaseChanged?.Invoke(currentEigongPhase);
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
        ChangeAttackSpeeds();
        CreateNewAttacks();
        ChangeAttackWeights();
        ChangeCharacterEigongColors();
        ChangeOST();
        ChangeEigongHealth();
    }
    
    void ChangeOST ()
    {
        phasesOst = GameObject.Find(BOSS_AMBIENCE_SOURCE).GetComponent<AmbienceSource>();
        phasesOst.ambPair.sound = PHASE1_2_OST;
        StartCoroutine(PlayOST(0));
        if (Player.i.health.CurrentHealthValue <= 0f)
            StopAllCoroutines();
    }
    
    void ChangeOSTPhase3 ()
    {
        if (!hasInitialized)
            return;
            
        if (LoadedEigong.currentMonsterState !=
            LoadedEigong.GetState(MonsterBase.States.FooStunEnter) ||
            LoadedEigong.PhaseIndex != 1) 
            return;
        phasesOst.ambPair.sound = PHASE3_OST;
        StartCoroutine(PlayOST(0));
        if (Player.i.health.CurrentHealthValue <= 0f)
            StopAllCoroutines();
    }
    
    IEnumerator PlayOST (float delay)
    {
        yield return new WaitForSeconds(delay);
        phasesOst.Play();
    }

    void ChangeEigongHealth ()
    {
        HealthFieldRef.Invoke(LoadedEigong.monsterStat) = EIGONG_PHASE_1_HEALTH_VALUE * BASE_HEALTH_MULTIPLIER;
        
        if (IsInBossMemoryMode)
            LoadedEigong.postureSystem.CurrentHealthValue = EIGONG_PHASE_1_HEALTH_VALUE * LoadedEigong.monsterStat.BossMemoryHealthScale * PHASE_1_HEALTH_MULTIPLIER;
        else
            LoadedEigong.postureSystem.CurrentHealthValue = EIGONG_PHASE_1_HEALTH_VALUE * PHASE_1_HEALTH_MULTIPLIER;
        
        LoadedEigong.monsterStat.Phase2HealthRatio = (float)EIGONG_PHASE_2_HEALTH_VALUE / EIGONG_PHASE_1_HEALTH_VALUE * PHASE_2_HEALTH_MULTIPLIER;
        LoadedEigong.monsterStat.Phase3HealthRatio = (float)EIGONG_PHASE_3_HEALTH_VALUE / EIGONG_PHASE_1_HEALTH_VALUE * PHASE_3_HEALTH_MULTIPLIER;
    }

    void ChangeAttackWeights ()
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
    }
    
    void CreateNewAttacks ()
    {
        var crimsonBallFactory = new InstantCrimsonBallFactory();
        BossGeneralState[] allBossStates = FindObjectsOfType<BossGeneralState>();
        foreach (var bossState in allBossStates)
        {
            if (bossState.name == crimsonBallFactory.attackToBeCopied)
                crimsonBallFactory.CopyAttack(bossState);
        }
    }
    
    void ChangeAttackSpeeds ()
    {
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
    
    void LogStates ()
    {
        if (!EIGONG_STATE_LOG)
            return;
        
        if (currentBossState == (BossGeneralState)LoadedEigong.currentMonsterState)
            return;
        currentBossState = (BossGeneralState)LoadedEigong.currentMonsterState;
        ToastManager.Toast($"Next state: [{currentBossState.name}]");
    }
}