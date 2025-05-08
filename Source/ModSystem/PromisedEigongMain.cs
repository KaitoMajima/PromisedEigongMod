using PromisedEigong.Effects;
using UnityEngine.SceneManagement;

namespace PromisedEigong;

using BepInEx.Configuration;
using Core;
using ModSystem;
using BepInEx;
using HarmonyLib;
using NineSolsAPI;

[BepInDependency(NineSolsAPICore.PluginGUID)]
[BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
public class PromisedEigongMain : BaseUnityPlugin, ICoroutineRunner
{
    public static PromisedEigongMain Instance { get; private set; }
    public PreloadingManager PreloadingManager { get; private set; }
    public EffectsManager EffectsManager { get; private set; }
    
    Harmony harmony = null!;

    ConfigEntry<bool> isUsingHotReload;

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
        ToastManager.Toast($"{MyPluginInfo.PLUGIN_NAME}: Loaded.");
        ToastManager.Toast($"Version: {MyPluginInfo.PLUGIN_VERSION}");
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
        PreloadingManager.OnLoadingDone += HandlePreloadingDone;
    }
    
    void RemoveListeners ()
    {
        SystemPatches.OnTitleScreenMenuLoaded -= HandleTitleScreenMenuLoaded;
        PreloadingManager.OnLoadingDone -= HandlePreloadingDone;
    }

    void HandleTitleScreenMenuLoaded ()
    {
        PreloadingManager.StartPreloading();
    }
    
    void HandlePreloadingDone ()
    {
        SceneManager.LoadScene("TitleScreenMenu");
    }

    void OnDestroy () 
    {
        RemoveListeners();
        harmony.UnpatchSelf();
        PreloadingManager.OnDestroy();
    }
}