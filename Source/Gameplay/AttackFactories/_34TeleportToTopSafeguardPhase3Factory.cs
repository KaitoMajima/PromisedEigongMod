using System.Collections.Generic;
using UnityEngine;

namespace PromisedEigong.Gameplay.AttackFactories;

using static PromisedEigongModGlobalSettings.EigongAttacks;
using static PromisedEigongModGlobalSettings.EigongSpeed;

public class _34TeleportToTopSafeguardPhase3Factory : BaseAttackFactory
{
    public override string AttackToBeCopied => ATTACK2_TELEPORT_TO_TOP;
    public override string AttackToBeCreated => ATTACK34_NEW_SAFEGUARD_TELEPORT_TO_TOP;

    public override void CopyAttack (BossGeneralState bossGeneralState)
    {
        var newAttack = SetupAttack(bossGeneralState);
        var attack1NextMove = GameObject.Find(ATTACK20_TELEPORT_OUT).GetComponent<BossGeneralState>();

        newAttack.AnimationSpeed = ATTACK2_TELEPORT_TO_TOP_SPEED_PHASE_3;
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