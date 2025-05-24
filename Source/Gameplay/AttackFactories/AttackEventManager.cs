using System;
using System.Collections;
using PromisedEigong.LevelChangers;
using PromisedEigong.ModSystem;
using UnityEngine;

namespace PromisedEigong.Gameplay.AttackFactories;
#nullable disable
using static PromisedEigongModGlobalSettings.EigongAttacks;

public class AttackEventManager
{
    EigongWrapper wrapper;
    JudgmentCutSpawners judgmentCutSpawner;

    public void Setup (EigongWrapper wrapper, JudgmentCutSpawners judgmentCutSpawner)
    {
        this.wrapper = wrapper;
        this.judgmentCutSpawner = judgmentCutSpawner;
    }

    public void Initialize ()
    {
        AddListeners();
    }

    void AddListeners ()
    {
        GeneralGameplayPatches.OnAttackStartCalled += HandleAttackChanged;
    }

    void RemoveListeners ()
    {
        GeneralGameplayPatches.OnAttackStartCalled -= HandleAttackChanged;
    }

    void HandleAttackChanged (BossStateIdentifier currentState)
    {
        var loadedEigong = wrapper.LoadedEigong;
        
        switch (currentState.IdName)
        {
            case ATTACK6_DOUBLE_ATTACK when BossPhaseProvider.CurrentPostAnimationPhase is 2:
                wrapper.StartCoroutine(SpawnAttackWithDelay(
                    loadedEigong, 
                    judgmentCutSpawner.JudgmentCutPart2Pool,
                    SpawningTarget.Eigong, 
                    new Vector3(10, 0 ,0), 
                    new Vector3(-10, 0 ,0),
                    172,
                    188,
                    0.7f,
                    0.7f)
                );
                break;
        }
    }

    IEnumerator SpawnAttackWithDelay (
        MonsterBase loadedEigong, 
        DualStatePool<GameObject> attackPool, 
        SpawningTarget target, 
        Vector3 offsetL, 
        Vector3 offsetR, 
        float randRotationMin,
        float randRotationMax,
        float delay,
        float returnInterval
    )
    {
        yield return new WaitForSeconds(delay);

        var attack = attackPool.Fetch();
        if (attack == null)
            yield break;
        
        attackPool.InsertAsActive(attack);

        switch (target)
        {
            case SpawningTarget.Player:
                var player = Player.i;
                var isPlayerFacingRight = player.Facing is Facings.Right;
                attack.transform.position =
                    isPlayerFacingRight
                        ? player.gameObject.transform.position + offsetL
                        : player.gameObject.transform.position + offsetR
                    ;
                break;
            case SpawningTarget.Eigong:
                var isEigongFacingRight = loadedEigong.Facing is Facings.Right;
                attack.transform.position =
                    isEigongFacingRight
                        ? loadedEigong.gameObject.transform.position + offsetL
                        : loadedEigong.gameObject.transform.position + offsetR
                    ;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(target), target, null);
        }

        var randomRotater = attack.GetComponent<RandomRotater>();
        if (randomRotater != null)
        {
            randomRotater.minRotation = randRotationMin;
            randomRotater.maxRotation = randRotationMax;
            randomRotater.RandomRotate();
        }

        attack.SetActive(false);
        attack.SetActive(true);
        
        yield return new WaitForSeconds(returnInterval);
        attack.SetActive(false);
        attackPool.InsertAsInactive(attack);
    }

    public void Dispose ()
    {
        RemoveListeners();
    }
}