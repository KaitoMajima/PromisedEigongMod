using System;
using System.Collections.Generic;
using System.Linq;
using NineSolsAPI;

namespace PromisedEigong.WeightChanges;

using UnityEngine;

public abstract class BaseWeightChanger
{
    protected virtual WeightReplaceMode WeightReplaceMode => WeightReplaceMode.Replace;
    
    protected LinkNextMoveStateWeight targetStateWeight = new();
    protected List<BossGeneralState> bossStates = new();

    public virtual void ChangeAttackWeight ()
    {
        ProcessWeight();
    }

    protected virtual void ProcessWeight ()
    {
        var attackWeights = new List<AttackWeight>();
        
        foreach (var bossState in bossStates)
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

    protected void CreateAndAddBossState (string path)
    {
        BossGeneralState bossState = GameObject.Find(path).GetComponent<BossGeneralState>();
        bossStates.Add(bossState);
    }

    protected AttackWeight CreateWeight (BossGeneralState bossState, int specifiedWeight = 1) =>
        new()
        {
            state = bossState,
            weight = specifiedWeight
        };
}