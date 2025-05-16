namespace PromisedEigong.Gameplay.AttackFactories;

using UnityEngine;
using static PromisedEigongModGlobalSettings.EigongAttacks;

public abstract class BaseAttackFactory
{
    public abstract string AttackToBeCopied { get; }
    public abstract string AttackToBeCreated { get; }

    public virtual ModifiedBossGeneralState CopyAttack (BossGeneralState bossGeneralState)
    {
        var attacksParent = GameObject.Find(ATTACK_PATH);
        var newAttackObj = Object.Instantiate(new GameObject(), attacksParent.transform);
        var newAttack = newAttackObj.AddComponent<ModifiedBossGeneralState>();
        newAttack.Setup(AttackToBeCreated, bossGeneralState, 1);
        newAttack.name = AttackToBeCreated;
        newAttack.ModifiedName = AttackToBeCreated;
        return newAttack;
    }
}