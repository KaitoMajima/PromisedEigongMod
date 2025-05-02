namespace PromisedEigong.SpeedChangers;
using static PromisedEigongModGlobalSettings.EigongAttacks;
using static PromisedEigongModGlobalSettings.EigongSpeed;

public class CrimsonSlamSpeedChanger : BaseSpeedChanger
{
    public override bool IsSpecifiedAttack (string attack) 
        => attack == ATTACK17_CRIMSON_SLAM;

    public override float GetSpeed (BossGeneralState state)
        => ATTACK17_CRIMSON_SLAM_SPEED;
}