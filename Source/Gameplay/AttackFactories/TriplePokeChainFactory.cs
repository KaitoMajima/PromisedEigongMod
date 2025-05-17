using System.Collections.Generic;
using UnityEngine;

namespace PromisedEigong.Gameplay.AttackFactories;

using static PromisedEigongModGlobalSettings.EigongAttacks;
using static PromisedEigongModGlobalSettings.EigongSpeed;

public class TriplePokeChainFactory : BaseAttackFactory
{
    public override string AttackToBeCopied => ATTACK13_TRIPLE_POKE;
    public override string AttackToBeCreated => ATTACK23_NEW_CHAIN_TRIPLE_POKE;

    public override void CopyAttack (BossGeneralState bossGeneralState)
    {
        var newAttack = SetupAttack(bossGeneralState);
        var attack1NextMove = GameObject.Find(ATTACK6_DOUBLE_ATTACK).GetComponent<BossGeneralState>();
       
        newAttack.AnimationSpeed = ATTACK23_NEW_CHAIN_TRIPLE_POKE_SPEED;
        newAttack.ForcePlayAnimAtNormalizeTime = ATTACK23_NEW_CHAIN_TRIPLE_POKE_SKIP;
        newAttack.IsFromAChain = true;
        
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
        
        newAttack.Phase1Weights = phase1Weights;
        newAttack.Phase2Weights = phase2Weights;
        newAttack.SubscribeSource(ATTACK22_NEW_CHAIN_SLOW_STARTER);
    }
}