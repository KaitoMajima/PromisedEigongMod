namespace PromisedEigong.DamageBoosters;

using UnityEngine.SceneManagement;
using HarmonyLib;
using static PromisedEigongModGlobalSettings.EigongAttackAnimationRefs;
using static PromisedEigongModGlobalSettings.EigongDamageBoost;
using static PromisedEigongModGlobalSettings.EigongRefs;
using static DamageBoost;

[HarmonyPatch]
public class DamageBoostPatches
{
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

        BoostRecoverableDamage(ref damageValue, EIGONG_FIRE_BOOST);
    }

    [HarmonyPrefix]
    [HarmonyPatch(typeof(PlayerHealth), "ReceiveRecoverableDamage")]
    static void EigongBoostRecoverableDamage (PlayerHealth __instance, ref float damage)
    {
        Scene activeScene = SceneManager.GetActiveScene();

        if (activeScene.name is not (SCENE_NORMAL_ENDING_EIGONG or SCENE_TRUE_ENDING_EIGONG))
            return;

        BoostRecoverableDamage(ref damage, EIGONG_EARLY_DEFLECT_BOOST);
    }
}