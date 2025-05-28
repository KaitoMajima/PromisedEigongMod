using System.Collections.Generic;
using UnityEngine;

namespace PromisedEigong.Gameplay.AttackFactories;

using static PromisedEigongModGlobalSettings.EigongAttacks;
using static PromisedEigongModGlobalSettings.EigongSpeed;

public class _29TeleportToBackFirstPokeChainFactory : BaseAttackFactory
{
    public override string AttackToBeCopied => ATTACK5_TELEPORT_TO_BACK;
    public override string AttackToBeCreated => ATTACK29_NEW_CHAIN_TELEPORT_TO_BACK_FIRST;

    public override void CopyAttack (BossGeneralState bossGeneralState)
    {
        var newAttack = SetupAttack(bossGeneralState);
        var attack1NextMove = GameObject.Find(ATTACK16_QUICK_FOO).GetComponent<BossGeneralState>();
        var attack2NextMove = GameObject.Find(ATTACK12_SLASH_UP_CRIMSON).GetComponent<BossGeneralState>();
        newAttack.AnimationSpeed = ATTACK5_TELEPORT_TO_BACK_SPEED;
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
                weight = 3
            },
            new()
            {
                state = attack2NextMove,
                weight = 7
            }
        };
        
        var phase3Weights = new List<AttackWeight>
        {
            new()
            {
                state = attack1NextMove,
                weight = 1
            },
            new()
            {
                state = attack2NextMove,
                weight = 7
            }
        };
        
        newAttack.Phase1Weights = phase1Weights;
        newAttack.Phase2Weights = phase2Weights;
        newAttack.Phase3Weights = phase3Weights;
        newAttack.SubscribeSource(ATTACK24_NEW_CHAIN_DOUBLE_ATTACK);
    }
}