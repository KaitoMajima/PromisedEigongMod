namespace PromisedEigong.SpeedChangers;
using static PromisedEigongModGlobalSettings.EigongAttacks;
using static PromisedEigongModGlobalSettings.EigongSpeed;

public class QuickFooSpeedChanger : BaseSpeedChanger
{
    public override bool IsSpecifiedAttack (string attack) 
        => attack == ATTACK16_QUICK_FOO;

    public override float GetSpeed (BossGeneralState state)
        => ATTACK16_QUICK_FOO_SPEED;
}