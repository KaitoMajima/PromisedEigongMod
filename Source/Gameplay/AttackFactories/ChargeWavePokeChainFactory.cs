using System.Collections.Generic;
using UnityEngine;

namespace PromisedEigong.Gameplay.AttackFactories;

using static PromisedEigongModGlobalSettings.EigongAttacks;
using static PromisedEigongModGlobalSettings.EigongSpeed;

public class ChargeWavePokeChainFactory : BaseAttackFactory
{
    public override string AttackToBeCopied => ATTACK11_GIANT_CHARGE_WAVE;
    public override string AttackToBeCreated => ATTACK27_NEW_CHAIN_CHARGE_WAVE;

    public override ModifiedBossGeneralState CopyAttack (BossGeneralState bossGeneralState)
    {
        var newAttack = base.CopyAttack(bossGeneralState);
        var attack1NextMove = GameObject.Find(ATTACK3_THRUST_DELAY).GetComponent<BossGeneralState>();
        var attack2NextMove = GameObject.Find(ATTACK18_TELEPORT_TO_BACK_COMBO).GetComponent<BossGeneralState>();
        newAttack.AnimationSpeed = ATTACK11_GIANT_CHARGE_WAVE_SPEED;
        newAttack.ForcePlayAnimAtNormalizeTime = ATTACK27_NEW_CHAIN_CHARGE_WAVE_SKIP;
        
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
                weight = 7
            }
        };
        
        newAttack.Phase2Weights = phase2Weights;
        newAttack.SubscribeSource(ATTACK26_NEW_CHAIN_TELEPORT_TO_BACK);
        return newAttack;
    }
}