using System.Collections.Generic;
using UnityEngine;

namespace PromisedEigong.Gameplay.AttackFactories;

using static PromisedEigongModGlobalSettings.EigongAttacks;
using static PromisedEigongModGlobalSettings.EigongSpeed;

public class _35JumpBackSafeguardPhase3Factory : BaseAttackFactory
{
    public override string AttackToBeCopied => JUMP_BACK;
    public override string AttackToBeCreated => ATTACK35_NEW_SAFEGUARD_JUMP_BACK;

    public override void CopyAttack (BossGeneralState bossGeneralState)
    {
        var newAttack = SetupAttack(bossGeneralState, true);
        var attack1NextMove = GameObject.Find(ATTACK1_SLOW_STARTER).GetComponent<BossGeneralState>();

        newAttack.AnimationSpeed = JUMP_BACK_SPEED;
        newAttack.PhaseIndexLock = 2;
        
        var phase3Weights = new List<AttackWeight>
        {
            new()
            {
                state = attack1NextMove,
                weight = 7
            }
        };
        
        newAttack.Phase3Weights = phase3Weights;
        newAttack.SubscribeSource(ATTACK4_SLASH_UP);
    }
}