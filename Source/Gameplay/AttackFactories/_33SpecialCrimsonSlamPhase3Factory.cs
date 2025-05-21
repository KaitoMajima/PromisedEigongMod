using System.Collections.Generic;
using UnityEngine;

namespace PromisedEigong.Gameplay.AttackFactories;

using static PromisedEigongModGlobalSettings.EigongAttacks;
using static PromisedEigongModGlobalSettings.EigongSpeed;

public class _33SpecialCrimsonSlamPhase3Factory : BaseAttackFactory
{
    public override string AttackToBeCopied => ATTACK17_CRIMSON_SLAM;
    public override string AttackToBeCreated => ATTACK33_NEW_CRIMSON_SLAM_SPECIAL;

    public override void CopyAttack (BossGeneralState bossGeneralState)
    {
        var newAttack = SetupAttack(bossGeneralState);
        var attack1NextMove = GameObject.Find(ATTACK5_TELEPORT_TO_BACK).GetComponent<BossGeneralState>();
        var attack2NextMove = GameObject.Find(ATTACK4_SLASH_UP).GetComponent<BossGeneralState>();
        var attack3NextMove = GameObject.Find(ATTACK12_SLASH_UP_CRIMSON).GetComponent<BossGeneralState>();
        var attack4NextMove = GameObject.Find(JUMP_BACK).GetComponent<BossGeneralState>();
        var attack5NextMove = GameObject.Find(ATTACK8_LONG_CHARGE).GetComponent<BossGeneralState>();
        var attack6NextMove = GameObject.Find(ATTACK9_STARTER).GetComponent<BossGeneralState>();

        newAttack.AnimationSpeed = ATTACK17_CRIMSON_SLAM_SPEED;
        newAttack.StartOffset = ATTACK33_NEW_CRIMSON_SLAM_OFFSET;
        newAttack.PhaseIndexLock = 2;
        
        var phase3Weights = new List<AttackWeight>
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
            },
            new()
            {
                state = attack5NextMove,
                weight = 7
            },
            new()
            {
                state = attack6NextMove,
                weight = 7
            }
        };
        
        newAttack.Phase3Weights = phase3Weights;
        newAttack.SubscribeSource(ATTACK32_NEW_TELEPORT_TO_BACK_TO_CS);
    }
}