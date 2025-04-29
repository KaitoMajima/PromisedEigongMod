namespace PromisedEigong;

using UnityEngine;
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
    
    FieldRef<MonsterStat, float> FieldRef => FieldRefAccess<MonsterStat, float>("BaseHealthValue");


    bool isHealthChanged;

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
        
        ChangeEigongHealth();
    }

    void ChangeEigongHealth ()
    {
        FieldRef.Invoke(EigongBase.monsterStat) = EIGONG_PHASE_1_HEALTH_VALUE;
        
        if (!isHealthChanged && EigongBase != null)
        {
            if (IsInBossMemoryMode)
                EigongBase.postureSystem.CurrentHealthValue = EIGONG_PHASE_1_HEALTH_VALUE * EigongBase.monsterStat.BossMemoryHealthScale;
            else
                EigongBase.postureSystem.CurrentHealthValue = EIGONG_PHASE_1_HEALTH_VALUE;

            isHealthChanged = true;
        }
        EigongBase.monsterStat.Phase2HealthRatio = (float)EIGONG_PHASE_2_HEALTH_VALUE / EIGONG_PHASE_1_HEALTH_VALUE;
        EigongBase.monsterStat.Phase3HealthRatio = (float)EIGONG_PHASE_3_HEALTH_VALUE / EIGONG_PHASE_1_HEALTH_VALUE;
    }
    
    void OnDestroy () 
    {
        harmony.UnpatchSelf();
    }
}