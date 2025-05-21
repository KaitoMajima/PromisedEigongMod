namespace PromisedEigong.WeightChanges;
using static PromisedEigongModGlobalSettings.EigongAttacks;

public class _X2JumpBackWeightChanger : BaseWeightChanger
{
    public override void ChangeAttackWeight ()
    {
        ChangePhase3();
    }
    
    void ChangePhase3 ()
    {
        ClearBossStates();
        SetStateWeight(STATES_PATH + JUMP_BACK_WEIGHT_PHASE_3);
        CreateAndAddBossState(ATTACK_PATH + ATTACK1_SLOW_STARTER);
        CreateAndAddBossState(ATTACK_PATH + ATTACK6_DOUBLE_ATTACK);
        ProcessCurrentWeight();
    }
}