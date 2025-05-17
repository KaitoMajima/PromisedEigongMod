using System.Collections.Generic;
using UnityEngine;

namespace PromisedEigong.Gameplay.AttackFactories;

using static PromisedEigongModGlobalSettings.EigongAttacks;
using static PromisedEigongModGlobalSettings.EigongSpeed;

public class SlashUpCrimsonPokeChainFactory : BaseAttackFactory
{
    public override string AttackToBeCopied => ATTACK12_SLASH_UP_CRIMSON;
    public override string AttackToBeCreated => ATTACK25_NEW_CHAIN_SLASH_UP_CRIMSON;

    public override ModifiedBossGeneralState CopyAttack (BossGeneralState bossGeneralState)
    {
        var newAttack = base.CopyAttack(bossGeneralState);
        var attack1NextMove = GameObject.Find(ATTACK5_TELEPORT_TO_BACK).GetComponent<BossGeneralState>();
        newAttack.AnimationSpeed = ATTACK12_SLASH_UP_CRIMSON_SPEED;
        newAttack.IsFromAChain = true;
        
        var phase2Weights = new List<AttackWeight>
        {
            new()
            {
                state = attack1NextMove,
                weight = 7
            }
        };
        
        newAttack.Phase2Weights = phase2Weights;
        newAttack.SubscribeSource(ATTACK29_NEW_CHAIN_TELEPORT_TO_BACK_FIRST);
        return newAttack;
    }
}