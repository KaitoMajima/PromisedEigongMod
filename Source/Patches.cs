namespace PromisedEigong;

using HarmonyLib;
using I2.Loc;
using NineSolsAPI;
using UnityEngine;
using static PromisedEigongModGlobalSettings.EigongLocs;
using static PromisedEigongModGlobalSettings.EigongTitle;
using static PromisedEigongModGlobalSettings.EigongRefs;
using static PromisedEigongModGlobalSettings.EigongAttackRefs;
using static PromisedEigongModGlobalSettings.EigongDamageBoost;

[HarmonyPatch]
public class Patches {
    
    [HarmonyPostfix]
    [HarmonyPatch(typeof(LocalizationManager), "GetTranslation")]
    static void ChangeEigongName (string Term, ref string __result)
    {
        if (Term == EIGONG_TITLE_LOC)
            __result = EIGONG_TITLE;
    }

    [HarmonyPrefix]
    [HarmonyPatch(typeof(PostureSystem), "InternalInjuryDecreasePosture")]
    static void EigongDecreaseScaledPosture (PostureSystem __instance, ref float value)
    {
        if (__instance.BindMonster.Name != BIND_MONSTER_EIGONG_NAME)
            return;

        value *= DamageScaler.GetScaledPostureDamage();
    }
    
    [HarmonyPrefix]
    [HarmonyPatch(typeof(PostureSystem), "TakeDamage")]
    static void EigongTakeScaledDamage (PostureSystem __instance, ref float value, bool isConsumeInjury = false, EffectDealer dealer = null)
    {
        if (__instance.BindMonster.Name != BIND_MONSTER_EIGONG_NAME)
            return;
        
        if (dealer == null)
            return;
        
        value *= DamageScaler.GetScaledDirectDamage(dealer.detailType);
    }
    
    [HarmonyPrefix]
    [HarmonyPatch(typeof(PlayerHealth), "ReceiveDamage")]
    static void EigongBoostDamage (PlayerHealth __instance, ref DamageDealer damageDealer)
    {
        if (damageDealer.Owner.currentPlayingAnimatorState == EIGONG_TALISMAN_ATTACK_1 ||
            damageDealer.Owner.currentPlayingAnimatorState == EIGONG_TALISMAN_ATTACK_2)
        {
            var parentScalarSource = damageDealer.GetComponentInParent<DamageScalarSource>();
            var invoker = AccessTools.FieldRefAccess<MonsterStat, float>("BaseAttackValue");
            var valueGet = invoker.Invoke(parentScalarSource.BindMonster.monsterStat);
            invoker.Invoke(parentScalarSource.BindMonster.monsterStat) = valueGet *= EIGONG_TALISMAN_BOOST;
            
            ToastManager.Toast("Hit by Talisman!");
            ToastManager.Toast("Base damage:" + parentScalarSource.BindMonster._monsterStat.AttackValue);
            ToastManager.Toast("Final damage:" + damageDealer.DamageAmount);
        }
    }
    
    [HarmonyPostfix]
    [HarmonyPatch(typeof(PlayerHealth), "ReceiveDamage")]
    static void RevertBoostedDamage (PlayerHealth __instance, ref DamageDealer damageDealer)
    {
        if (damageDealer.Owner.currentPlayingAnimatorState == EIGONG_TALISMAN_ATTACK_1 ||
            damageDealer.Owner.currentPlayingAnimatorState == EIGONG_TALISMAN_ATTACK_2)
        {
            var parentScalarSource = damageDealer.GetComponentInParent<DamageScalarSource>();
            var invoker = AccessTools.FieldRefAccess<MonsterStat, float>("BaseAttackValue");
            var valueGet = invoker.Invoke(parentScalarSource.BindMonster.monsterStat);
            invoker.Invoke(parentScalarSource.BindMonster.monsterStat) = valueGet /= EIGONG_TALISMAN_BOOST;
            
            ToastManager.Toast("Reverted Base Damage:" + parentScalarSource.BindMonster._monsterStat.AttackValue);
        }
    }
    
    [HarmonyPrefix]
    [HarmonyPatch(typeof(PlayerHealth), "ReceiveDOT_Damage")]
    static void EigongBoostFireDamage (ref float damageValue)
    {
        //Check if Eigong
        damageValue *= EIGONG_FIRE_BOOST;
        ToastManager.Toast("DOT Amount:" + damageValue);
    }
}