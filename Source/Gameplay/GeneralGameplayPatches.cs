namespace PromisedEigong.Gameplay;

using HarmonyLib;
using static PromisedEigongModGlobalSettings.EigongRefs;

public delegate void OnAttackEnterHandler (BossGeneralState previousState, BossGeneralState currentState);

[HarmonyPatch]
public class GeneralGameplayPatches
{
    public static OnAttackEnterHandler? OnAttackEnterCalled;
    
    static MonsterState previousState;
    
    [HarmonyPrefix]
    [HarmonyPatch(typeof(MonsterState), "OnStateEnter")]
    public static void OnStateEnterCall (MonsterState __instance)
    {
        if (__instance.monster.name != BIND_MONSTER_EIGONG_NAME)
            return;
        
        if (previousState == null ||
            previousState is not BossGeneralState previousBossState ||
            __instance is not BossGeneralState currentBossState)
        {
            previousState = __instance;
            return;
        }
        
        OnAttackEnterCalled?.Invoke(previousBossState, currentBossState);
        previousState = __instance;
    }
}