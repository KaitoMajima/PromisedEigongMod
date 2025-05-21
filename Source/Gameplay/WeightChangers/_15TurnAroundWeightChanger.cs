namespace PromisedEigong.WeightChanges;
using static PromisedEigongModGlobalSettings.EigongAttacks;

public class _15TurnAroundWeightChanger : BaseWeightChanger
{
    protected override WeightReplaceMode WeightReplaceMode => WeightReplaceMode.Add;

    public override void ChangeAttackWeight ()
    {
        ChangePhase2();
    }

    void ChangePhase2 ()
    {
        SetStateWeight(ATTACK_PATH + ATTACK15_TURN_AROUND_BRIGHT_EYES_WEIGHT_PHASE_2);
        SetAssociatedBossState(ATTACK_PATH + ATTACK15_TURN_AROUND_BRIGHT_EYES);
        CreateAndAddModifiedBossState(ATTACK_PATH + ATTACK21_NEW_QUICK_CRIMSON_BALL);
        CreateAndAddBossState(ATTACK_PATH + ATTACK2_TELEPORT_TO_TOP);
        ProcessCurrentWeight();
    }
}