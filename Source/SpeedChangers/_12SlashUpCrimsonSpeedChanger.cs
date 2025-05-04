namespace PromisedEigong.SpeedChangers;
using static PromisedEigongModGlobalSettings.EigongAttacks;
using static PromisedEigongModGlobalSettings.EigongSpeed;

public class _12SlashUpCrimsonSpeedChanger : BaseSpeedChanger
{
    public override bool IsSpecifiedAttack (string attack) 
        => attack == ATTACK12_SLASH_UP_CRIMSON;

    public override float GetSpeed (BossGeneralState state)
        => ATTACK12_SLASH_UP_CRIMSON_SPEED;
}