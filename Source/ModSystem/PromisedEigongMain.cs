using UnityEngine;

namespace PromisedEigong.ModSystem;

using UnityEngine.SceneManagement;
using Effects;
using BepInEx.Configuration;
using Core;
using BepInEx;
using HarmonyLib;
using NineSolsAPI;

[BepInDependency(NineSolsAPICore.PluginGUID)]
[BepInPlugin(
    PromisedEigong.MyPluginInfo.PLUGIN_GUID, 
    PromisedEigong.MyPluginInfo.PLUGIN_NAME,
    PromisedEigong.MyPluginInfo.PLUGIN_VERSION
    )
]
public class PromisedEigongMain : BaseUnityPlugin, ICoroutineRunner
{
    public static PromisedEigongMain Instance { get; private set; }
    public PreloadingManager PreloadingManager { get; private set; }
    public EffectsManager EffectsManager { get; private set; }
    
    Harmony harmony = null!;

    ConfigEntry<bool> isUsingHotReload;
    bool canPreload = true;

    void Awake ()
    {
        Instance = this;
        Initialize();
        DisplayLoadedVersion();
        InitializeSubmodels();
        ApplyConfig();
        AddListeners();
    }

    void Initialize ()
    {
        KLog.Init(Logger);
        RCGLifeCycle.DontDestroyForever(gameObject);
        harmony = Harmony.CreateAndPatchAll(typeof(PromisedEigongMain).Assembly);
    }

    void DisplayLoadedVersion ()
    {
        ToastManager.Toast($"{PromisedEigong.MyPluginInfo.PLUGIN_NAME}: Loaded.");
        ToastManager.Toast($"Version: {PromisedEigong.MyPluginInfo.PLUGIN_VERSION}");
    }

    void InitializeSubmodels ()
    {
        PreloadingManager = new PreloadingManager(this);
        PreloadingManager.Setup();
        EffectsManager = new EffectsManager();
        EffectsManager.Setup();
    }
    
    void ApplyConfig ()
    {
        isUsingHotReload = Config.Bind("Debug", "IsUsingHotReload", false, "Only Enable this to true if you're developing this mod with Hot Reload.");
        if (isUsingHotReload.Value)
            HandleTitleScreenMenuLoaded();
    }

    void AddListeners ()
    {
        SystemPatches.OnTitleScreenMenuLoaded += HandleTitleScreenMenuLoaded;
        SystemPatches.OnTitleScreenMenuUnloaded += HandleTitleScreenMenuUnloaded;
        PreloadingManager.OnLoadingDone += HandlePreloadingDone;
    }

    void HandleTitleScreenMenuLoaded ()
    {
        KLog.Info("Trying to preload, is preload scene? " + canPreload);
        if (!canPreload)
            return;
        
        PreloadingManager.StartPreloading();
        canPreload = false;
    }
    
    void HandleTitleScreenMenuUnloaded ()
    {
        canPreload = true;
    }
    
    void HandlePreloadingDone ()
    {
        SceneManager.LoadScene("TitleScreenMenu");
    }

    void OnDestroy () 
    {
        harmony.UnpatchSelf();
        PreloadingManager.Dispose();
        EffectsManager.Dispose();
    }
}