namespace PromisedEigong.WeightChanges;
using static PromisedEigongModGlobalSettings.EigongAttacks;

public class TriplePokeWeightChanger : BaseWeightChanger
{
    protected override WeightReplaceMode WeightReplaceMode => WeightReplaceMode.Add;

    public override void ChangeAttackWeight ()
    {
        SetStateWeight(ATTACK_PATH + ATTACK13_TRIPLE_POKE_WEIGHT);
        CreateAndAddBossState(ATTACK_PATH + ATTACK9_STARTER);
        base.ChangeAttackWeight();
    }
}