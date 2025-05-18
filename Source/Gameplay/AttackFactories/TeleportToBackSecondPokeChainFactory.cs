using System.Collections.Generic;
using UnityEngine;

namespace PromisedEigong.Gameplay.AttackFactories;

using static PromisedEigongModGlobalSettings.EigongAttacks;
using static PromisedEigongModGlobalSettings.EigongSpeed;

public class TeleportToBackSecondPokeChainFactory : BaseAttackFactory
{
    public override string AttackToBeCopied => ATTACK5_TELEPORT_TO_BACK;
    public override string AttackToBeCreated => ATTACK26_NEW_CHAIN_TELEPORT_TO_BACK_SECOND;

    public override void CopyAttack (BossGeneralState bossGeneralState)
    {
        var newAttack = SetupAttack(bossGeneralState);
        var attack1NextMove = GameObject.Find(ATTACK11_GIANT_CHARGE_WAVE).GetComponent<BossGeneralState>();
        newAttack.AnimationSpeed = ATTACK26_TELEPORT_TO_BACK_SPEED;
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
        newAttack.SubscribeSource(ATTACK25_NEW_CHAIN_SLASH_UP_CRIMSON);
    }
}