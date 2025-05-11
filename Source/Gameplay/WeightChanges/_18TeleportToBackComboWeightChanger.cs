namespace PromisedEigong.WeightChanges;
using static PromisedEigongModGlobalSettings.EigongAttacks;

public class _18TeleportToBackComboWeightChanger : BaseWeightChanger
{
    protected override WeightReplaceMode WeightReplaceMode => WeightReplaceMode.Add;

    public override void ChangeAttackWeight ()
    {
        ChangePhase1();
    }

    void ChangePhase1 ()
    {
        SetStateWeight(ATTACK_PATH + ATTACK18_TELEPORT_TO_BACK_COMBO_WEIGHT);
        SetAssociatedBossState(ATTACK_PATH + ATTACK18_TELEPORT_TO_BACK_COMBO);
        CreateAndAddModifiedBossState(ATTACK_PATH + ATTACK21_NEW_INSTANT_CRIMSON_BALL);
        ProcessCurrentWeight();
    }
}