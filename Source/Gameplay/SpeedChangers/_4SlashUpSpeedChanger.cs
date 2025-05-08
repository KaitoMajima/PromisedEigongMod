namespace PromisedEigong.SpeedChangers;
using static PromisedEigongModGlobalSettings.EigongAttacks;
using static PromisedEigongModGlobalSettings.EigongSpeed;

public class _4SlashUpSpeedChanger : BaseSpeedChanger
{
    public override bool IsSpecifiedAttack (string attack) 
        => attack == ATTACK4_SLASH_UP;

    public override float GetSpeed (BossGeneralState state)
        => ATTACK4_SLASH_UP_SPEED;
}