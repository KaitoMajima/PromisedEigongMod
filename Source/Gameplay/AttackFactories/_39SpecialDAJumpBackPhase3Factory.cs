using System.Collections.Generic;
using UnityEngine;

namespace PromisedEigong.Gameplay.AttackFactories;

using static PromisedEigongModGlobalSettings.EigongAttacks;
using static PromisedEigongModGlobalSettings.EigongSpeed;

public class _39SpecialDAJumpBackPhase3Factory : BaseAttackFactory
{
    public override string AttackToBeCopied => JUMP_BACK;
    public override string AttackToBeCreated => ATTACK39_SPECIAL_DOUBLE_ATTACK_JUMP_BACK;

    public override void CopyAttack (BossGeneralState bossGeneralState)
    {
        var newAttack = SetupAttack(bossGeneralState, true);
        var attack1NextMove = GameObject.Find(ATTACK6_DOUBLE_ATTACK).GetComponent<BossGeneralState>();
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
        newAttack.SubscribeSource(ATTACK38_SPECIAL_DOUBLE_ATTACK);
    }
}