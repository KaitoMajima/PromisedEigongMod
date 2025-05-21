namespace PromisedEigong.WeightChanges;
using static PromisedEigongModGlobalSettings.EigongAttacks;

public class _9StarterWeightChanger : BaseWeightChanger
{
    public override void ChangeAttackWeight ()
    {
        ChangePhase1();
        ChangePhase2();
        ChangePhase3();
    }

    void ChangePhase1 ()
    {
        SetStateWeight(ATTACK_PATH + ATTACK9_STARTER_WEIGHT_PHASE_1);
        SetAssociatedBossState(ATTACK_PATH + ATTACK9_STARTER);
        CreateAndAddBossState(ATTACK_PATH + ATTACK3_THRUST_DELAY);
        CreateAndAddBossState(ATTACK_PATH + ATTACK2_TELEPORT_TO_TOP);
        CreateAndAddBossState(ATTACK_PATH + ATTACK16_QUICK_FOO);
        CreateAndAddBossState(ATTACK_PATH + ATTACK6_DOUBLE_ATTACK);
        ProcessCurrentWeight();
    }
    
    void ChangePhase2 ()
    {
        ClearBossStates();
        SetStateWeight(ATTACK_PATH + ATTACK9_STARTER_WEIGHT_PHASE_2);
        SetAssociatedBossState(ATTACK_PATH + ATTACK9_STARTER);
        CreateAndAddBossState(ATTACK_PATH + ATTACK3_THRUST_DELAY);
        CreateAndAddBossState(ATTACK_PATH + ATTACK2_TELEPORT_TO_TOP);
        CreateAndAddBossState(ATTACK_PATH + ATTACK16_QUICK_FOO);
        CreateAndAddBossState(ATTACK_PATH + ATTACK6_DOUBLE_ATTACK);
        CreateAndAddBossState(ATTACK_PATH + ATTACK4_SLASH_UP);
        CreateAndAddBossState(ATTACK_PATH + ATTACK12_SLASH_UP_CRIMSON);
        ProcessCurrentWeight();
    }
    
    void ChangePhase3 ()
    {
        ClearBossStates();
        SetAssociatedBossState(ATTACK_PATH + ATTACK9_STARTER);
        CreateStateWeight(
            ATTACK_PATH + ATTACK9_STARTER, 
            ATTACK_PATH + ATTACK9_STARTER_WEIGHT_PHASE_2,
            ATTACK9_STARTER_WEIGHT_PHASE_3_NAME
        );
        CreateAndAddBossState(ATTACK_PATH + ATTACK19_THRUST_FULL_SCREEN);
        CreateAndAddBossState(ATTACK_PATH + ATTACK20_TELEPORT_OUT);
        CreateAndAddBossState(ATTACK_PATH + ATTACK2_TELEPORT_TO_TOP);
        CreateAndAddBossState(ATTACK_PATH + ATTACK16_QUICK_FOO);
        CreateAndAddBossState(ATTACK_PATH + ATTACK4_SLASH_UP);
        CreateAndAddBossState(ATTACK_PATH + ATTACK12_SLASH_UP_CRIMSON);
        ProcessCurrentWeight();
    }
}