using System;
using System.Collections;
using NineSolsAPI;
using PromisedEigong.LevelChangers;
using PromisedEigong.ModSystem;
using RCGMaker.Core;
using RCGMaker.Runtime.Sprite;
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
                    AttackEventType.JCAfterDoubleAttack,
                    SpawningTarget.Eigong, 
                    new Vector3(10, 0, 0), 
                    new Vector3(-10, 0, 0),
                    0.75f,
                    0.7f)
                );
                break;
            case ATTACK19_THRUST_FULL_SCREEN:
                wrapper.StartCoroutine(SpawnAttackWithDelay(
                    loadedEigong, 
                    judgmentCutSpawner.JudgmentCutPool,
                    AttackEventType.JCMirroredAfterJC,
                    SpawningTarget.Eigong, 
                    new Vector3(100, 0, 0), 
                    new Vector3(-100, 0, 0),
                    0.625f,
                    1f)
                );
                wrapper.StartCoroutine(SpawnAttackWithDelay(
                    loadedEigong, 
                    judgmentCutSpawner.JudgmentCutCrimsonPart2Pool,
                    AttackEventType.JCDoubleCrimsonEnder,
                    SpawningTarget.Eigong, 
                    new Vector3(0, 120, 0), 
                    new Vector3(-0, 120, 0),
                    1.5f,
                    1f)
                );
                break;
            case ATTACK20_TELEPORT_OUT:
                wrapper.StartCoroutine(SpawnAttackWithDelay(
                    loadedEigong, 
                    judgmentCutSpawner.JudgmentCutCrimsonPart2Pool,
                    AttackEventType.JCCrimsonOnTeleportOut,
                    SpawningTarget.Player, 
                    new Vector3(-70, 0, 0), 
                    new Vector3(70, 0, 0),
                    1f,
                    1f)
                );
                break;
            case ATTACK4_SLASH_UP when BossPhaseProvider.CurrentPostAnimationPhase is 2:
                wrapper.StartCoroutine(SpawnAttackWithDelay(
                    loadedEigong, 
                    judgmentCutSpawner.JudgmentCutPool,
                    AttackEventType.JCInterruptSlashUp,
                    SpawningTarget.Player, 
                    new Vector3(-20, -20, 0), 
                    new Vector3(20, -20, 0),
                    1.07f,
                    1f)
                );
                break;
        }
    }

    IEnumerator SpawnAttackWithDelay (
        MonsterBase loadedEigong, 
        DualStatePool<GameObject> attackPool,
        AttackEventType attackEventType,
        SpawningTarget target, 
        Vector3 offsetL, 
        Vector3 offsetR, 
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

        switch (attackEventType)
        {
            case AttackEventType.JCAfterDoubleAttack:
                HandleJCDoubleAttackLogic(attack);
                break;
            case AttackEventType.JCMirroredAfterJC:
                HandleMirroredJCAttackLogic(attack, loadedEigong);
                break;
            case AttackEventType.JCCrimsonOnTeleportOut:
                HandleJCTeleportOutLogic(attack, loadedEigong);
                break;
            case AttackEventType.JCInterruptSlashUp:
                HandleJCInterruptSlashUpLogic(attack, loadedEigong);
                break;
            case AttackEventType.JCDoubleCrimsonEnder:
                HandleJCDoubleCrimsonLogic(attack, loadedEigong);
                break;
        }

        attack.SetActive(false);
        attack.SetActive(true);
        
        yield return new WaitForSeconds(returnInterval);
        attack.SetActive(false);
        attackPool.InsertAsInactive(attack);
    }

    void HandleJCDoubleAttackLogic (GameObject attack)
    {
        var randomRotater = attack.GetComponent<RandomRotater>();
        randomRotater.minRotation = 172;
        randomRotater.maxRotation = 188;
        randomRotater.RandomRotate();
        
        var rootShadow = attack.GetComponentInChildren<PositionConstraintConsumer>(true);
        rootShadow.SetActive(false);

        var chaser = attack.GetComponentInChildren<PlayerPosChaser>(true);
        chaser.SetActive(false);
    }
    
    void HandleMirroredJCAttackLogic (GameObject attack, MonsterBase loadedEigong)
    {
        attack.transform.localScale =
            new Vector3(
                loadedEigong.Facing is Facings.Left
                    ? 1
                    : -1, attack.transform.localScale.y, attack.transform.localScale.z
                );
        var randomRotater = attack.GetComponent<RandomRotater>();
        randomRotater.enabled = false;
        
        var rootShadow = attack.GetComponentInChildren<PositionConstraintConsumer>(true);
        rootShadow.SetActive(false);

        var chaser = attack.GetComponentInChildren<PlayerPosChaser>(true);
        chaser.SetActive(false);
    }
    
    void HandleJCTeleportOutLogic (GameObject attack, MonsterBase loadedEigong)
    {
        var randomRotater = attack.GetComponent<RandomRotater>();
        randomRotater.minRotation = 22;
        randomRotater.maxRotation = 33;
        randomRotater.RandomRotate();
        
        var rootShadow = attack.GetComponentInChildren<PositionConstraintConsumer>(true);
        rootShadow.SetActive(false);

        var chaser = attack.GetComponentInChildren<PlayerPosChaser>(true);
        chaser.SetActive(false);
    }
    
    void HandleJCInterruptSlashUpLogic (GameObject attack, MonsterBase loadedEigong)
    {
        attack.transform.localScale =
            new Vector3(
                loadedEigong.Facing is Facings.Left
                    ? 1
                    : -1, attack.transform.localScale.y, attack.transform.localScale.z
            );
        var randomRotater = attack.GetComponent<RandomRotater>();
        randomRotater.enabled = false;
        
        var rootShadow = attack.GetComponentInChildren<PositionConstraintConsumer>(true);
        rootShadow.SetActive(false);

        var chaser = attack.GetComponentInChildren<PlayerPosChaser>(true);
        chaser.SetActive(false);
    }
    
    void HandleJCDoubleCrimsonLogic (GameObject attack, MonsterBase loadedEigong)
    {
        attack.transform.localScale =
            new Vector3(
                loadedEigong.Facing is Facings.Left
                    ? 1
                    : -1, attack.transform.localScale.y, attack.transform.localScale.z
            );
        
        var randomRotater = attack.GetComponent<RandomRotater>();
        randomRotater.minRotation = 133;
        randomRotater.maxRotation = 133;
        randomRotater.RandomRotate();
        
        var rootShadow = attack.GetComponentInChildren<PositionConstraintConsumer>(true);
        rootShadow.SetActive(false);

        var chaser = attack.GetComponentInChildren<PlayerPosChaser>(true);
        chaser.SetActive(false);
    }

    public void Dispose ()
    {
        RemoveListeners();
    }
}