namespace PromisedEigong.SpeedChangers;
using static PromisedEigongModGlobalSettings.EigongAttacks;
using static PromisedEigongModGlobalSettings.EigongSpeed;

public class _X2JumpBackSpeedChanger : BaseSpeedChanger
{
    public override bool IsSpecifiedAttack (string attack) 
        => attack == JUMP_BACK;

    public override float GetSpeed (BossGeneralState state)
        => JUMP_BACK_SPEED;
}