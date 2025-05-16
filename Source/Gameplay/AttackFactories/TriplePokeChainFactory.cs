using System.Collections.Generic;
using UnityEngine;

namespace PromisedEigong.Gameplay.AttackFactories;

using static PromisedEigongModGlobalSettings.EigongAttacks;
using static PromisedEigongModGlobalSettings.EigongSpeed;

public class TriplePokeChainFactory : BaseAttackFactory
{
    public override string AttackToBeCopied => ATTACK13_TRIPLE_POKE;
    public override string AttackToBeCreated => ATTACK24_NEW_CHAIN_TRIPLE_POKE;

    public override ModifiedBossGeneralState CopyAttack (BossGeneralState bossGeneralState)
    {
        var newAttack = base.CopyAttack(bossGeneralState);
        var attack1NextMove = GameObject.Find(ATTACK3_THRUST_DELAY).GetComponent<BossGeneralState>();
        var attack2NextMove = GameObject.Find(ATTACK16_QUICK_FOO).GetComponent<BossGeneralState>();
        var attack3NextMove = GameObject.Find(ATTACK12_SLASH_UP_CRIMSON).GetComponent<BossGeneralState>();
        newAttack.AnimationSpeed = ATTACK24_NEW_CHAIN_TRIPLE_POKE_SPEED;
        newAttack.ForcePlayAnimAtNormalizeTime = ATTACK24_NEW_CHAIN_TRIPLE_POKE_SKIP;
        
        var phase1Weights = new List<AttackWeight>
        {
            new()
            {
                state = attack1NextMove,
                weight = 7
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
                state = attack3NextMove,
                weight = 7
            }
        };
        
        newAttack.Phase1Weights = phase1Weights;
        newAttack.Phase2Weights = phase2Weights;
        newAttack.SubscribeSource(ATTACK22_NEW_CHAIN_SLOW_STARTER);
        return newAttack;
    }
}