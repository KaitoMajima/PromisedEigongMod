using System;
using System.Collections.Generic;
using NineSolsAPI;
using PromisedEigong.Gameplay.AttackFactories;
using UnityEngine.SceneManagement;

namespace PromisedEigong.Gameplay;

using HarmonyLib;
using static PromisedEigongModGlobalSettings.EigongRefs;

public delegate void OnAttackEnterHandler (BossStateIdentifier previousState, BossStateIdentifier currentState);
public delegate void OnAttackStartHandler (BossStateIdentifier currentState);


[HarmonyPatch]
public class GeneralGameplayPatches
{
    public static OnAttackEnterHandler? OnAttackEnterCalled;
    public static OnAttackStartHandler? OnAttackStartCalled;
    
    static BossStateIdentifier? previousState;
    static BossStateIdentifier? currentState;
    
    [HarmonyPrefix]
    [HarmonyPatch(typeof(BossGeneralState), "OnStateEnter")]
    public static void OnStateEnterCall (BossGeneralState __instance)
    {
        Scene activeScene = SceneManager.GetActiveScene();
        
        if (activeScene.name is not (SCENE_NORMAL_ENDING_EIGONG or SCENE_TRUE_ENDING_EIGONG))
            return;
        
        if (previousState == null)
        {
            previousState = __instance.GetComponent<BossStateIdentifier>();
            return;
        }
        
        currentState = __instance.GetComponent<BossStateIdentifier>();

        if (currentState == null)
        {
            previousState = null;
            return;
        }
        
        OnAttackEnterCalled?.Invoke(previousState, currentState);
        previousState = currentState;
    }
    
    [HarmonyPostfix]
    [HarmonyPatch(typeof(BossGeneralState), "OnStateEnter")]
    public static void OnStateStartCall (BossGeneralState __instance)
    {
        Scene activeScene = SceneManager.GetActiveScene();
        
        if (activeScene.name is not (SCENE_NORMAL_ENDING_EIGONG or SCENE_TRUE_ENDING_EIGONG))
            return;
        
        if (currentState == null)
            return;
        
        OnAttackStartCalled?.Invoke(currentState);
    }
    
    [HarmonyPrefix]
    [HarmonyPatch(typeof(LinkNextMoveStateWeight), "EnqueueAttacks")]
    public static void OnAttackSensorPreparingEnqueuedAttacks (LinkNextMoveStateWeight __instance, List<MonsterBase.States> QueuedAttacks)
    {
        Scene activeScene = SceneManager.GetActiveScene();
        
        if (activeScene.name is not (SCENE_NORMAL_ENDING_EIGONG or SCENE_TRUE_ENDING_EIGONG))
            return;
        
        // if (ModifiedBossGeneralStateManager.IsCurrentAttackAChain)
        QueuedAttacks.Clear();
    }
}