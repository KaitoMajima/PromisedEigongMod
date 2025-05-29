namespace PromisedEigong.ModSystem;
#nullable disable

using System;
using LevelChangers;
using UnityEngine;
using UnityEngine.UI;
using static PromisedEigongModGlobalSettings.EigongDebug;

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
    public Action OnEigongWrapperUpdated;
    public static PromisedEigongMain Instance { get; private set; }
    public PreloadingManager PreloadingManager { get; private set; }
    public EffectsManager EffectsManager { get; private set; }
    public WhiteScreen WhiteScreen { get; private set; }
    public EigongWrapper EigongWrapper { get; private set; }
    public Harmony Harmony = null!;
    
    public static ConfigEntry<bool> isLvl0Challenge;
    

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

    public void SubscribeEigongWrapper (EigongWrapper eigongWrapper)
    {
        EigongWrapper = eigongWrapper;
        OnEigongWrapperUpdated?.Invoke();
    }

    void Initialize ()
    {
        KLog.Init(Logger);
        RCGLifeCycle.DontDestroyForever(gameObject);
        Harmony = Harmony.CreateAndPatchAll(typeof(PromisedEigongMain).Assembly);
    }

    void DisplayLoadedVersion ()
    {
        ToastManager.Toast($"{PromisedEigong.MyPluginInfo.PLUGIN_NAME} v{PromisedEigong.MyPluginInfo.PLUGIN_VERSION}: Loaded.");
    }

    void InitializeSubmodels ()
    {
        PreloadingManager = new PreloadingManager(this);
        PreloadingManager.Setup();
        EffectsManager = new EffectsManager();
        EffectsManager.Setup();
        var whiteScreenGameObject = new GameObject();
        RCGLifeCycle.DontDestroyForever(whiteScreenGameObject);
        WhiteScreen = whiteScreenGameObject.AddComponent<WhiteScreen>();
    }
    
    void ApplyConfig ()
    {
        isUsingHotReload = Config.Bind("Debug", "IsUsingHotReload", false, "Only Enable this to true if you're developing this mod with Hot Reload.");
        isLvl0Challenge = Config.Bind("Debug", "LVL0Challenge", false, "(STILL WIP) For now, all this does is make the fire damage more forgiving. Makes lvl 0 challenges a little more bearable.");
        if (isUsingHotReload.Value)
            HandleTitleScreenMenuLoaded();
    }

    void AddListeners ()
    {
        SystemPatches.OnPauseOpened += HandlePauseOpened;
        SystemPatches.OnTitleScreenMenuLoaded += HandleTitleScreenMenuLoaded;
        SystemPatches.OnTitleScreenMenuUnloaded += HandleTitleScreenMenuUnloaded;
        PreloadingManager.OnLoadingDone += HandlePreloadingDone;
    }
    
    void HandlePauseOpened (PauseUIPanel pauseMenu)
    {
        if (!IS_PAUSE_MENU_BG_INVISIBLE)
            return;
        
        var pauseMenuImage = pauseMenu.GetComponent<Image>();
        pauseMenuImage.enabled = false;
    }

    void HandleTitleScreenMenuLoaded ()
    {
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
        Harmony.UnpatchSelf();
        PreloadingManager.Dispose();
        EffectsManager.Dispose();
    }
}