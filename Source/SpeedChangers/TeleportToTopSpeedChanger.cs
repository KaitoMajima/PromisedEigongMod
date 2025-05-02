namespace PromisedEigong.SpeedChangers;
using static PromisedEigongModGlobalSettings.EigongAttacks;
using static PromisedEigongModGlobalSettings.EigongSpeed;

public class TeleportToTopSpeedChanger : BaseSpeedChanger
{
    public override bool IsSpecifiedAttack (string attack) 
        => attack == ATTACK2_TELEPORT_TO_TOP;

    public override float GetSpeed (BossGeneralState state)
        => ATTACK2_TELEPORT_TO_TOP_SPEED;
}