namespace PromisedEigong.Gameplay.AttackFactories;
#nullable disable

using System.Linq;
using NineSolsAPI;
using UnityEngine;


public class ModifiedBossGeneralStateManager : MonoBehaviour
{
    public static bool IsCurrentAttackAChain { get; private set; }
    
    ModifiedBossGeneralState[] allModifiedStates;
    
    void Awake ()
    {
        allModifiedStates = FindObjectsOfType<ModifiedBossGeneralState>();
        AddListeners();
    }

    void AddListeners ()
    {
        GeneralGameplayPatches.OnAttackEnterCalled += HandleAttackEnter;
    }

    void RemoveListeners ()
    {
        GeneralGameplayPatches.OnAttackEnterCalled -= HandleAttackEnter;
    }
    
    void HandleAttackEnter (BossStateIdentifier previousState, BossStateIdentifier currentState)
    {
        var previousModifiedAttack = allModifiedStates.FirstOrDefault(x => x.ModifiedName == previousState.IdName);
        var attacksThatCanBeModified = allModifiedStates.Where(x => x.OriginalName == currentState.IdName).ToList();
        
        if (attacksThatCanBeModified.Count > 0)
        {
            bool hasFoundAttack = false;
            
            foreach (var attack in attacksThatCanBeModified)
            {
                if (!attack.CanBeModified(previousState))
                    continue;
                
                attack.ApplyMoveModification();
                IsCurrentAttackAChain = attack.IsFromAChain;
                hasFoundAttack = true;
                break;
            }

            if (!hasFoundAttack)
                IsCurrentAttackAChain = false;
        }
        else
        {
            IsCurrentAttackAChain = false;
        }
            
        if (previousModifiedAttack != null)
            previousModifiedAttack.ApplyMoveRevert();
    }
    
    void OnDestroy ()
    {
        RemoveListeners();
    }
}