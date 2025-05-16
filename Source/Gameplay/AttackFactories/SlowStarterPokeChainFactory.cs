using System.Collections.Generic;
using UnityEngine;

namespace PromisedEigong.Gameplay.AttackFactories;

using static PromisedEigongModGlobalSettings.EigongAttacks;
using static PromisedEigongModGlobalSettings.EigongSpeed;

public class SlowStarterPokeChainFactory : BaseAttackFactory
{
    public override string AttackToBeCopied => ATTACK1_SLOW_STARTER;
    public override string AttackToBeCreated => ATTACK22_NEW_CHAIN_SLOW_STARTER;

    public override ModifiedBossGeneralState CopyAttack (BossGeneralState bossGeneralState)
    {
        var newAttack = base.CopyAttack(bossGeneralState);
        var attack1NextMove = GameObject.Find(ATTACK13_TRIPLE_POKE).GetComponent<BossGeneralState>();
        newAttack.AnimationSpeed = ATTACK22_NEW_CHAIN_SLOW_STARTER_SPEED;
        
        var phase1Weights = new List<AttackWeight>
        {
            new()
            {
                state = attack1NextMove,
                weight = 7
            }
        };
        
        var phase2Weights = new List<AttackWeight>
        {
            new()
            {
                state = attack1NextMove,
                weight = 7
            }
        };
        
        var phase3Weights = new List<AttackWeight>
        {
            new()
            {
                state = attack1NextMove,
                weight = 7
            }
        };
        
        newAttack.Phase1Weights = phase1Weights;
        newAttack.Phase2Weights = phase2Weights;
        newAttack.Phase3Weights = phase3Weights;
        return newAttack;
    }
}