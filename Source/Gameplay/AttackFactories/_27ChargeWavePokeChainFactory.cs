﻿using System.Collections.Generic;
using UnityEngine;

namespace PromisedEigong.Gameplay.AttackFactories;

using static PromisedEigongModGlobalSettings.EigongAttacks;
using static PromisedEigongModGlobalSettings.EigongSpeed;

public class _27ChargeWavePokeChainFactory : BaseAttackFactory
{
    public override string AttackToBeCopied => ATTACK11_GIANT_CHARGE_WAVE;
    public override string AttackToBeCreated => ATTACK27_NEW_CHAIN_CHARGE_WAVE;

    public override void CopyAttack (BossGeneralState bossGeneralState)
    {
        var newAttack = SetupAttack(bossGeneralState);
        var attack1NextMove = GameObject.Find(ATTACK3_THRUST_DELAY).GetComponent<BossGeneralState>();
        var attack2NextMove = GameObject.Find(ATTACK18_TELEPORT_TO_BACK_COMBO).GetComponent<BossGeneralState>();
        var attack3NextMove = GameObject.Find(ATTACK2_TELEPORT_TO_TOP).GetComponent<BossGeneralState>();
        var attack4NextMove = GameObject.Find(ATTACK5_TELEPORT_TO_BACK).GetComponent<BossGeneralState>();
        
        newAttack.AnimationSpeed = ATTACK11_GIANT_CHARGE_WAVE_SPEED;
        newAttack.ForcePlayAnimAtNormalizeTime = ATTACK27_NEW_CHAIN_CHARGE_WAVE_SKIP;
        newAttack.IsFromAChain = true;
        
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
                state = attack2NextMove,
                weight = 3
            },
            new()
            {
                state = attack3NextMove,
                weight = 6
            },
            new()
            {
                state = attack4NextMove,
                weight = 5
            }
        };
        
        newAttack.Phase2Weights = phase2Weights;
        newAttack.Phase3Weights = phase3Weights;
        newAttack.SubscribeSource(ATTACK26_NEW_CHAIN_TELEPORT_TO_BACK_SECOND);
    }
}