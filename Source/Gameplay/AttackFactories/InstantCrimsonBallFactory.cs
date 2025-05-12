using UnityEngine;

namespace PromisedEigong.Gameplay.AttackFactories;

using static PromisedEigongModGlobalSettings.EigongAttacks;
using static PromisedEigongModGlobalSettings.EigongSpeed;

public class InstantCrimsonBallFactory
{
    public string attackToBeCopied = ATTACK14_CRIMSON_BALL;
    public string attackToBeCreated = ATTACK21_NEW_QUICK_CRIMSON_BALL;

    public void CopyAttack (BossGeneralState bossGeneralState)
    {
        var attacksParent = GameObject.Find(ATTACK_PATH);
        var newAttackObj = Object.Instantiate(new GameObject(), attacksParent.transform);
        ModifiedBossGeneralState newAttack = newAttackObj.AddComponent<ModifiedBossGeneralState>();
        newAttack.name = attackToBeCreated;
        newAttack.Setup(attackToBeCreated, bossGeneralState, 1);
        newAttack.ForcePlayAnimAtNormalizeTime = ATTACK21_NEW_INSTANT_CRIMSON_BALL_SKIP;
        newAttack.AnimationSpeed = ATTACK21_NEW_INSTANT_CRIMSON_BALL_SPEED;
    }
}