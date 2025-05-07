namespace PromisedEigong.Core;

using BepInEx;
using HarmonyLib;
using System;
using UnityEngine.SceneManagement;

[BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
public class KCore
{
    internal static PromisedEigongMain Main { get; private set; }
    internal KPreload Preloader => Instance.preloader;
    
    internal static KCore Instance;
    KPreload preloader;
    Harmony harmony;

    bool hasLoaded;
    
    float LoadProgress
    {
        set
        {
            if (value < 1.0)
                return;
            OnLoadDone();
        }
    }

    internal void Setup (PromisedEigongMain main) 
        => Main = main;
    
    internal void MainAwake ()
    {
        Instance = this;
        try
        {
            LoadProgress = 0.0f;
            preloader = new KPreload(progress => LoadProgress = progress);
        }
        catch (Exception ex)
        {
            KLog.Error($"Failed to initialize modding API: {ex}");
        }
    
        KLog.Info("K API loaded");
    }

    internal void StartPreloading ()
    {
        KLog.Info("Preloading starting!");
        Main.StartCoroutine(preloader.Preload());
    }

    void OnLoadDone ()
    {
        if (hasLoaded)
            return;
        hasLoaded = true;
        SceneManager.LoadScene("TitleScreenMenu");
    }

    internal void OnDestroy ()
    {
        preloader.Unload();
        harmony.UnpatchSelf();
        KLog.Info("KLOG! K API unloaded");
    }
}