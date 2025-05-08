namespace PromisedEigong.SpeedChangers;
using static PromisedEigongModGlobalSettings.EigongAttacks;
using static PromisedEigongModGlobalSettings.EigongSpeed;

public class _10FooSpeedChanger : BaseSpeedChanger
{
    public override bool IsSpecifiedAttack (string attack) 
        => attack == ATTACK10_FOO;

    public override float GetSpeed (BossGeneralState state)
        => ATTACK10_FOO_SPEED;
}