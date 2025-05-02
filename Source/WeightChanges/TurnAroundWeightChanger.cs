namespace PromisedEigong.WeightChanges;
using static PromisedEigongModGlobalSettings.EigongAttacks;

public class TurnAroundWeightChanger : BaseWeightChanger
{
    protected override WeightReplaceMode WeightReplaceMode => WeightReplaceMode.Add;

    public override void ChangeAttackWeight ()
    {
        SetStateWeight(ATTACK_PATH + ATTACK15_TURN_AROUND_BRIGHT_EYES_WEIGHT);
        CreateAndAddBossState(ATTACK_PATH + ATTACK14_CRIMSON_BALL);
        CreateAndAddBossState(ATTACK_PATH + ATTACK13_TRIPLE_POKE);
        CreateAndAddBossState(ATTACK_PATH + ATTACK6_DOUBLE_ATTACK);
        CreateAndAddBossState(ATTACK_PATH + ATTACK9_STARTER);
        CreateAndAddBossState(ATTACK_PATH + ATTACK2_TELEPORT_TO_TOP);
        base.ChangeAttackWeight();
    }
}