using System;

namespace PromisedEigong.SpeedChangers;
using static PromisedEigongModGlobalSettings.EigongAttacks;
using static PromisedEigongModGlobalSettings.EigongSpeed;

public class _5TeleportToBackSpeedChanger : BaseSpeedChanger
{
    public override bool IsSpecifiedAttack (string attack) 
        => attack == ATTACK5_TELEPORT_TO_BACK;

    public override float GetSpeed (int phase) 
    {
        return phase switch
        {
            0 or 1 => ATTACK5_TELEPORT_TO_BACK_SPEED,
            2 => ATTACK5_TELEPORT_TO_BACK_SPEED_PHASE_3,
            _ => throw new InvalidOperationException()
        };
    }
}