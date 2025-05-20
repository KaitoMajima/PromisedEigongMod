namespace PromisedEigong.SpeedChangers;
using static PromisedEigongModGlobalSettings.EigongAttacks;
using static PromisedEigongModGlobalSettings.EigongSpeed;

public class _17CrimsonSlamSpeedChanger : BaseSpeedChanger
{
    public override bool IsSpecifiedAttack (string attack) 
        => attack == ATTACK17_CRIMSON_SLAM;

    public override float GetSpeed (int phase) 
        => ATTACK17_CRIMSON_SLAM_SPEED;
}