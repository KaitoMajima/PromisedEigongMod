namespace PromisedEigong.Gameplay.AttackFactories;

using static PromisedEigongModGlobalSettings.EigongAttacks;
using static PromisedEigongModGlobalSettings.EigongSpeed;

public class InstantCrimsonBallFactory : BaseAttackFactory
{
    public override string AttackToBeCopied => ATTACK14_CRIMSON_BALL;
    public override string AttackToBeCreated => ATTACK21_NEW_QUICK_CRIMSON_BALL;

    public override ModifiedBossGeneralState CopyAttack (BossGeneralState bossGeneralState)
    {
        var newAttack = base.CopyAttack(bossGeneralState);
        newAttack.ForcePlayAnimAtNormalizeTime = ATTACK21_NEW_INSTANT_CRIMSON_BALL_SKIP;
        newAttack.AnimationSpeed = ATTACK21_NEW_INSTANT_CRIMSON_BALL_SPEED;
        newAttack.SubscribeSource(ATTACK28_NEW_TELEPORT_TO_BACK_COMBO);
        return newAttack;
    }
}