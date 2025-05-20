namespace PromisedEigong.SpeedChangers;
using static PromisedEigongModGlobalSettings.EigongAttacks;
using static PromisedEigongModGlobalSettings.EigongSpeed;

public class _15TurnAroundSpeedChanger : BaseSpeedChanger
{
    public override bool IsSpecifiedAttack (string attack) 
        => attack == ATTACK15_TURN_AROUND_BRIGHT_EYES;

    public override float GetSpeed (int phase) 
        => ATTACK15_TURN_AROUND_BRIGHT_EYES_SPEED;
}