namespace PromisedEigong.SpeedChangers;
using static PromisedEigongModGlobalSettings.EigongAttacks;
using static PromisedEigongModGlobalSettings.EigongSpeed;

public class _8LongChargeSpeedChanger : BaseSpeedChanger
{
    public override bool IsSpecifiedAttack (string attack) 
        => attack == ATTACK8_LONG_CHARGE;

    public override float GetSpeed (int phase) 
        => ATTACK8_LONG_CHARGE_SPEED;
}