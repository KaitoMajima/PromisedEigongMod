namespace PromisedEigong.SpeedChangers;
using static PromisedEigongModGlobalSettings.EigongAttacks;
using static PromisedEigongModGlobalSettings.EigongSpeed;

public class _14CrimsonBallSpeedChanger : BaseSpeedChanger
{
    public override bool IsSpecifiedAttack (string attack) 
        => attack == ATTACK14_CRIMSON_BALL;

    public override float GetSpeed (int phase) 
        => ATTACK14_CRIMSON_BALL_SPEED;
}