namespace PromisedEigong.WeightChanges;
using static PromisedEigongModGlobalSettings.EigongAttacks;

public class _10FooWeightChanger : BaseWeightChanger
{
    public override void ChangeAttackWeight ()
    {
        ChangePhase1();
        ChangePhase2();
    }

    void ChangePhase1 ()
    {
        SetStateWeight(ATTACK_PATH + ATTACK10_FOO_INTERRUPT_WEIGHT);
        SetAssociatedBossState(ATTACK_PATH + ATTACK10_FOO);
        CreateAndAddBossState(ATTACK_PATH + ATTACK3_THRUST_DELAY);
        CreateAndAddBossState(ATTACK_PATH + ATTACK9_STARTER);
        CreateAndAddBossState(STATES_PATH + JUMP_BACK);
        CreateAndAddBossState(ATTACK_PATH + ATTACK6_DOUBLE_ATTACK);
        ProcessCurrentWeight();
    }
    
    void ChangePhase2 ()
    {
        ClearBossStates();
        SetStateWeight(ATTACK_PATH + ATTACK10_FOO_INTERRUPT_WEIGHT_PHASE_2);
        SetAssociatedBossState(ATTACK_PATH + ATTACK10_FOO);
        CreateAndAddBossState(ATTACK_PATH + ATTACK2_TELEPORT_TO_TOP);
        CreateAndAddBossState(ATTACK_PATH + ATTACK3_THRUST_DELAY);
        CreateAndAddBossState(ATTACK_PATH + ATTACK4_SLASH_UP);
        CreateAndAddBossState(ATTACK_PATH + ATTACK12_SLASH_UP_CRIMSON);
        CreateAndAddBossState(ATTACK_PATH + ATTACK6_DOUBLE_ATTACK);
        CreateAndAddBossState(STATES_PATH + JUMP_BACK);
        ProcessCurrentWeight();
    }
}