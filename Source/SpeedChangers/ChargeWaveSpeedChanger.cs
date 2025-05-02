namespace PromisedEigong.SpeedChangers;
using static PromisedEigongModGlobalSettings.EigongAttacks;
using static PromisedEigongModGlobalSettings.EigongSpeed;

public class ChargeWaveSpeedChanger : BaseSpeedChanger
{
    public override bool IsSpecifiedAttack (string attack) 
        => attack == ATTACK11_GIANT_CHARGE_WAVE;

    public override float GetSpeed (BossGeneralState state) 
        => ATTACK11_GIANT_CHARGE_WAVE_SPEED;
}