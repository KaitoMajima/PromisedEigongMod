namespace PromisedEigong.Gameplay.AttackFactories;

using static PromisedEigongModGlobalSettings.EigongAttacks;
using static PromisedEigongModGlobalSettings.EigongSpeed;

public class _21InstantCrimsonBallFactory : BaseAttackFactory
{
    public override string AttackToBeCopied => ATTACK14_CRIMSON_BALL;
    public override string AttackToBeCreated => ATTACK21_NEW_QUICK_CRIMSON_BALL;

    public override void CopyAttack (BossGeneralState bossGeneralState)
    {
        var newAttack = SetupAttack(bossGeneralState);
        newAttack.ForcePlayAnimAtNormalizeTime = ATTACK21_NEW_INSTANT_CRIMSON_BALL_SKIP;
        newAttack.AnimationSpeed = ATTACK21_NEW_INSTANT_CRIMSON_BALL_SPEED;
        newAttack.SubscribeSource(ATTACK28_NEW_CHAIN_TELEPORT_TO_BACK_COMBO);
    }
}