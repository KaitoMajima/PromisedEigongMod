namespace PromisedEigong.WeightChanges;
#nullable disable

using System;
using System.Collections.Generic;
using Gameplay.AttackFactories;
using UnityEngine;
using static HarmonyLib.AccessTools;

public abstract class BaseWeightChanger
{
    protected virtual WeightReplaceMode WeightReplaceMode { get; set; } = WeightReplaceMode.Replace;

    protected BossGeneralState associatedBossState;
    protected LinkNextMoveStateWeight targetStateWeight = new();
    protected List<BossGeneralState> bossStates = new();
    protected List<ModifiedBossGeneralState> modifiedBossStates = new();

    FieldRef<BossGeneralState, LinkMoveGroupingNode[]> groupingNodesRef 
        = FieldRefAccess<BossGeneralState, LinkMoveGroupingNode[]>("groupingNodes");
    
    FieldRef<LinkMoveGroupingNode, MonsterStateQueue> queueRef 
        = FieldRefAccess<LinkMoveGroupingNode, MonsterStateQueue>("queue");
    
    public abstract void ChangeAttackWeight ();

    protected virtual void ProcessCurrentWeight ()
    {
        var attackWeights = new List<AttackWeight>();

        foreach (var bossState in bossStates)
            attackWeights.Add(CreateWeight(bossState));

        foreach (var bossState in modifiedBossStates)
            attackWeights.Add(CreateWeight(bossState));
        
        switch (WeightReplaceMode)
        {
            case WeightReplaceMode.Replace:
                targetStateWeight.stateWeightList = attackWeights;
                break;
            case WeightReplaceMode.Add:
            {
                foreach (AttackWeight weight in attackWeights)
                    if (!targetStateWeight.stateWeightList.Contains(weight))
                        targetStateWeight.stateWeightList.Add(weight);
                break;
            }
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
    
    protected virtual void ProcessCurrentGroupNodeWeight (int groupingNodeIndex, int phaseIndex)
    {
        var attackWeights = new List<AttackWeight>();

        foreach (var bossState in bossStates)
            attackWeights.Add(CreateWeight(bossState));

        foreach (var bossState in modifiedBossStates)
            attackWeights.Add(CreateWeight(bossState));
        
        var groupingNodes = groupingNodesRef.Invoke(associatedBossState);
        var firstGroupingNode = groupingNodes[groupingNodeIndex];
        var nodeQueue = queueRef.Invoke(firstGroupingNode);
        var currentWeight = nodeQueue.linkNextMoveStateWeights[phaseIndex];
        
        switch (WeightReplaceMode)
        {
            case WeightReplaceMode.Replace:
                currentWeight.stateWeightList = attackWeights;
                break;
            case WeightReplaceMode.Add:
            {
                foreach (AttackWeight weight in attackWeights)
                    if (!currentWeight.stateWeightList.Contains(weight))
                        currentWeight.stateWeightList.Add(weight);
                break;
            }
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
    
    protected void CreateStateWeight (string parentPath, string objClonePath, string name)
    {
        var cloneObj = GameObject.Find(objClonePath).GetComponent<LinkNextMoveStateWeight>();
        var parentPathObj = GameObject.Find(parentPath).transform;
        var createdObj = GameObject.Instantiate(cloneObj, parentPathObj);
        createdObj.name = name;
        targetStateWeight = createdObj;
        associatedBossState.linkNextMoveStateWeights.Add(createdObj);
    }
    
    protected void SetStateWeight (string path)
    {
        targetStateWeight = GameObject.Find(path).GetComponent<LinkNextMoveStateWeight>();
    }

    protected void SetAssociatedBossState (string path)
    {
        associatedBossState = GameObject.Find(path).GetComponent<BossGeneralState>();
    }

    protected void CreateAndAddBossState (string path)
    {
        BossGeneralState bossState = GameObject.Find(path).GetComponent<BossGeneralState>();
        bossStates.Add(bossState);
    }
    
    protected void CreateAndAddModifiedBossState (string path)
    {
        ModifiedBossGeneralState bossState = GameObject.Find(path).GetComponent<ModifiedBossGeneralState>();
        modifiedBossStates.Add(bossState);
        bossState.SubscribeSource(associatedBossState.name);
    }

    protected void ClearBossStates ()
    {
        bossStates.Clear();
        modifiedBossStates.Clear();
    }

    protected void SetWeightRandomness (bool isRandom)
    {
        targetStateWeight.IsRandom = isRandom;
    }

    protected AttackWeight CreateWeight (BossGeneralState bossState, int specifiedWeight = 1) =>
        new()
        {
            state = bossState,
            weight = specifiedWeight
        };
    
    protected AttackWeight CreateWeight (ModifiedBossGeneralState bossState, int specifiedWeight = 1) =>
        new()
        {
            state = bossState.BaseState,
            weight = specifiedWeight
        };
}