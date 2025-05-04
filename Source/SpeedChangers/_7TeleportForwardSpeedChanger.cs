namespace PromisedEigong.SpeedChangers;
using static PromisedEigongModGlobalSettings.EigongAttacks;
using static PromisedEigongModGlobalSettings.EigongSpeed;

public class _7TeleportForwardSpeedChanger : BaseSpeedChanger
{
    public override bool IsSpecifiedAttack (string attack) 
        => attack == ATTACK7_TELEPORT_FORWARD;

    public override float GetSpeed (BossGeneralState state)
        => ATTACK7_TELEPORT_FORWARD_SPEED;
}