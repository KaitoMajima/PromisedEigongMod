﻿namespace PromisedEigong.DamageAbsorbers;
#nullable disable

using HarmonyLib;
using static PromisedEigongModGlobalSettings.EigongRefs;
using static PromisedEigongModGlobalSettings.EigongProperties;
using static DamageReduction;

[HarmonyPatch]
public class DamageAbsorbPatches
{
    [HarmonyPrefix]
    [HarmonyPatch(typeof(PostureSystem), "InternalInjuryDecreasePosture")]
    static void EigongDecreaseReducedPosture (PostureSystem __instance, ref float value)
    {
        if (__instance.BindMonster.Name != BIND_MONSTER_EIGONG_NAME)
            return;

        value *= GetReducedPostureDamage();
    }
    
    [HarmonyPrefix]
    [HarmonyPatch(typeof(PostureSystem), "TakeDamage")]
    static void EigongTakeReducedDamage (PostureSystem __instance, ref float value, bool isConsumeInjury = false, EffectDealer dealer = null)
    {
        if (__instance.BindMonster.Name != BIND_MONSTER_EIGONG_NAME)
            return;
        
        if (dealer == null)
            return;
        
        value *= GetReducedDirectDamage(dealer.detailType);
    }
    
    [HarmonyPrefix]
    [HarmonyPatch(typeof(MonsterBase), "HurtInterruptCheck")]
    static bool EigongForbidStun (MonsterBase __instance, MonsterBase.States targetState = MonsterBase.States.Hurt)
    {
        if (__instance.Name == BIND_MONSTER_EIGONG_NAME)
            return !EIGONG_FORBID_STUNNING;
        
        return true;
    }
}