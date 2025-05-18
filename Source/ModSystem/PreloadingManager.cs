namespace PromisedEigong.ModSystem;
#nullable disable

using System;
using PromisedEigong.Core;


using NineSolsAPI.Preload;
using UnityEngine;
using static PromisedEigongModGlobalSettings.EigongPreloadRefs;

public class PreloadingManager (ICoroutineRunner coroutineRunner)
{
    [Preload(BIG_BAD_SCENE, BIG_BAD_PATH)]
    GameObject bigBad;
    
    [Preload(BIG_BAD_SCENE, BEAUTIFUL_FX_CAMERA_PRELOAD_PATH)]
    GameObject beautifulFXCamera;

    public event Action OnLoadingDone;
    
    public GameObject BigBad => bigBad;
    public GameObject BeautifulFXCamera => beautifulFXCamera;
    
    KPreload preloader;
    
    public void Setup ()
    {
        preloader = new KPreload(HandleProgressStep);
        preloader.AddPreloadClass(this);
    }

    public void StartPreloading ()
    {
        coroutineRunner.StartCoroutine(preloader.Preload(coroutineRunner));
    }
    
    void OnLoadDone ()
    {
        OnLoadingDone?.Invoke();
    }

    void HandleProgressStep (float progress)
    {
        if (progress < 1)
            return;
        OnLoadDone();
    }

    public void Dispose ()
    {
        preloader.Unload();
    }
}