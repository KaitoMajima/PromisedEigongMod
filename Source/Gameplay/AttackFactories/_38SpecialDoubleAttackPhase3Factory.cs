using System.Collections.Generic;
using UnityEngine;

namespace PromisedEigong.Gameplay.AttackFactories;

using static PromisedEigongModGlobalSettings.EigongAttacks;
using static PromisedEigongModGlobalSettings.EigongSpeed;

public class _38SpecialDoubleAttackFactory : BaseAttackFactory
{
    public override string AttackToBeCopied => ATTACK6_DOUBLE_ATTACK;
    public override string AttackToBeCreated => ATTACK38_SPECIAL_DOUBLE_ATTACK;

    public override void CopyAttack (BossGeneralState bossGeneralState)
    {
        var newAttack = SetupAttack(bossGeneralState);
        var attack1NextMove = GameObject.Find(ATTACK18_TELEPORT_TO_BACK_COMBO).GetComponent<BossGeneralState>();
        var attack2NextMove = GameObject.Find(JUMP_BACK).GetComponent<BossGeneralState>();
        var attack3NextMove = GameObject.Find(ATTACK2_TELEPORT_TO_TOP).GetComponent<BossGeneralState>();
        newAttack.AnimationSpeed = ATTACK6_DOUBLE_ATTACK_SPEED;
        newAttack.ShouldClearGroupingNodes = true;
        newAttack.PhaseIndexLock = 2;
        
        var phase2Weights = new List<AttackWeight>
        {
            new()
            {
                state = attack1NextMove,
                weight = 7
            },
            new()
            {
                state = attack2NextMove,
                weight = 3
            },
            new()
            {
                state = attack3NextMove,
                weight = 3
            }
        };
        
        newAttack.Phase2Weights = phase2Weights;
        newAttack.SubscribeSource(ATTACK39_SPECIAL_DOUBLE_ATTACK_JUMP_BACK);
    }
}