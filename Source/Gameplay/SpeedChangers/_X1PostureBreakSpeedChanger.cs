using System;

namespace PromisedEigong.SpeedChangers;
using static PromisedEigongModGlobalSettings.EigongAttacks;
using static PromisedEigongModGlobalSettings.EigongSpeed;

public class _X1PostureBreakSpeedChanger : BaseSpeedChanger
{
    public override bool ShouldChangeClearEnterSpeedValue => true;
    public override bool ClearEnterSpeedValue => false;
    
    public override bool ShouldChangeClearExitSpeedValue => true;
    public override bool ClearExitSpeedValue => true;
    
    public override bool IsSpecifiedAttack (string attack) 
        => attack == POSTURE_BREAK;

    public override float GetSpeed (int phase) 
    {
        return phase switch
        {
            0 or 1 => POSTURE_BREAK_SPEED,
            2 => POSTURE_BREAK_SPEED_PHASE_3,
            _ => throw new InvalidOperationException()
        };
    }
}