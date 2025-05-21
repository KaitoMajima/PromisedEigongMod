using System;

namespace PromisedEigong.SpeedChangers;
using static PromisedEigongModGlobalSettings.EigongAttacks;
using static PromisedEigongModGlobalSettings.EigongSpeed;

public class _X3AttackParryingSpeedChanger : BaseSpeedChanger
{
    public override bool ShouldChangeClearEnterSpeedValue => true;
    public override bool ClearEnterSpeedValue => false;
    
    public override bool ShouldChangeClearExitSpeedValue => true;
    public override bool ClearExitSpeedValue => true;
    
    public override bool IsSpecifiedAttack (string attack) 
        => attack == ATTACK_PARRYING;

    public override float GetSpeed (int phase) 
    {
        return phase switch
        {
            0 or 1 => ATTACK_PARRYING_SPEED,
            2 => ATTACK_PARRYING_SPEED_PHASE_3,
            _ => throw new InvalidOperationException()
        };
    }
}