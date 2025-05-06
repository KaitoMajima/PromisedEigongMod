using System;
using System.Collections.Generic;
using BepInEx.Logging;
using NineSolsAPI;
using UnityEngine.SceneManagement;

namespace PromisedEigong;

using HarmonyLib;
using I2.Loc;
using UnityEngine;
using static PromisedEigongModGlobalSettings.EigongLocs;
using static PromisedEigongModGlobalSettings.EigongTitle;
using static PromisedEigongModGlobalSettings.EigongRefs;
using static PromisedEigongModGlobalSettings.EigongAttackAnimationRefs;
using static PromisedEigongModGlobalSettings.EigongDamageBoost;

[HarmonyPatch]
public class Patches
{
    public static Action<SpriteRenderer> OnFoundTaiDanger;
    public static Action<ParticleSystem> OnFoundJieChuanFireParticles;
    public static Action<SpriteRenderer> OnFoundJieChuanFireImage;
    public static Action<SpriteRenderer> OnFoundFooExplosionSprite;
    public static Action<SpriteRenderer> OnFoundFooSprite;
    public static Action<SpriteRenderer> OnFoundCrimsonGeyserSprite;
    public static Action<SpriteRenderer> OnFoundCrimsonPillarSprite;
        
    [HarmonyPostfix]
    [HarmonyPatch(typeof(LocalizationManager), "GetTranslation")]
    static void ChangeEigongName (string Term, ref string __result)
    {
        if (Term == EIGONG_TITLE_LOC)
            __result = EIGONG_TITLE;
    }

    [HarmonyPostfix]
    [HarmonyPatch(typeof(LocalizationManager), "GetTranslation")]
    static void ChangeRootProgressText (string Term, ref string __result)
    {
        if (Term == "AG_ST_Hub/M322_AG_ST_古樹解封進度_Bubble00")
            __result = ROOT_PROGRESS_TEXT;
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
    
    [HarmonyPrefix]
    [HarmonyPatch(typeof(ApplicationCore), "Awake")]
    static bool ToastAwake (ApplicationCore __instance)
    {
        KLog.Info("KLOG: Scene that is calling Application Core in: " + __instance.gameObject.scene.name);
        return true;
    }
    
    [HarmonyPrefix]
    [HarmonyPatch(typeof(RuntimeInitHandler), "LoadCore")]
    static bool KillLogo ()
    {
        string name = SceneManager.GetActiveScene().name;
        return name != "Logo";
    }
    
    [HarmonyPrefix]
    [HarmonyPatch(typeof(GameLevel), "Awake")]
    static void AddGameLevelComponents (GameLevel __instance)
    {
        Scene activeScene = SceneManager.GetActiveScene();

        switch (activeScene.name)
        {
            case NEW_KUNLUN:
                __instance.AddComp(typeof(NewKunlunRoomChanger));
                break;
            case SCENE_NORMAL_ENDING_EIGONG or SCENE_TRUE_ENDING_EIGONG:
                __instance.AddComp(typeof(RootPinnacleBackgroundChanger));
                break;
        }
    }
    
    [HarmonyPostfix]
    [HarmonyPatch(typeof(PoolObject), "EnterLevelAwake")]
    static void PoolHijack (PoolObject __instance)
    {
        Scene activeScene = SceneManager.GetActiveScene();
        
        if (activeScene.name is not (SCENE_NORMAL_ENDING_EIGONG or SCENE_TRUE_ENDING_EIGONG))
            return;
        
        var taiDangerObjName = "Effect_TaiDanger(Clone)";

        if (__instance.name == taiDangerObjName)
            OnFoundTaiDanger?.Invoke(__instance.GetComponentInChildren<SpriteRenderer>());

        var jiechuanFire = "Fire_FX_damage_Long jiechuan(Clone)";

        if (__instance.name == jiechuanFire)
        {
            OnFoundJieChuanFireParticles?.Invoke(__instance.GetComponentInChildren<ParticleSystem>());
            var sprites = __instance.GetComponentsInChildren<SpriteRenderer>();
            foreach (var sprite in sprites)
            {
                if (sprite.name == "Light")
                    OnFoundJieChuanFireImage?.Invoke(sprite);
            }
        }

        var fooExplosionObjName = "Fx_YiGong Foo Explosion_pool obj(Clone)";
        
        if (__instance.name == fooExplosionObjName)
        {
            var sprites = __instance.GetComponentsInChildren<SpriteRenderer>(true);
            foreach (var sprite in sprites)
                OnFoundFooExplosionSprite?.Invoke(sprite);
        }

        var fooName = "Fx_YiGong 貼符(Clone)";
        
        if (__instance.name == fooName)
        {
            var sprites = __instance.GetComponentsInChildren<SpriteRenderer>(true);
            foreach (var sprite in sprites)
                OnFoundFooSprite?.Invoke(sprite);
        }
        
        var crimsonGeyserName = "Fx_YiGong Upper12_pool obj(Clone)";
        
        if (__instance.name == crimsonGeyserName)
        {
            var sprites = __instance.GetComponentsInChildren<SpriteRenderer>(true);
            foreach (var sprite in sprites)
                OnFoundCrimsonGeyserSprite?.Invoke(sprite);
        }
        
        var crimsonGeyserPillarName = "FireExplosionPillar_FX_damage(Clone)";
        
        if (__instance.name == crimsonGeyserPillarName)
        {
            var sprites = __instance.GetComponentsInChildren<SpriteRenderer>(true);
            foreach (var sprite in sprites)
                OnFoundCrimsonPillarSprite?.Invoke(sprite);
        }
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