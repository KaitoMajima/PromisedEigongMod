namespace PromisedEigong.WeightChanges;
using static PromisedEigongModGlobalSettings.EigongAttacks;

public class _2TeleportToTopWeightChanger : BaseWeightChanger
{
    public override void ChangeAttackWeight ()
    {
        ChangePhase1();
        ChangePhase2();
        ChangePhase3();
    }

    void ChangePhase1 ()
    {
        SetStateWeight(ATTACK_PATH + ATTACK2_TELEPORT_TO_TOP_WEIGHT);
        SetAssociatedBossState(ATTACK_PATH + ATTACK2_TELEPORT_TO_TOP);
        CreateAndAddBossState(ATTACK_PATH + ATTACK17_CRIMSON_SLAM);
        SetWeightRandomness(true);
        ProcessCurrentWeight();
    }
    
    void ChangePhase2 ()
    {
        ClearBossStates();
        SetStateWeight(ATTACK_PATH + ATTACK2_TELEPORT_TO_TOP_WEIGHT_PHASE_2);
        SetAssociatedBossState(ATTACK_PATH + ATTACK2_TELEPORT_TO_TOP);
        CreateAndAddBossState(ATTACK_PATH + ATTACK17_CRIMSON_SLAM);
        SetWeightRandomness(true);
        ProcessCurrentWeight();
    }
    
    void ChangePhase3 ()
    {
        ClearBossStates();
        SetStateWeight(ATTACK_PATH + ATTACK2_TELEPORT_TO_TOP_WEIGHT_PHASE_3);
        SetAssociatedBossState(ATTACK_PATH + ATTACK2_TELEPORT_TO_TOP);
        CreateAndAddModifiedBossState(ATTACK_PATH + ATTACK32_NEW_TELEPORT_TO_BACK_TO_CS);
        CreateAndAddBossState(ATTACK_PATH + ATTACK20_TELEPORT_OUT);
        SetWeightRandomness(true);
        ProcessCurrentWeight();
    }
}