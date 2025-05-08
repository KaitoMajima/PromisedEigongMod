namespace PromisedEigong.SpeedChangers;
using static PromisedEigongModGlobalSettings.EigongAttacks;
using static PromisedEigongModGlobalSettings.EigongSpeed;

public class _18TeleportToBackComboSpeedChanger : BaseSpeedChanger
{
    public override bool IsSpecifiedAttack (string attack) 
        => attack == ATTACK18_TELEPORT_TO_BACK_COMBO;

    public override float GetSpeed (BossGeneralState state)
        => ATTACK18_TELEPORT_TO_BACK_COMBO_SPEED;
}