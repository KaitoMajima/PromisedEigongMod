using System.Collections.Generic;
using UnityEngine;

namespace PromisedEigong.Gameplay.AttackFactories;

using static PromisedEigongModGlobalSettings.EigongAttacks;
using static PromisedEigongModGlobalSettings.EigongSpeed;

public class TeleportToBackComboPokeChainFactory : BaseAttackFactory
{
    public override string AttackToBeCopied => ATTACK18_TELEPORT_TO_BACK_COMBO;
    public override string AttackToBeCreated => ATTACK28_NEW_CHAIN_TELEPORT_TO_BACK_COMBO;

    public override void CopyAttack (BossGeneralState bossGeneralState)
    {
        var newAttack = SetupAttack(bossGeneralState);
        var attack1NextMove = GameObject.Find(ATTACK14_CRIMSON_BALL).GetComponent<BossGeneralState>();
        var attack2NextMove = GameObject.Find(ATTACK8_LONG_CHARGE).GetComponent<BossGeneralState>();
        var attack3NextMove = GameObject.Find(ATTACK10_FOO).GetComponent<BossGeneralState>();
        var attack4NextMove = GameObject.Find(ATTACK9_STARTER).GetComponent<BossGeneralState>();

        newAttack.AnimationSpeed = ATTACK5_TELEPORT_TO_BACK_SPEED;
        newAttack.IsFromAChain = true;
        
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
            },
            new()
            {
                state = attack3NextMove,
                weight = 7
            },
            new()
            {
                state = attack4NextMove,
                weight = 7
            }
        };
        
        newAttack.Phase1Weights = phase1Weights;
        newAttack.SubscribeSource(ATTACK27_NEW_CHAIN_CHARGE_WAVE);
    }
}