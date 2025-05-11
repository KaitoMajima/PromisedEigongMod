using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PromisedEigong.Gameplay.AttackFactories;

public class ModifiedBossGeneralState : MonoBehaviour
{
    public float ForcePlayAnimAtNormalizeTime { get; set; }
    public float AnimationSpeed { get; set; }
    public BossGeneralState BaseState { get; set; }

    List<BossGeneralState> stateMoveSources = new();
    float selectionWeight;
    
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
        ForcePlayAnimAtNormalizeTime = originalForcePlayAnimAtNormalizeTime = baseState.forcePlayAnimAtNormalizeTime;
        AnimationSpeed = originalAnimationSpeed = baseState.AnimationSpeed;
    }

    public void SubscribeSource (BossGeneralState sourceState)
    {
        stateMoveSources.Add(sourceState);
    }
    
    void AddListeners ()
    {
        GeneralGameplayPatches.OnAttackEnterCalled += HandleAttackEnterCalled;
    }
    
    void RemoveListeners ()
    {
        GeneralGameplayPatches.OnAttackEnterCalled -= HandleAttackEnterCalled;
    }

    void ApplyMoveModification (BossGeneralState state)
    {
        state.forcePlayAnimAtNormalizeTime = ForcePlayAnimAtNormalizeTime;
        state.AnimationSpeed = AnimationSpeed;
    }
    
    void ApplyMoveRevert (BossGeneralState state)
    {
        state.forcePlayAnimAtNormalizeTime = originalForcePlayAnimAtNormalizeTime;
        state.AnimationSpeed = originalAnimationSpeed;
    }
    
    void HandleAttackEnterCalled (BossGeneralState previousState, BossGeneralState currentState)
    {
        if (currentState.name != BaseState.name)
            return;
        
        if (stateMoveSources.Any(x => x.name == previousState.name))
            ApplyMoveModification(BaseState);
        else
            ApplyMoveRevert(BaseState);
    }

    void OnDestroy ()
    {
        RemoveListeners();
    }
}