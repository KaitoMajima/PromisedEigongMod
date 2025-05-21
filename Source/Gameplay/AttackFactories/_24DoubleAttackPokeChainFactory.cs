using System.Collections.Generic;
using UnityEngine;

namespace PromisedEigong.Gameplay.AttackFactories;

using static PromisedEigongModGlobalSettings.EigongAttacks;
using static PromisedEigongModGlobalSettings.EigongSpeed;

public class _24DoubleAttackPokeChainFactory : BaseAttackFactory
{
    public override string AttackToBeCopied => ATTACK6_DOUBLE_ATTACK;
    public override string AttackToBeCreated => ATTACK24_NEW_CHAIN_DOUBLE_ATTACK;

    public override void CopyAttack (BossGeneralState bossGeneralState)
    {
        var newAttack = SetupAttack(bossGeneralState);
        var attack1NextMove = GameObject.Find(ATTACK3_THRUST_DELAY).GetComponent<BossGeneralState>();
        var attack2NextMove = GameObject.Find(ATTACK5_TELEPORT_TO_BACK).GetComponent<BossGeneralState>();
        newAttack.AnimationSpeed = ATTACK24_NEW_CHAIN_DOUBLE_ATTACK_SPEED;
        newAttack.ShouldClearGroupingNodes = true;
        newAttack.IsFromAChain = true;
        
        var phase1Weights = new List<AttackWeight>
        {
            new()
            {
                state = attack1NextMove,
                weight = 3
            },
            new()
            {
                state = attack2NextMove,
                weight = 7
            }
        };
        
        var phase2Weights = new List<AttackWeight>
        {
            new()
            {
                state = attack2NextMove,
                weight = 7
            }
        };
        
        newAttack.Phase1Weights = phase1Weights;
        newAttack.Phase2Weights = phase2Weights;
        newAttack.SubscribeSource(ATTACK23_NEW_CHAIN_TRIPLE_POKE);
    }
}