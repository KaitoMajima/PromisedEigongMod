using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PromisedEigong.Gameplay.AttackFactories;

public class ModifiedBossGeneralState : MonoBehaviour
{
    public string ModifiedName { get; set; }
    public string OriginalName { get; private set; }
    public float ForcePlayAnimAtNormalizeTime { get; set; }
    public float AnimationSpeed { get; set; }
    public List<AttackWeight> Phase1Weights { get; set; }
    public List<AttackWeight> Phase2Weights { get; set; }
    public List<AttackWeight> Phase3Weights { get; set; }

    public BossGeneralState BaseState { get; set; }

    List<string> stateMoveSources = new();
    float selectionWeight;

    float originalForcePlayAnimAtNormalizeTime;
    float originalAnimationSpeed;
    List<AttackWeight> originalPhase1Weights = new();
    List<AttackWeight> originalPhase2Weights = new();
    List<AttackWeight> originalPhase3Weights = new();

    public void Setup (string name, BossGeneralState baseState, float selectionWeight)
    {
        this.name = name;
        BaseState = baseState;
        this.selectionWeight = selectionWeight;
        ModifiedName = OriginalName = BaseState.name;
        ForcePlayAnimAtNormalizeTime = originalForcePlayAnimAtNormalizeTime = BaseState.forcePlayAnimAtNormalizeTime;
        AnimationSpeed = originalAnimationSpeed = BaseState.AnimationSpeed;
        
        for (var i = 0; i < BaseState.linkNextMoveStateWeights.Count; i++)
        {
            switch (i)
            {
                case 0:
                    Phase1Weights = originalPhase1Weights = BaseState.linkNextMoveStateWeights[i].stateWeightList;
                    break;
                case 1:
                    Phase2Weights = originalPhase2Weights = BaseState.linkNextMoveStateWeights[i].stateWeightList;
                    break;
                case 2:
                    Phase3Weights = originalPhase3Weights = BaseState.linkNextMoveStateWeights[i].stateWeightList;
                    break;
            }
        }
    }

    public void SubscribeSource (string sourceStateName)
    {
        stateMoveSources.Add(sourceStateName);
    }

    public void ApplyMoveModification ()
    {
        BaseState.forcePlayAnimAtNormalizeTime = ForcePlayAnimAtNormalizeTime;
        BaseState.AnimationSpeed = AnimationSpeed;
        
        for (var i = 0; i < BaseState.linkNextMoveStateWeights.Count; i++)
        {
            switch (i)
            {
                case 0:
                    BaseState.linkNextMoveStateWeights[i].stateWeightList = Phase1Weights;
                    break;
                case 1:
                    BaseState.linkNextMoveStateWeights[i].stateWeightList = Phase2Weights;
                    break;
                case 2:
                    BaseState.linkNextMoveStateWeights[i].stateWeightList = Phase3Weights;
                    break;
            }
        }

        BaseState.GetComponent<BossStateIdentifier>().IdName = ModifiedName;
    }
    
    public void ApplyMoveRevert ()
    {
        BaseState.forcePlayAnimAtNormalizeTime = originalForcePlayAnimAtNormalizeTime;
        BaseState.AnimationSpeed = originalAnimationSpeed;
        
        for (var i = 0; i < BaseState.linkNextMoveStateWeights.Count; i++)
        {
            switch (i)
            {
                case 0:
                    BaseState.linkNextMoveStateWeights[i].stateWeightList = originalPhase1Weights;
                    break;
                case 1:
                    BaseState.linkNextMoveStateWeights[i].stateWeightList = originalPhase2Weights;
                    break;
                case 2:
                    BaseState.linkNextMoveStateWeights[i].stateWeightList = originalPhase3Weights;
                    break;
            }
        }

        BaseState.GetComponent<BossStateIdentifier>().IdName = OriginalName;
    }

    public bool CanBeModified (BossStateIdentifier previousState)
    {
        return stateMoveSources.Any(x => x == previousState.IdName);
    }
}