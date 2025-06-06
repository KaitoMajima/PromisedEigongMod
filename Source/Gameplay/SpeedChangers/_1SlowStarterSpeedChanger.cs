﻿namespace PromisedEigong.SpeedChangers;
using static PromisedEigongModGlobalSettings.EigongAttacks;
using static PromisedEigongModGlobalSettings.EigongSpeed;

public class _1SlowStarterSpeedChanger : BaseSpeedChanger
{
    public override bool IsSpecifiedAttack (string attack) 
        => attack == ATTACK1_SLOW_STARTER;

    public override float GetSpeed (int phase) 
        => ATTACK1_SLOW_STARTER_SPEED;
}