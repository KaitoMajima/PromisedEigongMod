namespace PromisedEigong.Gameplay.AttackFactories;

using UnityEngine;
using static PromisedEigongModGlobalSettings.EigongAttacks;

public abstract class BaseAttackFactory
{
    public abstract string AttackToBeCopied { get; }
    public abstract string AttackToBeCreated { get; }

    public abstract void CopyAttack (BossGeneralState bossGeneralState);

    protected ModifiedBossGeneralState SetupAttack (BossGeneralState bossGeneralState, bool isNotAnAttack = false)
    {
        var attacksParent = GameObject.Find(isNotAnAttack ? STATES_PATH : ATTACK_PATH);
        var newAttackObj = new GameObject();
        newAttackObj.transform.SetParent(attacksParent.transform);
        var newAttack = newAttackObj.AddComponent<ModifiedBossGeneralState>();
        newAttack.Setup(AttackToBeCreated, bossGeneralState, 1);
        newAttack.name = AttackToBeCreated;
        newAttack.ModifiedName = AttackToBeCreated;
        return newAttack;
    }
}