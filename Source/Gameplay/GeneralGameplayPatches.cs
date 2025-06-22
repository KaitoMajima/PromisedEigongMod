using System.Linq;
using PromisedEigong.ModSystem;
using UnityEngine;

namespace PromisedEigong.Gameplay;
#nullable disable

using System.Collections.Generic;
using AttackFactories;
using UnityEngine.SceneManagement;
using HarmonyLib;
using static PromisedEigongModGlobalSettings.EigongRefs;

public delegate void OnAttackEnterHandler (BossStateIdentifier previousState, BossStateIdentifier currentState);
public delegate void OnAttackStartHandler (BossStateIdentifier currentState);


[HarmonyPatch]
public class GeneralGameplayPatches
{
    public static event OnAttackEnterHandler OnAttackEnterCalled;
    public static event OnAttackStartHandler OnAttackStartCalled;

    static BossStateIdentifier previousState;
    static BossStateIdentifier currentState;

    static System.Random systemRandom;

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
    [HarmonyPatch(typeof(BossGeneralState), "LinkMove")]
    public static bool MoveShuffle (BossGeneralState __instance)
    {
        if (!PromisedEigongMain.shufflerChallenge.Value)
            return true;

        Scene activeScene = SceneManager.GetActiveScene();

        if (activeScene.name is not (SCENE_NORMAL_ENDING_EIGONG or SCENE_TRUE_ENDING_EIGONG))
            return true;

        if (__instance.linkNextMoveStateWeights.Count < __instance.monster.PhaseIndex + 1)
            return true;

        LinkNextMoveStateWeight currentLinkMoveSet =
            __instance.linkNextMoveStateWeights[__instance.monster.PhaseIndex];

        systemRandom ??= new System.Random();
        List<AttackWeight> newStateWeights =
            currentLinkMoveSet.stateWeightList.OrderBy(_ => systemRandom.NextDouble()).ToList();
        currentLinkMoveSet.stateWeightList = newStateWeights;
        return true;
    }

    [HarmonyPrefix]
    [HarmonyPatch(typeof(BossGeneralState), "LinkMoveInterruptConditional")]
    public static bool FixInterruptMoves (BossGeneralState __instance)
    {
        Scene activeScene = SceneManager.GetActiveScene();

        if (activeScene.name is not (SCENE_NORMAL_ENDING_EIGONG or SCENE_TRUE_ENDING_EIGONG))
            return true;

        if (Player.i.CurrentStateType == PlayerStateType.HurtGrabbed)
            return false;

        if (__instance.linkInterruptMoveConditionalWeights.Count < __instance.monster.PhaseIndex + 1)
            return false;

        LinkNextMoveStateWeight currentInterruptLinkMoveSet =
            __instance.linkInterruptMoveConditionalWeights[__instance.monster.PhaseIndex];

        List<AttackWeight> stateWeightList = currentInterruptLinkMoveSet.stateWeightList;

        if (stateWeightList == null || stateWeightList.Count == 0)
            return false;

        List<AttackWeight> selectedStateWeights = [];

        for (int i = 0; i < stateWeightList.Count; ++i)
        {
            if (stateWeightList[i].weight != 0)
                selectedStateWeights.Add(stateWeightList[i]);
        }

        if (selectedStateWeights.Count == 0)
            return false;

        AttackWeight selectedStateWeight = selectedStateWeights[Random.Range(0, selectedStateWeights.Count)];
        MonsterBase.States selectedStateType = selectedStateWeight.StateType;
        var selectedState = __instance.monster.ChangeStateIfValid(selectedStateType) as BossGeneralState;

        if (selectedState != null)
        {
            selectedState.bindSensor = __instance.bindSensor;
            __instance.bindSensor = null;
            if (selectedState.state == MonsterBase.States.Dead)
                __instance.monster.deathType = MonsterBase.DeathType.SelfDestruction;
        }

        return false;
    }

    [HarmonyPrefix]
    [HarmonyPatch(typeof(LinkNextMoveStateWeight), "EnqueueAttacks")]
    public static void OnAttackSensorPreparingEnqueuedAttacks (LinkNextMoveStateWeight __instance,
        List<MonsterBase.States> QueuedAttacks)
    {
        Scene activeScene = SceneManager.GetActiveScene();

        if (activeScene.name is not (SCENE_NORMAL_ENDING_EIGONG or SCENE_TRUE_ENDING_EIGONG))
            return;

        QueuedAttacks.Clear();
    }
}