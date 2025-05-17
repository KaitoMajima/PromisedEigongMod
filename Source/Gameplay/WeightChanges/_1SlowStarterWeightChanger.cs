namespace PromisedEigong.WeightChanges;
using static PromisedEigongModGlobalSettings.EigongAttacks;

public class _1SlowStarterWeightChanger : BaseWeightChanger
{
    public override void ChangeAttackWeight ()
    {
        ChangePhase1();
        ChangePhase2();
    }

    void ChangePhase1 ()
    {
        SetStateWeight(ATTACK_PATH + ATTACK1_SLOW_STARTER_WEIGHT);
        SetAssociatedBossState(ATTACK_PATH + ATTACK1_SLOW_STARTER);
        CreateAndAddModifiedBossState(ATTACK_PATH + ATTACK22_NEW_CHAIN_SLOW_STARTER);
        CreateAndAddBossState(ATTACK_PATH + ATTACK9_STARTER);
        CreateAndAddBossState(ATTACK_PATH + ATTACK6_DOUBLE_ATTACK);
        CreateAndAddBossState(STATES_PATH + JUMP_BACK);
        ProcessCurrentWeight();
    }
    
    void ChangePhase2 ()
    {
        SetStateWeight(ATTACK_PATH + ATTACK1_SLOW_STARTER_WEIGHT_PHASE_2);
        SetAssociatedBossState(ATTACK_PATH + ATTACK1_SLOW_STARTER);
        CreateAndAddModifiedBossState(ATTACK_PATH + ATTACK22_NEW_CHAIN_SLOW_STARTER);
        CreateAndAddBossState(ATTACK_PATH + ATTACK9_STARTER);
        CreateAndAddBossState(ATTACK_PATH + ATTACK6_DOUBLE_ATTACK);
        CreateAndAddBossState(STATES_PATH + JUMP_BACK);
        CreateAndAddBossState(ATTACK_PATH + ATTACK18_TELEPORT_TO_BACK_COMBO);
        ProcessCurrentWeight();
    }
}