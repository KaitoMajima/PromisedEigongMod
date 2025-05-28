namespace PromisedEigong.WeightChanges;
using static PromisedEigongModGlobalSettings.EigongAttacks;

public class _8LongChargeWeightChanger : BaseWeightChanger
{
    public override void ChangeAttackWeight ()
    {
        ChangePhase2();
        ChangePhase3();
    }

    void ChangePhase2 ()
    {
        SetStateWeight(ATTACK_PATH + ATTACK8_LONG_CHARGE_WEIGHT_PHASE_2);
        SetAssociatedBossState(ATTACK_PATH + ATTACK8_LONG_CHARGE);
        CreateAndAddBossState(ATTACK_PATH + ATTACK3_THRUST_DELAY);
        CreateAndAddBossState(ATTACK_PATH + ATTACK5_TELEPORT_TO_BACK);
        CreateAndAddBossState(ATTACK_PATH + ATTACK18_TELEPORT_TO_BACK_COMBO);
        CreateAndAddBossState(ATTACK_PATH + ATTACK11_GIANT_CHARGE_WAVE);
        CreateAndAddBossState(ATTACK_PATH + ATTACK11_GIANT_CHARGE_WAVE);
        CreateAndAddBossState(ATTACK_PATH + ATTACK11_GIANT_CHARGE_WAVE);
        CreateAndAddModifiedBossState(STATES_PATH + ATTACK30_NEW_CHAIN_JUMP_BACK);
        SetWeightRandomness(true);
        ProcessCurrentWeight();
    }
    
    void ChangePhase3 ()
    {
        ClearBossStates();
        WeightReplaceMode = WeightReplaceMode.Add;
        SetStateWeight(ATTACK_PATH + ATTACK8_LONG_CHARGE_WEIGHT_PHASE_3);
        SetAssociatedBossState(ATTACK_PATH + ATTACK8_LONG_CHARGE);
        CreateAndAddBossState(ATTACK_PATH + ATTACK3_THRUST_DELAY);
        CreateAndAddBossState(ATTACK_PATH + ATTACK5_TELEPORT_TO_BACK);
        CreateAndAddBossState(ATTACK_PATH + ATTACK18_TELEPORT_TO_BACK_COMBO);
        CreateAndAddModifiedBossState(STATES_PATH + ATTACK30_NEW_CHAIN_JUMP_BACK);
        CreateAndAddBossState(ATTACK_PATH + ATTACK11_GIANT_CHARGE_WAVE);
        CreateAndAddBossState(ATTACK_PATH + ATTACK11_GIANT_CHARGE_WAVE);
        CreateAndAddBossState(STATES_PATH + JUMP_BACK);
        SetWeightRandomness(true);
        ProcessCurrentWeight();
    }
}