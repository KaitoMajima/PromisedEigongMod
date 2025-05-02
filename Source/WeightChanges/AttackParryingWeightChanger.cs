namespace PromisedEigong.WeightChanges;
using static PromisedEigongModGlobalSettings.EigongAttacks;

public class AttackParryingWeightChanger : BaseWeightChanger
{
    public override void ChangeAttackWeight ()
    {
        SetStateWeight(STATES_PATH + ATTACK_PARRYING_WEIGHT);
        CreateAndAddBossState(ATTACK_PATH + ATTACK5_TELEPORT_TO_BACK);
        CreateAndAddBossState(ATTACK_PATH + ATTACK18_TELEPORT_TO_BACK_COMBO);
        CreateAndAddBossState(ATTACK_PATH + ATTACK7_TELEPORT_FORWARD);
        CreateAndAddBossState(ATTACK_PATH + ATTACK9_STARTER);
        base.ChangeAttackWeight();
    }
}