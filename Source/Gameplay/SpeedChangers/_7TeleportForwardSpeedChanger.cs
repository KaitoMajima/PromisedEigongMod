using System;

namespace PromisedEigong.SpeedChangers;
using static PromisedEigongModGlobalSettings.EigongAttacks;
using static PromisedEigongModGlobalSettings.EigongSpeed;

public class _7TeleportForwardSpeedChanger : BaseSpeedChanger
{
    public override bool IsSpecifiedAttack (string attack) 
        => attack == ATTACK7_TELEPORT_FORWARD;

    public override float GetSpeed (int phase) 
    {
        return phase switch
        {
            0 or 1 => ATTACK7_TELEPORT_FORWARD_SPEED,
            2 => ATTACK7_TELEPORT_FORWARD_SPEED_PHASE_3,
            _ => throw new InvalidOperationException()
        };
    }
}