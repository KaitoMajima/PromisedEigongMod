using System.Collections.Generic;
using System.Linq;
using NineSolsAPI;
using UnityEngine;

namespace PromisedEigong.Gameplay.AttackFactories;

public class ModifiedBossGeneralState : MonoBehaviour
{
    public string ModifiedName { get; set; }
    public float ForcePlayAnimAtNormalizeTime { get; set; }
    public float AnimationSpeed { get; set; }
    public BossGeneralState BaseState { get; set; }

    List<string> stateMoveSources = new();
    float selectionWeight;

    string originalName;
    float originalForcePlayAnimAtNormalizeTime;
    float originalAnimationSpeed;

    void Awake ()
    {
        AddListeners();
    }

    public void Setup (string name, BossGeneralState baseState, float selectionWeight)
    {
        this.name = name;
        BaseState = baseState;
        this.selectionWeight = selectionWeight;
        ModifiedName = originalName = BaseState.name;
        ForcePlayAnimAtNormalizeTime = originalForcePlayAnimAtNormalizeTime = BaseState.forcePlayAnimAtNormalizeTime;
        AnimationSpeed = originalAnimationSpeed = BaseState.AnimationSpeed;
    }

    public void SubscribeSource (string sourceStateName)
    {
        stateMoveSources.Add(sourceStateName);
    }
    
    void AddListeners ()
    {
        GeneralGameplayPatches.OnAttackEnterCalled += HandleAttackEnterCalled;
        GeneralGameplayPatches.OnAttackExitCalled += HandleAttackExitCalled;
    }

    void RemoveListeners ()
    {
        GeneralGameplayPatches.OnAttackEnterCalled -= HandleAttackEnterCalled;
        GeneralGameplayPatches.OnAttackExitCalled -= HandleAttackExitCalled;
    }

    void ApplyMoveModification ()
    {
        BaseState.forcePlayAnimAtNormalizeTime = ForcePlayAnimAtNormalizeTime;
        BaseState.AnimationSpeed = AnimationSpeed;
        BaseState.GetComponent<BossStateIdentifier>().Setup(ModifiedName);
        ToastManager.Toast("Modified Move: " + BaseState.GetComponent<BossStateIdentifier>().IdName);
    }
    
    void ApplyMoveRevert ()
    {
        BaseState.forcePlayAnimAtNormalizeTime = originalForcePlayAnimAtNormalizeTime;
        BaseState.AnimationSpeed = originalAnimationSpeed;
        BaseState.GetComponent<BossStateIdentifier>().Setup(originalName);
        ToastManager.Toast("Reverted Move: " + BaseState.GetComponent<BossStateIdentifier>().IdName);
    }
    
    void HandleAttackEnterCalled (BossStateIdentifier previousState, BossStateIdentifier currentState)
    {
        if (currentState.IdName != BaseState.name)
            return;
        
        if (stateMoveSources.Any(x => x == previousState.IdName))
            ApplyMoveModification();
    }
    
    void HandleAttackExitCalled (BossStateIdentifier currentState)
    {
        if (currentState.IdName != ModifiedName)
            return;
        
        ApplyMoveRevert();
    }

    void OnDestroy ()
    {
        RemoveListeners();
    }
    
    // Ball -> Instant Ball
    // 
}