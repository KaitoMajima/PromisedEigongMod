namespace PromisedEigong.WeightChanges;
using static PromisedEigongModGlobalSettings.EigongAttacks;

public class _1SlowStarterWeightChanger : BaseWeightChanger
{
    public override void ChangeAttackWeight ()
    {
        ChangePhase1();
        ChangePhase2();
        ChangePhase3();
    }

    void ChangePhase1 ()
    {
        SetStateWeight(ATTACK_PATH + ATTACK1_SLOW_STARTER_WEIGHT);
        SetAssociatedBossState(ATTACK_PATH + ATTACK1_SLOW_STARTER);
        CreateAndAddModifiedBossState(STATES_PATH + ATTACK30_NEW_CHAIN_JUMP_BACK);
        CreateAndAddBossState(ATTACK_PATH + ATTACK9_STARTER);
        CreateAndAddBossState(ATTACK_PATH + ATTACK6_DOUBLE_ATTACK);
        CreateAndAddBossState(ATTACK_PATH + ATTACK2_TELEPORT_TO_TOP);
        ProcessCurrentWeight();
    }
    
    void ChangePhase2 ()
    {
        ClearBossStates();
        SetStateWeight(ATTACK_PATH + ATTACK1_SLOW_STARTER_WEIGHT_PHASE_2);
        SetAssociatedBossState(ATTACK_PATH + ATTACK1_SLOW_STARTER);
        CreateAndAddModifiedBossState(STATES_PATH + ATTACK30_NEW_CHAIN_JUMP_BACK);
        CreateAndAddBossState(ATTACK_PATH + ATTACK9_STARTER);
        CreateAndAddBossState(ATTACK_PATH + ATTACK6_DOUBLE_ATTACK);
        CreateAndAddBossState(ATTACK_PATH + ATTACK18_TELEPORT_TO_BACK_COMBO);
        ProcessCurrentWeight();
    }

    void ChangePhase3 ()
    {
        ClearBossStates();
        SetAssociatedBossState(ATTACK_PATH + ATTACK1_SLOW_STARTER);
        CreateStateWeight(
            ATTACK_PATH + ATTACK1_SLOW_STARTER, 
            ATTACK_PATH + ATTACK1_SLOW_STARTER_WEIGHT_PHASE_2, 
            ATTACK1_SLOW_STARTER_WEIGHT_PHASE_3_NAME
        );
        CreateAndAddModifiedBossState(STATES_PATH + ATTACK30_NEW_CHAIN_JUMP_BACK);
        CreateAndAddBossState(ATTACK_PATH + ATTACK9_STARTER);
        CreateAndAddModifiedBossState(ATTACK_PATH + ATTACK38_SPECIAL_DOUBLE_ATTACK);
        CreateAndAddModifiedBossState(ATTACK_PATH + ATTACK40_TELEPORT_TO_BACK_COMBO_PHASE_3);
        ProcessCurrentWeight();
    }
}