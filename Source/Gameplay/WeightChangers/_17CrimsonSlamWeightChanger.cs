namespace PromisedEigong.WeightChanges;
using static PromisedEigongModGlobalSettings.EigongAttacks;

public class _17CrimsonSlamWeightChanger : BaseWeightChanger
{
    public override void ChangeAttackWeight ()
    {
        ChangePhase1();
        ChangePhase2();
        ChangePhase3();
    }

    void ChangePhase1 ()
    {
        SetStateWeight(ATTACK_PATH + ATTACK17_CRIMSON_SLAM_WEIGHT);
        SetAssociatedBossState(ATTACK_PATH + ATTACK17_CRIMSON_SLAM);
        CreateAndAddBossState(ATTACK_PATH + ATTACK2_TELEPORT_TO_TOP);
        CreateAndAddBossState(ATTACK_PATH + ATTACK5_TELEPORT_TO_BACK);
        CreateAndAddBossState(ATTACK_PATH + ATTACK6_DOUBLE_ATTACK);
        CreateAndAddBossState(ATTACK_PATH + ATTACK1_SLOW_STARTER);
        CreateAndAddBossState(ATTACK_PATH + ATTACK7_TELEPORT_FORWARD);
        ProcessCurrentWeight();
    }
    
    void ChangePhase2 ()
    {
        ClearBossStates();
        SetStateWeight(ATTACK_PATH + ATTACK17_CRIMSON_SLAM_WEIGHT_PHASE_2);
        SetAssociatedBossState(ATTACK_PATH + ATTACK17_CRIMSON_SLAM);
        CreateAndAddBossState(ATTACK_PATH + ATTACK2_TELEPORT_TO_TOP);
        CreateAndAddBossState(ATTACK_PATH + ATTACK5_TELEPORT_TO_BACK);
        CreateAndAddBossState(ATTACK_PATH + ATTACK4_SLASH_UP);
        CreateAndAddBossState(ATTACK_PATH + ATTACK12_SLASH_UP_CRIMSON);
        CreateAndAddModifiedBossState(STATES_PATH + ATTACK30_NEW_CHAIN_JUMP_BACK);
        CreateAndAddBossState(ATTACK_PATH + ATTACK8_LONG_CHARGE);
        CreateAndAddBossState(ATTACK_PATH + ATTACK9_STARTER);
        ProcessCurrentWeight();
    }
    
    void ChangePhase3 ()
    {
        ClearBossStates();
        WeightReplaceMode = WeightReplaceMode.Add;
        SetStateWeight(ATTACK_PATH + ATTACK17_CRIMSON_SLAM_WEIGHT_PHASE_3);
        SetAssociatedBossState(ATTACK_PATH + ATTACK17_CRIMSON_SLAM);
        CreateAndAddBossState(ATTACK_PATH + ATTACK2_TELEPORT_TO_TOP);
        CreateAndAddBossState(ATTACK_PATH + ATTACK5_TELEPORT_TO_BACK);
        CreateAndAddBossState(ATTACK_PATH + ATTACK4_SLASH_UP);
        CreateAndAddBossState(ATTACK_PATH + ATTACK12_SLASH_UP_CRIMSON);
        CreateAndAddModifiedBossState(STATES_PATH + ATTACK30_NEW_CHAIN_JUMP_BACK);
        CreateAndAddBossState(ATTACK_PATH + ATTACK8_LONG_CHARGE);
        CreateAndAddBossState(ATTACK_PATH + ATTACK9_STARTER);
        ProcessCurrentWeight();
    }
    
}