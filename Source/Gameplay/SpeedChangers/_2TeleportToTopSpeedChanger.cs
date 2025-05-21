using System;

namespace PromisedEigong.SpeedChangers;
using static PromisedEigongModGlobalSettings.EigongAttacks;
using static PromisedEigongModGlobalSettings.EigongSpeed;

public class _2TeleportToTopSpeedChanger : BaseSpeedChanger
{
    public override bool IsSpecifiedAttack (string attack) 
        => attack == ATTACK2_TELEPORT_TO_TOP;

    public override float GetSpeed (int phase)
    {
        return phase switch
        {
            0 or 1 => ATTACK2_TELEPORT_TO_TOP_SPEED,
            2 => ATTACK2_TELEPORT_TO_TOP_SPEED,
            _ => throw new InvalidOperationException()
        };
    }
}