namespace PromisedEigong.WeightChanges;
using static PromisedEigongModGlobalSettings.EigongAttacks;

public class _11ChargeWaveWeightChanger : BaseWeightChanger
{
    protected override WeightReplaceMode WeightReplaceMode => WeightReplaceMode.Add;

    public override void ChangeAttackWeight ()
    {
        ChangePhase2();
        ChangePhase3();
    }

    void ChangePhase2 ()
    {
        SetStateWeight(ATTACK_PATH + ATTACK11_GIANT_CHARGE_WAVE_WEIGHT_PHASE_2);
        SetAssociatedBossState(ATTACK_PATH + ATTACK11_GIANT_CHARGE_WAVE);
        CreateAndAddBossState(ATTACK_PATH + ATTACK3_THRUST_DELAY);
        CreateAndAddBossState(ATTACK_PATH + ATTACK2_TELEPORT_TO_TOP);
        CreateAndAddModifiedBossState(ATTACK_PATH + ATTACK21_NEW_QUICK_CRIMSON_BALL);
        CreateAndAddModifiedBossState(ATTACK_PATH + ATTACK22_NEW_CHAIN_SLOW_STARTER);
        SetWeightRandomness(true);
        ProcessCurrentWeight();
    }
    
    void ChangePhase3 ()
    {
        ClearBossStates();
        WeightReplaceMode = WeightReplaceMode.Add;
        SetStateWeight(ATTACK_PATH + ATTACK11_GIANT_CHARGE_WAVE_WEIGHT_PHASE_3);
        SetAssociatedBossState(ATTACK_PATH + ATTACK11_GIANT_CHARGE_WAVE);
        CreateAndAddBossState(ATTACK_PATH + ATTACK3_THRUST_DELAY);
        CreateAndAddBossState(ATTACK_PATH + ATTACK2_TELEPORT_TO_TOP);
        CreateAndAddModifiedBossState(ATTACK_PATH + ATTACK21_NEW_QUICK_CRIMSON_BALL);
        CreateAndAddModifiedBossState(ATTACK_PATH + ATTACK22_NEW_CHAIN_SLOW_STARTER);
        CreateAndAddBossState(ATTACK_PATH + ATTACK19_THRUST_FULL_SCREEN);
        SetWeightRandomness(true);
        ProcessCurrentWeight();
    }
}