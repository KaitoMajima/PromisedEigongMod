namespace PromisedEigong.WeightChanges;
using static PromisedEigongModGlobalSettings.EigongAttacks;

public class StarterWeightChanger : BaseWeightChanger
{
    public override void ChangeAttackWeight ()
    {
        SetStateWeight(ATTACK_PATH + ATTACK9_STARTER_PHASE_0);
        CreateAndAddBossState(ATTACK_PATH + ATTACK3_THRUST_DELAY);
        CreateAndAddBossState(ATTACK_PATH + ATTACK2_TELEPORT_TO_TOP);
        CreateAndAddBossState(ATTACK_PATH + ATTACK16_QUICK_FOO);
        CreateAndAddBossState(ATTACK_PATH + ATTACK6_DOUBLE_ATTACK);
        CreateAndAddBossState(ATTACK_PATH + ATTACK14_CRIMSON_BALL);
        base.ChangeAttackWeight();
    }
}