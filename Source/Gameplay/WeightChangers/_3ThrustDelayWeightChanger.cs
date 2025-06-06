﻿namespace PromisedEigong.WeightChanges;
using static PromisedEigongModGlobalSettings.EigongAttacks;

public class _3ThrustDelayWeightChanger : BaseWeightChanger
{
    public override void ChangeAttackWeight ()
    {
        ChangePhase1();
        ChangePhase2();
        ChangePhase3();
    }

    void ChangePhase1 ()
    {
        SetStateWeight(ATTACK_PATH + ATTACK3_THRUST_DELAY_WEIGHT);
        SetAssociatedBossState(ATTACK_PATH + ATTACK3_THRUST_DELAY);
        CreateAndAddBossState(STATES_PATH + JUMP_BACK);
        CreateAndAddBossState(ATTACK_PATH + ATTACK15_TURN_AROUND_BRIGHT_EYES);
        ProcessCurrentWeight();
    }

    void ChangePhase2 ()
    {
        ClearBossStates();
        SetStateWeight(ATTACK_PATH + ATTACK3_THRUST_DELAY_WEIGHT_PHASE_2);
        SetAssociatedBossState(ATTACK_PATH + ATTACK3_THRUST_DELAY);
        CreateAndAddBossState(ATTACK_PATH + ATTACK15_TURN_AROUND_BRIGHT_EYES);
        CreateAndAddBossState(ATTACK_PATH + ATTACK18_TELEPORT_TO_BACK_COMBO);
        CreateAndAddBossState(ATTACK_PATH + ATTACK9_STARTER);
        CreateAndAddBossState(STATES_PATH + JUMP_BACK);
        ProcessCurrentWeight();
    }
    
    void ChangePhase3 ()
    {
        ClearBossStates();
        WeightReplaceMode = WeightReplaceMode.Add;
        SetStateWeight(ATTACK_PATH + ATTACK3_THRUST_DELAY_WEIGHT_PHASE_3);
        SetAssociatedBossState(ATTACK_PATH + ATTACK3_THRUST_DELAY);
        CreateAndAddBossState(ATTACK_PATH + ATTACK15_TURN_AROUND_BRIGHT_EYES);
        CreateAndAddModifiedBossState(ATTACK_PATH + ATTACK40_TELEPORT_TO_BACK_COMBO_PHASE_3);
        CreateAndAddBossState(ATTACK_PATH + ATTACK9_STARTER);
        CreateAndAddBossState(ATTACK_PATH + ATTACK19_THRUST_FULL_SCREEN);
        CreateAndAddBossState(STATES_PATH + JUMP_BACK);
        ProcessCurrentWeight();
    }
}