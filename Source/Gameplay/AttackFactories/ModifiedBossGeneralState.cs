namespace PromisedEigong.Gameplay.AttackFactories;
#nullable disable

using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static HarmonyLib.AccessTools;

public class ModifiedBossGeneralState : MonoBehaviour
{
    public string ModifiedName { get; set; }
    public string OriginalName { get; private set; }
    public float ForcePlayAnimAtNormalizeTime { get; set; }
    public float AnimationSpeed { get; set; }
    public Vector3 StartOffset { get; set; }
    public List<AttackWeight> Phase1Weights { get; set; }
    public List<AttackWeight> Phase2Weights { get; set; }
    public List<AttackWeight> Phase3Weights { get; set; }
    public bool ShouldClearGroupingNodes { get; set; }
    public bool IsFromAChain { get; set; }
    public int PhaseIndexLock { get; set; }

    public BossGeneralState BaseState { get; set; }

    List<string> stateMoveSources = new();
    float selectionWeight;

    float originalForcePlayAnimAtNormalizeTime;
    float originalAnimationSpeed;
    Vector3 originalStartOffset = Vector3.zero;
    List<AttackWeight> originalPhase1Weights = new();
    List<AttackWeight> originalPhase2Weights = new();
    List<AttackWeight> originalPhase3Weights = new();
    LinkMoveGroupingNode[] originalGroupingNodes;
    int originalPhaseIndexLock = -1;
    
    FieldRef<BossGeneralState, LinkMoveGroupingNode[]> groupingNodesRef 
        = FieldRefAccess<BossGeneralState, LinkMoveGroupingNode[]>("groupingNodes");
    
    FieldRef<LinkMoveGroupingNode, MonsterStateQueue> queueRef 
        = FieldRefAccess<LinkMoveGroupingNode, MonsterStateQueue>("queue");

    public void Setup (string name, BossGeneralState baseState, float selectionWeight)
    {
        this.name = name;
        BaseState = baseState;
        this.selectionWeight = selectionWeight;
        ModifiedName = OriginalName = BaseState.name;
        ForcePlayAnimAtNormalizeTime = originalForcePlayAnimAtNormalizeTime = BaseState.forcePlayAnimAtNormalizeTime;
        AnimationSpeed = originalAnimationSpeed = BaseState.AnimationSpeed;
        StartOffset = originalStartOffset;
        PhaseIndexLock = originalPhaseIndexLock;
    }
    
    public void SubscribeSource (string sourceStateName)
    {
        stateMoveSources.Add(sourceStateName);
    }

    public void ResetOriginalWeights ()
    {
        if (ShouldClearGroupingNodes)
            originalGroupingNodes = groupingNodesRef.Invoke(BaseState);
        else
        {
            UpdateOriginalStateWeightList(
                BaseState.linkNextMoveStateWeights, 
                ref originalPhase1Weights, 
                ref originalPhase2Weights, 
                ref originalPhase3Weights
            );

            Phase1Weights ??= originalPhase1Weights;
            Phase2Weights ??= originalPhase2Weights;
            Phase3Weights ??= originalPhase3Weights;
        }
    }
    
    public void ApplyMoveModification ()
    {
        if (StartOffset != Vector3.zero)
        {
            if (BaseState.monster.Facing is Facings.Right)
                BaseState.monster.transform.localPosition += StartOffset;
            else
                BaseState.monster.transform.localPosition += new Vector3(StartOffset.x * -1, StartOffset.y, StartOffset.z);
        }
        
        BaseState.forcePlayAnimAtNormalizeTime = ForcePlayAnimAtNormalizeTime;
        BaseState.AnimationSpeed = AnimationSpeed;
        
        if (ShouldClearGroupingNodes)
        {
            var groupingNodes = groupingNodesRef.Invoke(BaseState);
            var firstGroupingNode = groupingNodes[0];
            var nodeQueue = queueRef.Invoke(firstGroupingNode);
            
            UpdateCurrentStateWeightList(
                ref nodeQueue.linkNextMoveStateWeights, 
                Phase1Weights, 
                Phase2Weights, 
                Phase3Weights
            );
            groupingNodesRef.Invoke(BaseState) = [firstGroupingNode];
        }
        else
        {
            UpdateCurrentStateWeightList(
                ref BaseState.linkNextMoveStateWeights, 
                Phase1Weights, 
                Phase2Weights, 
                Phase3Weights
            );
        }
        
        BaseState.GetComponent<BossStateIdentifier>().IdName = ModifiedName;
    }

    public void ApplyMoveRevert ()
    {
        BaseState.forcePlayAnimAtNormalizeTime = originalForcePlayAnimAtNormalizeTime;
        BaseState.AnimationSpeed = originalAnimationSpeed;

        if (ShouldClearGroupingNodes)
        {
            groupingNodesRef.Invoke(BaseState) = originalGroupingNodes;
            
            var groupingNodes = groupingNodesRef.Invoke(BaseState);
            var firstGroupingNode = groupingNodes[0];
            var nodeQueue = queueRef.Invoke(firstGroupingNode);
            
            UpdateCurrentStateWeightList(
                ref nodeQueue.linkNextMoveStateWeights, 
                originalPhase1Weights, 
                originalPhase2Weights, 
                originalPhase3Weights
            );
        }
        else
        {
            UpdateCurrentStateWeightList(
                ref BaseState.linkNextMoveStateWeights, 
                originalPhase1Weights, 
                originalPhase2Weights, 
                originalPhase3Weights
            );
        }
        
        BaseState.GetComponent<BossStateIdentifier>().IdName = OriginalName;
    }

    public bool CanBeModified (BossStateIdentifier previousState)
    { 
        if (PhaseIndexLock > -1 && PhaseIndexLock != BaseState.monster.PhaseIndex)
            return false;
        
        return stateMoveSources.Any(x => x == previousState.IdName);
    }
    
    void UpdateOriginalStateWeightList (
        List<LinkNextMoveStateWeight> nextMoveWeights, 
        ref List<AttackWeight> phase1Weights,
        ref List<AttackWeight> phase2Weights, 
        ref List<AttackWeight> phase3Weights
    )
    {
        for (var i = 0; i < nextMoveWeights.Count; i++)
        {
            switch (i)
            {
                case 0:
                    phase1Weights = nextMoveWeights[i].stateWeightList;
                    break;
                case 1:
                    phase2Weights = nextMoveWeights[i].stateWeightList;
                    break;
                case 2:
                    phase3Weights = nextMoveWeights[i].stateWeightList;
                    break;
            }
        }
    }

    void UpdateCurrentStateWeightList (
        ref List<LinkNextMoveStateWeight> nextMoveWeights, 
        List<AttackWeight> phase1Weights,
        List<AttackWeight> phase2Weights, 
        List<AttackWeight> phase3Weights
    )
    {
        for (var i = 0; i < nextMoveWeights.Count; i++)
        {
            switch (i)
            {
                case 0:
                    nextMoveWeights[i].stateWeightList = phase1Weights;
                    break;
                case 1:
                    nextMoveWeights[i].stateWeightList = phase2Weights;
                    break;
                case 2:
                    nextMoveWeights[i].stateWeightList = phase3Weights;
                    break;
            }
        }
    }
}