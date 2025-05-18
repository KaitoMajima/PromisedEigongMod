namespace PromisedEigong.PoolObjectWrapper;
#nullable disable

using System;
using ModSystem;
using HarmonyLib;
using UnityEngine;
using UnityEngine.SceneManagement;
using static PromisedEigongModGlobalSettings.EigongRefs;
using static PromisedEigongModGlobalSettings.EigongVFX;

[HarmonyPatch]
public class PoolObjectPatches
{
    public static event Action<SpriteRenderer> OnFoundTaiDanger;
    public static event Action<SpriteRenderer> OnFoundSimpleTaiDanger;
    public static event Action<ParticleSystem> OnFoundJieChuanFireParticles;
    public static event Action<SpriteRenderer> OnFoundJieChuanFireImage;
    public static event Action<SpriteRenderer> OnFoundFooExplosionSprite;
    public static event Action<SpriteRenderer> OnFoundFooSprite;
    public static event Action<SpriteRenderer> OnFoundCrimsonGeyserSprite;
    public static event Action<SpriteRenderer> OnFoundCrimsonPillarSprite;
    
    [HarmonyPostfix]
    [HarmonyPatch(typeof(PoolObject), "EnterLevelAwake")]
    static void PoolHijack (PoolObject __instance)
    {
        Scene activeScene = SceneManager.GetActiveScene();
        
        if (activeScene.name is not (SCENE_NORMAL_ENDING_EIGONG or SCENE_TRUE_ENDING_EIGONG))
            return;
        
        InvokeCallbackForComponents(__instance, PO_VFX_TAI_DANGER, OnFoundTaiDanger);
        InvokeCallbackForComponents(__instance, PO_VFX_EIGONG_FOO_EXPLOSION, OnFoundFooExplosionSprite);
        InvokeCallbackForComponents(__instance, PO_VFX_EIGONG_FOO, OnFoundFooSprite);
        InvokeCallbackForComponents(__instance, PO_VFX_EIGONG_CRIMSON_GEYSER, OnFoundCrimsonGeyserSprite);
        InvokeCallbackForComponents(__instance, PO_VFX_EIGONG_CRIMSON_PILLAR, OnFoundCrimsonPillarSprite);
        InvokeCallbackForComponents(__instance, PO_VFX_JIECHUAN_FIRE, OnFoundJieChuanFireParticles);
        InvokeCallbackForComponents(__instance, PO_VFX_JIECHUAN_FIRE, OnFoundJieChuanFireImage, PO_VFX_JIECHUAN_FIRE_IMAGE);
    }
    
    [HarmonyPostfix]
    [HarmonyPatch(typeof(CustomPoolObject), "EnterLevelAwake")]
    static void PoolHijack (CustomPoolObject __instance)
    {
        Scene activeScene = SceneManager.GetActiveScene();
        
        if (activeScene.name is not (SCENE_NORMAL_ENDING_EIGONG or SCENE_TRUE_ENDING_EIGONG))
            return;
        
        InvokeCallbackForComponents(__instance, SIMPLE_DANGER_VFX_NAME, OnFoundSimpleTaiDanger);
    }

    static void InvokeCallbackForComponents <T> (MonoBehaviour instance, string objName, Action<T> callback) where T : Component
    {
        if (instance.name != objName) 
            return;
        
        var objs = instance.GetComponentsInChildren<T>(true);
        foreach (var obj in objs)
            callback?.Invoke(obj);
    }
    
    static void InvokeCallbackForComponents <T> (MonoBehaviour instance, string objName, Action<T> callback, string objNameComparison) where T : Component
    {
        if (instance.name != objName) 
            return;
        
        var objs = instance.GetComponentsInChildren<T>(true);
        foreach (var obj in objs)
        {
            if (objNameComparison == obj.name)
                callback?.Invoke(obj);
        }
    }
}