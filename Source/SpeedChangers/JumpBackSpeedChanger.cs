namespace PromisedEigong.SpeedChangers;
using static PromisedEigongModGlobalSettings.EigongAttacks;
using static PromisedEigongModGlobalSettings.EigongSpeed;

public class JumpBackSpeedChanger : BaseSpeedChanger
{
    public override bool IsSpecifiedAttack (string attack) 
        => attack == JUMP_BACK;

    public override float GetSpeed (BossGeneralState state)
        => JUMP_BACK_SPEED;
}