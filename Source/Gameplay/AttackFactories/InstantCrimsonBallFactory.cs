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
        var newAttack = newAttackObj.AddComponent<ModifiedBossGeneralState>();
        newAttack.Setup(attackToBeCreated, bossGeneralState, 1);
        newAttack.name = attackToBeCreated;
        newAttack.ModifiedName = attackToBeCreated;
        newAttack.ForcePlayAnimAtNormalizeTime = ATTACK21_NEW_INSTANT_CRIMSON_BALL_SKIP;
        newAttack.AnimationSpeed = ATTACK21_NEW_INSTANT_CRIMSON_BALL_SPEED;
    }
}