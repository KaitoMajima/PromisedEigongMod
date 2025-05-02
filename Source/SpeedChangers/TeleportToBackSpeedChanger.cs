namespace PromisedEigong.SpeedChangers;
using static PromisedEigongModGlobalSettings.EigongAttacks;
using static PromisedEigongModGlobalSettings.EigongSpeed;

public class TeleportToBackSpeedChanger : BaseSpeedChanger
{
    public override bool IsSpecifiedAttack (string attack) 
        => attack == ATTACK5_TELEPORT_TO_BACK;

    public override float GetSpeed (BossGeneralState state)
        => ATTACK5_TELEPORT_TO_BACK_SPEED;
}