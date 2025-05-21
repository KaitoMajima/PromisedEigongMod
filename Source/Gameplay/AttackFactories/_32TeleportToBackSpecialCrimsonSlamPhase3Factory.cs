using System.Collections.Generic;
using UnityEngine;

namespace PromisedEigong.Gameplay.AttackFactories;

using static PromisedEigongModGlobalSettings.EigongAttacks;
using static PromisedEigongModGlobalSettings.EigongSpeed;

public class _32TeleportToBackSpecialCrimsonSlamPhase3Factory : BaseAttackFactory
{
    public override string AttackToBeCopied => ATTACK5_TELEPORT_TO_BACK;
    public override string AttackToBeCreated => ATTACK32_NEW_TELEPORT_TO_BACK_TO_CS;

    public override void CopyAttack (BossGeneralState bossGeneralState)
    {
        var newAttack = SetupAttack(bossGeneralState);
        var attack1NextMove = GameObject.Find(ATTACK17_CRIMSON_SLAM).GetComponent<BossGeneralState>();

        newAttack.ForcePlayAnimAtNormalizeTime = ATTACK32_NEW_CHAIN_TELEPORT_TO_BACK_TO_CS_SKIP;
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
    }
}