namespace PromisedEigong.WeightChanges;
using static PromisedEigongModGlobalSettings.EigongAttacks;

public class _17CrimsonSlamWeightChanger : BaseWeightChanger
{
    public override void ChangeAttackWeight ()
    {
        ChangePhase1();
        ChangePhase2();
    }

    void ChangePhase1 ()
    {
        SetStateWeight(ATTACK_PATH + ATTACK17_CRIMSON_SLAM_WEIGHT);
        CreateAndAddBossState(ATTACK_PATH + ATTACK2_TELEPORT_TO_TOP);
        CreateAndAddBossState(ATTACK_PATH + ATTACK5_TELEPORT_TO_BACK);
        CreateAndAddBossState(ATTACK_PATH + ATTACK6_DOUBLE_ATTACK);
        CreateAndAddBossState(ATTACK_PATH + ATTACK1_SLOW_STARTER);
        CreateAndAddBossState(ATTACK_PATH + ATTACK7_TELEPORT_FORWARD);
        CreateAndAddBossState(ATTACK_PATH + ATTACK16_QUICK_FOO);
        ProcessCurrentWeight();
    }
    
    void ChangePhase2 ()
    {
        SetStateWeight(ATTACK_PATH + ATTACK17_CRIMSON_SLAM_WEIGHT_PHASE_2);
        CreateAndAddBossState(ATTACK_PATH + ATTACK2_TELEPORT_TO_TOP);
        CreateAndAddBossState(ATTACK_PATH + ATTACK5_TELEPORT_TO_BACK);
        CreateAndAddBossState(ATTACK_PATH + ATTACK4_SLASH_UP);
        CreateAndAddBossState(ATTACK_PATH + ATTACK12_SLASH_UP_CRIMSON);
        CreateAndAddBossState(ATTACK_PATH + ATTACK16_QUICK_FOO);
        CreateAndAddBossState(ATTACK_PATH + ATTACK8_LONG_CHARGE);
        CreateAndAddBossState(ATTACK_PATH + ATTACK9_STARTER);
        ProcessCurrentWeight();
    }
    
}