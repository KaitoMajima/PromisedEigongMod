using System.Collections.Generic;
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

[BepInDependency(NineSolsAPICore.PluginGUID)]
[BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
public class PromisedEigongMain : BaseUnityPlugin
{
    MonsterBase EigongBase => SingletonBehaviour<MonsterManager>.Instance.ClosetMonster;
    bool IsInBossMemoryMode => ApplicationCore.IsInBossMemoryMode;

    Harmony harmony = null!;
    
    FieldRef<MonsterStat, float> HealthFieldRef => FieldRefAccess<MonsterStat, float>("BaseHealthValue");


    BossGeneralState currentBossState;
    bool hasInitialized;
    bool hasFinishedInitializing;

    void Awake () 
    {
        Log.Init(Logger);
        RCGLifeCycle.DontDestroyForever(gameObject);
        harmony = Harmony.CreateAndPatchAll(typeof(PromisedEigongMain).Assembly);
        Logger.LogInfo($"{MyPluginInfo.PLUGIN_NAME}: Loaded.");
        Logger.LogInfo($"Version: {MyPluginInfo.PLUGIN_VERSION}");
        ToastManager.Toast($"{MyPluginInfo.PLUGIN_NAME}: Loaded.");
    }

    void Update ()
    {
        Scene activeScene = SceneManager.GetActiveScene();

        if (activeScene.name is not (SCENE_NORMAL_ENDING_EIGONG or SCENE_TRUE_ENDING_EIGONG))
            return;
        
        StartInitialization();
        ChangeEigongHealth();
        ChangeAttackWeights();
        ChangeAttackSpeeds();
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
    
    void ChangeEigongHealth ()
    {
        if (!hasInitialized) 
            return;

        if (hasFinishedInitializing)
            return;
        
        HealthFieldRef.Invoke(EigongBase.monsterStat) = EIGONG_PHASE_1_HEALTH_VALUE;
        
        if (IsInBossMemoryMode)
            EigongBase.postureSystem.CurrentHealthValue = EIGONG_PHASE_1_HEALTH_VALUE * EigongBase.monsterStat.BossMemoryHealthScale;
        else
            EigongBase.postureSystem.CurrentHealthValue = EIGONG_PHASE_1_HEALTH_VALUE;
        
        EigongBase.monsterStat.Phase2HealthRatio = (float)EIGONG_PHASE_2_HEALTH_VALUE / EIGONG_PHASE_1_HEALTH_VALUE;
        EigongBase.monsterStat.Phase3HealthRatio = (float)EIGONG_PHASE_3_HEALTH_VALUE / EIGONG_PHASE_1_HEALTH_VALUE;
        
        ToastManager.Toast("Changed health.");
    }

    void ChangeAttackWeights ()
    {
        if (!hasInitialized)
            return;
        
        if (hasFinishedInitializing)
            return;
        
        var weightChangers = new List<BaseWeightChanger>
        {
            new SlowStarterWeightChanger(),
            new StarterWeightChanger(),
            new TriplePokeWeightChanger(),
            new TurnAroundWeightChanger(),
            new CrimsonSlamWeightChanger(),
            new PostureBreakWeightChanger(),
            new AttackParryingWeightChanger()
        };
        foreach (var weightChanger in weightChangers)
            weightChanger.ChangeAttackWeight();
        
        ToastManager.Toast("Changed weights.");
    }
    
    void ChangeAttackSpeeds ()
    {
        if (!hasInitialized)
            return;
        
        if (hasFinishedInitializing)
            return;

        BaseSpeedChanger[] speedChangers = [
            new SlowStarterSpeedChanger(), 
            new TeleportToTopSpeedChanger(),
            new ThrustDelaySpeedChanger(),
            new TeleportToBackSpeedChanger(),
            new DoubleAttackSpeedChanger(),
            new TeleportForwardSpeedChanger(),
            new StarterSpeedChanger(),
            new FooSpeedChanger(),
            new ChargeWaveSpeedChanger(),
            new TriplePokeSpeedChanger(),
            new CrimsonBallSpeedChanger(),
            new TurnAroundSpeedChanger(),
            new QuickFooSpeedChanger(),
            new CrimsonSlamSpeedChanger(),
            new JumpBackSpeedChanger()
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
        
        ToastManager.Toast("All speeds changed.");
    }
    
    void LogStates ()
    {
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