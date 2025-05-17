namespace PromisedEigong.WeightChanges;
using static PromisedEigongModGlobalSettings.EigongAttacks;

public class _7TeleportForwardWeightChanger : BaseWeightChanger
{
    public override void ChangeAttackWeight ()
    {
        ChangePhase2();
    }

    void ChangePhase2 ()
    {
        SetStateWeight(ATTACK_PATH + ATTACK7_TELEPORT_FORWARD_WEIGHT_PHASE_2);
        SetAssociatedBossState(ATTACK_PATH + ATTACK7_TELEPORT_FORWARD);
        CreateAndAddBossState(ATTACK_PATH + ATTACK10_FOO);
        CreateAndAddBossState(ATTACK_PATH + ATTACK5_TELEPORT_TO_BACK);
        CreateAndAddBossState(ATTACK_PATH + ATTACK3_THRUST_DELAY);
        ProcessCurrentWeight();
    }
}