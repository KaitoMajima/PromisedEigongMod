namespace PromisedEigong.WeightChanges;
using static PromisedEigongModGlobalSettings.EigongAttacks;

public class CrimsonSlamWeightChanger : BaseWeightChanger
{
    public override void ChangeAttackWeight ()
    {
        SetStateWeight(ATTACK_PATH + ATTACK17_CRIMSON_SLAM_WEIGHT);
        CreateAndAddBossState(ATTACK_PATH + ATTACK2_TELEPORT_TO_TOP);
        CreateAndAddBossState(ATTACK_PATH + ATTACK5_TELEPORT_TO_BACK);
        CreateAndAddBossState(ATTACK_PATH + ATTACK6_DOUBLE_ATTACK);
        CreateAndAddBossState(ATTACK_PATH + ATTACK1_SLOW_STARTER);
        CreateAndAddBossState(ATTACK_PATH + ATTACK7_TELEPORT_FORWARD);
        CreateAndAddBossState(ATTACK_PATH + ATTACK16_QUICK_FOO);
        base.ChangeAttackWeight();
    }
}