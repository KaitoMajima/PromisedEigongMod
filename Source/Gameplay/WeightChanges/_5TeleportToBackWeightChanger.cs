namespace PromisedEigong.WeightChanges;
using static PromisedEigongModGlobalSettings.EigongAttacks;

public class _5TeleportToBackWeightChanger : BaseWeightChanger
{
    public override void ChangeAttackWeight ()
    {
        ChangePhase1();
        ChangePhase2();
    }

    void ChangePhase1 ()
    {
        SetStateWeight(ATTACK_PATH + ATTACK5_TELEPORT_TO_BACK_WEIGHT_PHASE_2);
        SetAssociatedBossState(ATTACK_PATH + ATTACK5_TELEPORT_TO_BACK);
        ProcessCurrentWeight();
    }

    void ChangePhase2 ()
    {
        ClearBossStates();
        SetStateWeight(ATTACK_PATH + ATTACK5_TELEPORT_TO_BACK_WEIGHT_PHASE_2);
        SetAssociatedBossState(ATTACK_PATH + ATTACK5_TELEPORT_TO_BACK);
        CreateAndAddBossState(ATTACK_PATH + ATTACK10_FOO);
        CreateAndAddBossState(ATTACK_PATH + ATTACK8_LONG_CHARGE);
        CreateAndAddBossState(STATES_PATH + JUMP_BACK);
        ProcessCurrentWeight();
    }
}