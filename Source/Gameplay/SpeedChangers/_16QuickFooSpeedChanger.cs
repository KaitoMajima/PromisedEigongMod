namespace PromisedEigong.SpeedChangers;
using static PromisedEigongModGlobalSettings.EigongAttacks;
using static PromisedEigongModGlobalSettings.EigongSpeed;

public class _16QuickFooSpeedChanger : BaseSpeedChanger
{
    public override bool IsSpecifiedAttack (string attack) 
        => attack == ATTACK16_QUICK_FOO;

    public override float GetSpeed (int phase) 
        => ATTACK16_QUICK_FOO_SPEED;
}