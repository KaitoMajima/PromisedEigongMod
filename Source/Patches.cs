using UnityEngine.SceneManagement;

namespace PromisedEigong;

using HarmonyLib;
using I2.Loc;
using NineSolsAPI;
using UnityEngine;
using static PromisedEigongModGlobalSettings.EigongLocs;
using static PromisedEigongModGlobalSettings.EigongTitle;
using static PromisedEigongModGlobalSettings.EigongRefs;
using static PromisedEigongModGlobalSettings.EigongAttackAnimationRefs;
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
        if (damageDealer.Owner.name != BIND_MONSTER_EIGONG_NAME)
            return;

        switch (damageDealer.Owner.currentPlayingAnimatorState)
        {
            case EIGONG_FOO_ATTACK_1 or EIGONG_FOO_ATTACK_2:
            {
                BoostAttack(damageDealer, EIGONG_FOO_BOOST);
                break;
            }
            case EIGONG_CRIMSON_BALL_ATTACK:
            {
                BoostAttack(damageDealer, EIGONG_CRIMSON_BALL_BOOST);
                break;
            }
            default:
            {
                BoostAttack(damageDealer, EIGONG_DEFAULT_ATTACK_BOOST);
                break;
            }
        }
    }

    [HarmonyPostfix]
    [HarmonyPatch(typeof(PlayerHealth), "ReceiveDamage")]
    static void RevertBoostedDamage (PlayerHealth __instance, ref DamageDealer damageDealer)
    {
        if (damageDealer.Owner.name != BIND_MONSTER_EIGONG_NAME)
            return;

        switch (damageDealer.Owner.currentPlayingAnimatorState)
        {
            case EIGONG_FOO_ATTACK_1 or EIGONG_FOO_ATTACK_2:
            {
                BoostAttack(damageDealer, EIGONG_FOO_BOOST, revert: true);
                break;
            }
            case EIGONG_CRIMSON_BALL_ATTACK:
            {
                BoostAttack(damageDealer, EIGONG_CRIMSON_BALL_BOOST, revert: true);
                break;
            }
            default:
            {
                BoostAttack(damageDealer, EIGONG_DEFAULT_ATTACK_BOOST, revert: true);
                break;
            }
        }
    }
    
    [HarmonyPrefix]
    [HarmonyPatch(typeof(PlayerHealth), "ReceiveDOT_Damage")]
    static void EigongBoostFireDamage (ref float damageValue)
    {
        Scene activeScene = SceneManager.GetActiveScene();

        if (activeScene.name is not (SCENE_NORMAL_ENDING_EIGONG or SCENE_TRUE_ENDING_EIGONG))
            return;
        
        damageValue *= EIGONG_FIRE_BOOST;
    }
    
    [HarmonyPrefix]
    [HarmonyPatch(typeof(PlayerHealth), "ReceiveRecoverableDamage")]
    static void EigongBoostRecoverableDamage (PlayerHealth __instance, ref float damage)
    {
        Scene activeScene = SceneManager.GetActiveScene();

        if (activeScene.name is not (SCENE_NORMAL_ENDING_EIGONG or SCENE_TRUE_ENDING_EIGONG))
            return;

        damage *= EIGONG_EARLY_DEFLECT_BOOST;
    }
    
    static void BoostAttack (DamageDealer damageDealer, float damageBoost, bool revert = false)
    {
        var parentScalarSource = damageDealer.GetComponentInParent<DamageScalarSource>();
        var invoker = AccessTools.FieldRefAccess<MonsterStat, float>("BaseAttackValue");
        var valueGet = invoker.Invoke(parentScalarSource.BindMonster.monsterStat);

        if (revert)
            invoker.Invoke(parentScalarSource.BindMonster.monsterStat) = valueGet / damageBoost;
        else
            invoker.Invoke(parentScalarSource.BindMonster.monsterStat) = valueGet * damageBoost;
    }
}