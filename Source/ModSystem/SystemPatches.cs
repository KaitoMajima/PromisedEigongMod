namespace PromisedEigong.ModSystem;

using System;
using HarmonyLib;
using UnityEngine.SceneManagement;
using static PromisedEigongModGlobalSettings.EigongRefs;

[HarmonyPatch]
public class SystemPatches
{
    public static Action OnTitleScreenMenuLoaded;

    [HarmonyPrefix]
    [HarmonyPatch(typeof(StartMenuLogic), "Awake")]
    static void TitleScreenSpawn (StartMenuLogic __instance)
    {
        OnTitleScreenMenuLoaded?.Invoke();
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
                break;
        }
    }
}