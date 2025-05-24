using System;
using System.Collections;
using NineSolsAPI;
using PromisedEigong.ModSystem;
using RCGMaker.Core;
using UnityEngine;

namespace PromisedEigong.LevelChangers;

using static PromisedEigongModGlobalSettings.EigongRefs;

public class JudgmentCutSpawners : MonoBehaviour
{
    //TODO: Remove
    FxPlayer judgmentCutPrefab;
    FxPlayer judgmentCutPart2Prefab;
    FxPlayer judgmentCutCrimsonPrefab;
    FxPlayer judgmentCutCrimsonPart2Prefab;

    GameObject spawnedJudgmentCutPart2Obj;

    void Awake ()
    {
        var fxPlayers = FindObjectsOfType<FxPlayer>(true);

        foreach (var fxplayer in fxPlayers)
        {
            switch (fxplayer.name)
            {
                case JUDGMENT_CUT_PART_2_PREFAB_HOLDER_NAME:
                    judgmentCutPart2Prefab = fxplayer;
                    judgmentCutPart2Prefab.EmitPoolObject.SetActive(false);
                    var removeComponents1 = judgmentCutPart2Prefab.EmitPoolObject
                        .GetComponentsInChildren<CameraFollowTargetInvoker>(true);
                    foreach (var removable in removeComponents1)
                        Destroy(removable);
                    break;
            }
        }

        StartCoroutine(SpawnJudgmentCut());
    }

    void Update ()
    {
        if (spawnedJudgmentCutPart2Obj == null)
            spawnedJudgmentCutPart2Obj = judgmentCutPart2Prefab.PlayCustomAt(Vector3.zero);
    }

    IEnumerator SpawnJudgmentCut ()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.7f);
            
            if (BossPhaseProvider.CurrentPreAnimationPhase < 1)
                continue;
            
            if (spawnedJudgmentCutPart2Obj == null)
                continue;
            
            spawnedJudgmentCutPart2Obj.gameObject.SetActive(false);
            spawnedJudgmentCutPart2Obj.gameObject.SetActive(true);
        }
    }

    void OnDestroy ()
    {
        StopAllCoroutines();
    }
}