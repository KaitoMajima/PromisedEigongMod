namespace PromisedEigong.SpeedChangers;
using static PromisedEigongModGlobalSettings.EigongAttacks;
using static PromisedEigongModGlobalSettings.EigongSpeed;

public class _3ThrustDelaySpeedChanger : BaseSpeedChanger
{
    public override bool IsSpecifiedAttack (string attack) 
        => attack == ATTACK3_THRUST_DELAY;

    public override float GetSpeed (BossGeneralState state)
        => ATTACK3_THRUST_DELAY_SPEED;
}