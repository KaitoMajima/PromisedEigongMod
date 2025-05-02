namespace PromisedEigong.WeightChanges;
using static PromisedEigongModGlobalSettings.EigongAttacks;

public class SlowStarterWeightChanger : BaseWeightChanger
{
    protected override WeightReplaceMode WeightReplaceMode => WeightReplaceMode.Add;

    public override void ChangeAttackWeight ()
    {
        SetStateWeight(ATTACK_PATH + ATTACK1_SLOW_STARTER_PHASE_0);
        CreateAndAddBossState(ATTACK_PATH + ATTACK9_STARTER);
        base.ChangeAttackWeight();
    }
}