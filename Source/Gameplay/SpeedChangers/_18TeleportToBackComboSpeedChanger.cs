using System;

namespace PromisedEigong.SpeedChangers;
using static PromisedEigongModGlobalSettings.EigongAttacks;
using static PromisedEigongModGlobalSettings.EigongSpeed;

public class _18TeleportToBackComboSpeedChanger : BaseSpeedChanger
{
    public override bool IsSpecifiedAttack (string attack) 
        => attack == ATTACK18_TELEPORT_TO_BACK_COMBO;

    public override float GetSpeed (int phase) 
    {
        return phase switch
        {
            0 or 1 => ATTACK18_TELEPORT_TO_BACK_COMBO_SPEED,
            2 => ATTACK18_TELEPORT_TO_BACK_COMBO_SPEED_PHASE_3,
            _ => throw new InvalidOperationException()
        };
    }
}