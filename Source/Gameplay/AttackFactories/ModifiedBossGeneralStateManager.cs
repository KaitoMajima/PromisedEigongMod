using System.Linq;
using NineSolsAPI;
using UnityEngine;

namespace PromisedEigong.Gameplay.AttackFactories;

public class ModifiedBossGeneralStateManager : MonoBehaviour
{
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
        var currentModifiedAttack = allModifiedStates.FirstOrDefault(x => x.OriginalName == currentState.IdName);
        
        if (currentModifiedAttack != null)
        {
            ToastManager.Toast($"Previous state was {previousState.IdName} for {currentState.IdName}.");
            if (currentModifiedAttack.CanBeModified(previousState))
            {
                ToastManager.Toast($"Seems like I can modify it!");
                currentModifiedAttack.ApplyMoveModification();
            }
            else
            {
                ToastManager.Toast($"...But I can't modify it from here.");
            }
        }
            
        if (previousModifiedAttack != null)
            previousModifiedAttack.ApplyMoveRevert();
    }
    
    void OnDestroy ()
    {
        RemoveListeners();
    }
}