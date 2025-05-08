using System;
using PromisedEigong.Core;

namespace PromisedEigong.ModSystem;

using NineSolsAPI.Preload;
using UnityEngine;
using static PromisedEigongModGlobalSettings.EigongPreloadRefs;

public class PreloadingManager (ICoroutineRunner coroutineRunner)
{
    [Preload(BIG_BAD_SCENE, BIG_BAD_PATH)]
    GameObject? bigBad;

    public Action OnLoadingDone;
    
    public GameObject? BigBad => bigBad;
    
    KPreload preloader;
    bool hasLoaded;
    
    public void Setup ()
    {
        preloader = new KPreload(HandleProgressStep);
        preloader.AddPreloadClass(this);
    }

    public void StartPreloading ()
    {
        if (hasLoaded)
            return;
        coroutineRunner.StartCoroutine(preloader.Preload(coroutineRunner));
    }
    
    void OnLoadDone ()
    {
        if (hasLoaded)
            return;
        hasLoaded = true;
        OnLoadingDone?.Invoke();
    }

    void HandleProgressStep (float progress)
    {
        if (progress < 1)
            return;
        OnLoadDone();
    }

    public void OnDestroy ()
    {
        preloader.Unload();
    }
}