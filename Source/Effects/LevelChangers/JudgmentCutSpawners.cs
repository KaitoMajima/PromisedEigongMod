using System.Collections;
using System.Linq;
using NineSolsAPI;
using PromisedEigong.ModSystem;
using RCGMaker.Core;
using UnityEngine;

namespace PromisedEigong.LevelChangers;
#nullable disable

using static PromisedEigongModGlobalSettings.EigongRefs;

public class JudgmentCutSpawners : MonoBehaviour
{
    static readonly string[] judgmentCutNames =
    [
        JUDGMENT_CUT_PREFAB_HOLDER_NAME, 
        JUDGMENT_CUT_PART_2_PREFAB_HOLDER_NAME,
        JUDGMENT_CUT_CRIMSON_PREFAB_HOLDER_NAME, 
        JUDGMENT_CUT_CRIMSON_PART_2_PREFAB_HOLDER_NAME
    ];

    FxPlayer[] initialFxPlayers;
        
    FxPlayer judgmentCutPrefab;
    FxPlayer judgmentCutPart2Prefab;
    FxPlayer judgmentCutCrimsonPrefab;
    FxPlayer judgmentCutCrimsonPart2Prefab;

    GameObject spawnedJudgmentCutObj;
    GameObject spawnedJudgmentCutPart2Obj;
    GameObject spawnedJudgmentCutCrimsonObj;
    GameObject spawnedJudgmentCutCrimsonPart2Obj;
    
    float spawnDelay = 0.5f;
    bool revertedCamera;

    void Awake ()
    {
        initialFxPlayers = FindObjectsOfType<FxPlayer>(true);

        foreach (var fxplayer in initialFxPlayers)
        {
            switch (fxplayer.name)
            {
                case JUDGMENT_CUT_PREFAB_HOLDER_NAME:
                    judgmentCutPrefab = fxplayer;
                    judgmentCutPrefab.EmitPoolObject.SetActive(false);
                    CameraInvokerActivator(judgmentCutPrefab, false);
                    break;
                case JUDGMENT_CUT_PART_2_PREFAB_HOLDER_NAME:
                    judgmentCutPart2Prefab = fxplayer;
                    judgmentCutPart2Prefab.EmitPoolObject.SetActive(false);
                    CameraInvokerActivator(judgmentCutPart2Prefab, false);
                    break;
                case JUDGMENT_CUT_CRIMSON_PREFAB_HOLDER_NAME:
                    judgmentCutCrimsonPrefab = fxplayer;
                    judgmentCutCrimsonPrefab.EmitPoolObject.SetActive(false);
                    CameraInvokerActivator(judgmentCutCrimsonPrefab, false);
                    break;
                case JUDGMENT_CUT_CRIMSON_PART_2_PREFAB_HOLDER_NAME:
                    judgmentCutCrimsonPart2Prefab = fxplayer;
                    judgmentCutCrimsonPart2Prefab.EmitPoolObject.SetActive(false);
                    CameraInvokerActivator(judgmentCutCrimsonPart2Prefab, false);
                    break;
            }
        }

        StartCoroutine(SpawnJudgmentCut());
    }

    void Update ()
    {
        if (spawnDelay > 0)
        {
            spawnDelay -= Time.deltaTime;
            return;
        }
        TryInstantiatePrefabInstance(ref spawnedJudgmentCutObj, judgmentCutPrefab);
        TryInstantiatePrefabInstance(ref spawnedJudgmentCutPart2Obj, judgmentCutPart2Prefab);
        TryInstantiatePrefabInstance(ref spawnedJudgmentCutCrimsonObj, judgmentCutCrimsonPrefab);
        TryInstantiatePrefabInstance(ref spawnedJudgmentCutCrimsonPart2Obj, judgmentCutCrimsonPart2Prefab);
        TryRevertingCameraInvokers();
    }

    IEnumerator SpawnJudgmentCut ()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.7f);
            
            if (BossPhaseProvider.CurrentPostAnimationPhase < 2)
                continue;
            
            if (spawnedJudgmentCutPart2Obj == null)
                continue;
            
            spawnedJudgmentCutPart2Obj.gameObject.SetActive(false);
            spawnedJudgmentCutPart2Obj.gameObject.SetActive(true);
        }
    }

    void TryInstantiatePrefabInstance (ref GameObject prefabInstance, FxPlayer prefabPlayer)
    {
        if (prefabInstance == null)
        {
            prefabInstance = prefabPlayer.PlayCustomAt(Vector3.zero);
            prefabInstance.SetActive(false);
        }
    }

    void TryRevertingCameraInvokers ()
    {
        if (revertedCamera)
            return;
        
        if (spawnedJudgmentCutObj == null || 
            spawnedJudgmentCutPart2Obj == null || 
            spawnedJudgmentCutCrimsonObj == null || 
            spawnedJudgmentCutPart2Obj == null
           )
            return;
        
        foreach (var fxplayer in initialFxPlayers)
        {
            if (judgmentCutNames.Contains(fxplayer.name))
                CameraInvokerActivator(fxplayer, true);
        }
        revertedCamera = true;
    }
    
    void CameraInvokerActivator (FxPlayer prefabPlayer, bool active)
    {
        var invokers = prefabPlayer.EmitPoolObject
            .GetComponentsInChildren<CameraFollowTargetInvoker>(true);
        foreach (var invoker in invokers)
            invoker.SetActive(active);
    }

    void OnDestroy ()
    {
        StopAllCoroutines();
    }
}