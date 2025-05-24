namespace PromisedEigong.ModSystem;

using System;
using HarmonyLib;
using LevelChangers;
using UnityEngine.SceneManagement;
using static PromisedEigongModGlobalSettings.EigongRefs;

[HarmonyPatch]
public class SystemPatches
{
    public static event Action<PauseUIPanel>? OnPauseOpened;
    public static event Action? OnTitleScreenMenuLoaded;
    public static event Action? OnTitleScreenMenuUnloaded;
    
    [HarmonyPrefix]
    [HarmonyPatch(typeof(StartMenuLogic), "Awake")]
    static void TitleScreenSpawn (StartMenuLogic __instance)
    {
        OnTitleScreenMenuLoaded?.Invoke();
    }
    
    [HarmonyPrefix]
    [HarmonyPatch(typeof(StartMenuLogic), "CreateOrLoadSaveSlotAndPlay")]
    static void TitleScreenDespawn (StartMenuLogic __instance)
    {
        OnTitleScreenMenuUnloaded?.Invoke();
    }
    
    [HarmonyPrefix]
    [HarmonyPatch(typeof(StealthGameMonster), "Awake")]
    static void AddEigongWrapper (StealthGameMonster __instance)
    {
        if (__instance.BindMonster.Name == BIND_MONSTER_EIGONG_NAME)
            __instance.gameObject.AddComponent<EigongWrapper>();
    }
    
    [HarmonyPrefix]
    [HarmonyPatch(typeof(GameLevel), "Awake")]
    static void AddGameLevelComponents (GameLevel __instance)
    {
        Scene activeScene = SceneManager.GetActiveScene();

        switch (activeScene.name)
        {
            case SCENE_NEW_KUNLUN:
                __instance.AddComp(typeof(NewKunlunRoomChanger));
                break;
            case SCENE_NORMAL_ENDING_EIGONG or SCENE_TRUE_ENDING_EIGONG:
                __instance.AddComp(typeof(RootPinnacleBackgroundChanger));
                __instance.AddComp(typeof(TitleChanger));
                __instance.AddComp(typeof(JudgmentCutSpawners));
                break;
        }
    }
    
    [HarmonyPostfix]
    [HarmonyPatch(typeof(PauseUIPanel), "ShowInit")]
    static void CallPauseOpened (PauseUIPanel __instance)
    {
        OnPauseOpened?.Invoke(__instance);
    }
}