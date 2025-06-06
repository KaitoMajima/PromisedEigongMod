﻿namespace PromisedEigong.SpeedChangers;
using static PromisedEigongModGlobalSettings.EigongAttacks;
using static PromisedEigongModGlobalSettings.EigongSpeed;

public class _11ChargeWaveSpeedChanger : BaseSpeedChanger
{
    public override bool IsSpecifiedAttack (string attack) 
        => attack == ATTACK11_GIANT_CHARGE_WAVE;

    public override float GetSpeed (int phase) 
        => ATTACK11_GIANT_CHARGE_WAVE_SPEED;
}