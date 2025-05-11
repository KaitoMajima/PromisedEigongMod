namespace PromisedEigong.WeightChanges;
using static PromisedEigongModGlobalSettings.EigongAttacks;

public class _X3AttackParryingWeightChanger : BaseWeightChanger
{
    public override void ChangeAttackWeight ()
    {
        ChangePhase1();
        ChangePhase2();
    }

    void ChangePhase1 ()
    {
        SetStateWeight(STATES_PATH + ATTACK_PARRYING_WEIGHT);
        CreateAndAddBossState(ATTACK_PATH + ATTACK5_TELEPORT_TO_BACK);
        CreateAndAddBossState(ATTACK_PATH + ATTACK7_TELEPORT_FORWARD);
        CreateAndAddBossState(ATTACK_PATH + ATTACK9_STARTER);
        ProcessCurrentWeight();
    }
    
    void ChangePhase2 ()
    {
        SetStateWeight(STATES_PATH + ATTACK_PARRYING_WEIGHT_PHASE_2);
        CreateAndAddBossState(ATTACK_PATH + ATTACK2_TELEPORT_TO_TOP);
        CreateAndAddBossState(ATTACK_PATH + ATTACK8_LONG_CHARGE);
        CreateAndAddBossState(ATTACK_PATH + ATTACK3_THRUST_DELAY);
        CreateAndAddBossState(ATTACK_PATH + ATTACK9_STARTER);
        ProcessCurrentWeight();
    }
}