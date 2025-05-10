using System;
using RCGMaker.Core;

namespace PromisedEigong.PreloadObjectHandlers;

using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using static PromisedEigongModGlobalSettings.EigongBackground;
using static PromisedEigongModGlobalSettings.EigongRefs;

public class BeautifulFXCameraHandler : MonoBehaviour
{
    bool waitedForFirstUpdateFrame;

    void Awake ()
    {
        name = BEAUTIFUL_FX_CAMERA_NAME;
    }

    void Update ()
    {
        if (waitedForFirstUpdateFrame)
            return;
        
        RemoveShakeComponent();
        
        waitedForFirstUpdateFrame = true;
    }

    void RemoveShakeComponent ()
    {
        GetComponent<CameraFilterPack_FX_EarthQuake>().enabled = false;
        GetComponent<PostProcessVolume>().weight = BEAUTIFUL_FX_CAMERA_WEIGHT;
    }
}