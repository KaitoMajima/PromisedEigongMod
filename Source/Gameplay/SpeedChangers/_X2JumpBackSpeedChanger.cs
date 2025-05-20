using System;

namespace PromisedEigong.SpeedChangers;
using static PromisedEigongModGlobalSettings.EigongAttacks;
using static PromisedEigongModGlobalSettings.EigongSpeed;

public class _X2JumpBackSpeedChanger : BaseSpeedChanger
{
    public override bool IsSpecifiedAttack (string attack) 
        => attack == JUMP_BACK;

    public override float GetSpeed (int phase) 
    {
        return phase switch
        {
            0 or 1 => JUMP_BACK_SPEED,
            2 => JUMP_BACK_SPEED_PHASE_3,
            _ => throw new InvalidOperationException()
        };
    }
}