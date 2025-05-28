using System.Collections.Generic;
using UnityEngine;

namespace PromisedEigong.Gameplay.AttackFactories;

using static PromisedEigongModGlobalSettings.EigongAttacks;
using static PromisedEigongModGlobalSettings.EigongSpeed;

public class _40TeleportToBackComboPhase3Changer : BaseAttackFactory
{
    public override string AttackToBeCopied => ATTACK18_TELEPORT_TO_BACK_COMBO;
    public override string AttackToBeCreated => ATTACK40_TELEPORT_TO_BACK_COMBO_PHASE_3;

    public override void CopyAttack (BossGeneralState bossGeneralState)
    {
        var newAttack = SetupAttack(bossGeneralState);
        var attack1NextMove = GameObject.Find(ATTACK14_CRIMSON_BALL).GetComponent<BossGeneralState>();
        var attack2NextMove = GameObject.Find(ATTACK8_LONG_CHARGE).GetComponent<BossGeneralState>();
        var attack3NextMove = GameObject.Find(ATTACK10_FOO).GetComponent<BossGeneralState>();
        var attack4NextMove = GameObject.Find(ATTACK9_STARTER).GetComponent<BossGeneralState>();

        newAttack.AnimationSpeed = ATTACK5_TELEPORT_TO_BACK_SPEED;
        newAttack.PhaseIndexLock = 2;
        
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
                weight = 2
            },
            new()
            {
                state = attack3NextMove,
                weight = 2
            },
            new()
            {
                state = attack4NextMove,
                weight = 4
            }
        };
        
        newAttack.Phase1Weights = phase1Weights;
    }
}