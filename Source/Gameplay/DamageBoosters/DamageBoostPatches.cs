namespace PromisedEigong.DamageBoosters;

using UnityEngine.SceneManagement;
using HarmonyLib;
using static PromisedEigongModGlobalSettings.EigongAttackAnimationRefs;
using static PromisedEigongModGlobalSettings.EigongDamageBoost;
using static PromisedEigongModGlobalSettings.EigongRefs;
using static PromisedEigongModGlobalSettings.EigongDebug;
using static DamageBoost;

[HarmonyPatch]
public class DamageBoostPatches
{
    [HarmonyPrefix]
    [HarmonyPatch(typeof(PlayerHealth), "ReceiveDamage")]
    static bool EigongBoostDamage (PlayerHealth __instance, ref DamageDealer damageDealer)
    {
        if (TEST_YI_INVINCIBLE)
            return false;
        
        if (damageDealer.Owner == null)
            return true;
        
        if (damageDealer.Owner.name != BIND_MONSTER_EIGONG_NAME)
            return true;
        
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

        return true;
    }

    [HarmonyPostfix]
    [HarmonyPatch(typeof(PlayerHealth), "ReceiveDamage")]
    static void RevertBoostedDamage (PlayerHealth __instance, ref DamageDealer damageDealer)
    {
        if (TEST_YI_INVINCIBLE)
            return;
        
        if (damageDealer.Owner == null)
            return;
        
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
    static bool EigongBoostFireDamage (ref float damageValue)
    {
        if (TEST_YI_INVINCIBLE)
            return false;
        
        Scene activeScene = SceneManager.GetActiveScene();
        
        if (activeScene.name is not (SCENE_NORMAL_ENDING_EIGONG or SCENE_TRUE_ENDING_EIGONG))
            return true;
        
        BoostRecoverableDamage(ref damageValue, EIGONG_FIRE_BOOST);
        
        return true;
    }

    [HarmonyPrefix]
    [HarmonyPatch(typeof(PlayerHealth), "ReceiveRecoverableDamage")]
    static bool EigongBoostRecoverableDamage (PlayerHealth __instance, ref float damage)
    {
        if (TEST_YI_INVINCIBLE)
            return false;
        
        Scene activeScene = SceneManager.GetActiveScene();
        
        if (activeScene.name is not (SCENE_NORMAL_ENDING_EIGONG or SCENE_TRUE_ENDING_EIGONG))
            return true;
        
        BoostRecoverableDamage(ref damage, EIGONG_EARLY_DEFLECT_BOOST);

        return true;
    }
}