namespace PromisedEigong.WeightChanges;

using System;
using System.Collections.Generic;
using Gameplay.AttackFactories;

using UnityEngine;

public abstract class BaseWeightChanger
{
    protected virtual WeightReplaceMode WeightReplaceMode { get; set; } = WeightReplaceMode.Replace;

    protected BossGeneralState associatedBossState;
    protected LinkNextMoveStateWeight targetStateWeight = new();
    protected List<BossGeneralState> bossStates = new();
    protected List<ModifiedBossGeneralState> modifiedBossStates = new();

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
        bossState.SubscribeSource(associatedBossState);
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