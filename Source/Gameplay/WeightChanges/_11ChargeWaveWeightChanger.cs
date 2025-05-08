namespace PromisedEigong.WeightChanges;
using static PromisedEigongModGlobalSettings.EigongAttacks;

public class _11ChargeWaveWeightChanger : BaseWeightChanger
{
    protected override WeightReplaceMode WeightReplaceMode => WeightReplaceMode.Add;

    public override void ChangeAttackWeight ()
    {
        ChangePhase2();
    }

    void ChangePhase2 ()
    {
        SetStateWeight(ATTACK_PATH + ATTACK11_GIANT_CHARGE_WAVE_WEIGHT_PHASE_2);
        CreateAndAddBossState(ATTACK_PATH + ATTACK3_THRUST_DELAY);
        CreateAndAddBossState(ATTACK_PATH + ATTACK2_TELEPORT_TO_TOP);
        SetWeightRandomness(true);
        ProcessCurrentWeight();
    }
}