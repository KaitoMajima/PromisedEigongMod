namespace PromisedEigong.WeightChanges;
using static PromisedEigongModGlobalSettings.EigongAttacks;

public class _6DoubleAttackWeightChanger : BaseWeightChanger
{
    public override void ChangeAttackWeight ()
    {
        ChangePhase1();
        ChangePhase2();
    }

    void ChangePhase1 ()
    {
        SetAssociatedBossState(ATTACK_PATH + ATTACK6_DOUBLE_ATTACK);
        CreateAndAddBossState(ATTACK_PATH + ATTACK5_TELEPORT_TO_BACK);
        CreateAndAddBossState(ATTACK_PATH + ATTACK15_TURN_AROUND_BRIGHT_EYES);
        ProcessCurrentGroupNodeWeight(0, 0);
        ClearBossStates();
        CreateAndAddModifiedBossState(STATES_PATH + ATTACK30_NEW_CHAIN_JUMP_BACK);
        CreateAndAddBossState(ATTACK_PATH + ATTACK15_TURN_AROUND_BRIGHT_EYES);
        CreateAndAddBossState(ATTACK_PATH + ATTACK9_STARTER);
        CreateAndAddBossState(ATTACK_PATH + ATTACK6_DOUBLE_ATTACK);
        CreateAndAddBossState(ATTACK_PATH + ATTACK13_TRIPLE_POKE);
        CreateAndAddBossState(ATTACK_PATH + ATTACK2_TELEPORT_TO_TOP);
        CreateAndAddBossState(STATES_PATH + JUMP_BACK);
        ProcessCurrentGroupNodeWeight(1, 0);
    }

    void ChangePhase2 ()
    {
        ClearBossStates();
        CreateAndAddBossState(ATTACK_PATH + ATTACK5_TELEPORT_TO_BACK);
        CreateAndAddBossState(ATTACK_PATH + ATTACK15_TURN_AROUND_BRIGHT_EYES);
        ProcessCurrentGroupNodeWeight(0, 1);
        ClearBossStates();
        SetAssociatedBossState(ATTACK_PATH + ATTACK6_DOUBLE_ATTACK);
        CreateAndAddModifiedBossState(STATES_PATH + ATTACK30_NEW_CHAIN_JUMP_BACK);
        CreateAndAddBossState(ATTACK_PATH + ATTACK15_TURN_AROUND_BRIGHT_EYES);
        CreateAndAddBossState(ATTACK_PATH + ATTACK9_STARTER);
        CreateAndAddBossState(ATTACK_PATH + ATTACK18_TELEPORT_TO_BACK_COMBO);
        CreateAndAddBossState(ATTACK_PATH + ATTACK2_TELEPORT_TO_TOP);
        CreateAndAddBossState(STATES_PATH + JUMP_BACK);
        ProcessCurrentGroupNodeWeight(1, 1);
    }
}