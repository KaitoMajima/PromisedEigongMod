using NineSolsAPI;

namespace PromisedEigong.DamageBoosters;

using HarmonyLib;

public static class DamageBoost
{
    public static void BoostAttack (DamageDealer damageDealer, float damageBoost, bool revert = false)
    {
        var parentScalarSource = damageDealer.GetComponentInParent<DamageScalarSource>();
        var invoker = AccessTools.FieldRefAccess<MonsterStat, float>("BaseAttackValue");
        var valueGet = invoker.Invoke(parentScalarSource.BindMonster.monsterStat);
        
        ToastManager.Toast("Damage: " + valueGet * damageBoost + "Revert: " + revert);

        if (revert)
            invoker.Invoke(parentScalarSource.BindMonster.monsterStat) = valueGet / damageBoost;
        else
            invoker.Invoke(parentScalarSource.BindMonster.monsterStat) = valueGet * damageBoost;
    }

    public static void BoostRecoverableDamage (ref float damage, float boost)
    {
        damage *= boost;
    }
}