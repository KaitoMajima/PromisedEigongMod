namespace PromisedEigong.SpeedChangers;
using static PromisedEigongModGlobalSettings.EigongAttacks;
using static PromisedEigongModGlobalSettings.EigongSpeed;

public class _6DoubleAttackSpeedChanger : BaseSpeedChanger
{
    public override bool IsSpecifiedAttack (string attack) 
        => attack == ATTACK6_DOUBLE_ATTACK;

    public override float GetSpeed ( BossGeneralState state)
        => ATTACK6_DOUBLE_ATTACK_SPEED;
}