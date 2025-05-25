using PromisedEigong.ModSystem;
using RCGMaker.Core;
using UnityEngine;

namespace PromisedEigong.LevelChangers;
#nullable disable

using static PromisedEigongModGlobalSettings.EigongRefs;

public class JudgmentCutSpawners : MonoBehaviour
{
    public DualStatePool<GameObject> JudgmentCutPool => judgmentCutPool;
    public DualStatePool<GameObject> JudgmentCutPart2Pool => judgmentCutPart2Pool;
    public DualStatePool<GameObject> JudgmentCutCrimsonPool => judgmentCutCrimsonPool;
    public DualStatePool<GameObject> JudgmentCutCrimsonPart2Pool => judgmentCutCrimsonPart2Pool;
    
    FxPlayer[] initialFxPlayers;
        
    FxPlayer judgmentCutPrefab;
    FxPlayer judgmentCutPart2Prefab;
    FxPlayer judgmentCutCrimsonPrefab;
    FxPlayer judgmentCutCrimsonPart2Prefab;

    DualStatePool<GameObject> judgmentCutPool;
    DualStatePool<GameObject> judgmentCutPart2Pool;
    DualStatePool<GameObject> judgmentCutCrimsonPool;
    DualStatePool<GameObject> judgmentCutCrimsonPart2Pool;

    bool hasUpdatedPools;

    void Awake ()
    {
        initialFxPlayers = FindObjectsOfType<FxPlayer>(true);

        foreach (var fxplayer in initialFxPlayers)
        {
            switch (fxplayer.name)
            {
                case JUDGMENT_CUT_PREFAB_HOLDER_NAME:
                    judgmentCutPrefab = fxplayer;
                    break;
                case JUDGMENT_CUT_PART_2_PREFAB_HOLDER_NAME:
                    judgmentCutPart2Prefab = fxplayer;
                    break;
                case JUDGMENT_CUT_CRIMSON_PREFAB_HOLDER_NAME:
                    judgmentCutCrimsonPrefab = fxplayer;
                    break;
                case JUDGMENT_CUT_CRIMSON_PART_2_PREFAB_HOLDER_NAME:
                    judgmentCutCrimsonPart2Prefab = fxplayer;
                    break;
            }
        }
    }

    void Update ()
    {
        if (hasUpdatedPools)
            return;
        
        judgmentCutPool = CreatePool(judgmentCutPrefab, JUDGMENT_CUT_NEW_NAME);
        judgmentCutPart2Pool = CreatePool(judgmentCutPart2Prefab, JUDGMENT_CUT_PART_2_NEW_NAME);
        judgmentCutCrimsonPool = CreatePool(judgmentCutCrimsonPrefab, JUDGMENT_CUT_CRIMSON_NEW_NAME);
        judgmentCutCrimsonPart2Pool = CreatePool(judgmentCutCrimsonPart2Prefab, JUDGMENT_CUT_CRIMSON_PART_2_NEW_NAME);
        hasUpdatedPools = true;
    }

    DualStatePool<GameObject> CreatePool (FxPlayer prefabPlayer, string objName)
    {
        var pool = new DualStatePool<GameObject>();
        var emitPoolObj = prefabPlayer.EmitPoolObject;
        emitPoolObj.SetActive(false);
        
        for (int i = 0; i < 10; i++)
        {
            var obj = prefabPlayer.PlayCustomAt(Vector3.zero);
            obj.name = objName;
            CameraInvokerActivator(obj, false);
            obj.GetComponent<PoolObject>().enabled = false;
            obj.AddComponent<CustomPoolObject>();
            obj.SetActive(false);
            pool.InsertAsInactive(obj);
        }
        return pool;
    }
    
    void CameraInvokerActivator (GameObject gameObj, bool active)
    {
        var invokers = gameObj.GetComponentsInChildren<CameraFollowTargetInvoker>(true);
        foreach (var invoker in invokers)
            invoker.enabled = active;
    }

    void OnDestroy ()
    {
        foreach (var judgmentCut in judgmentCutPool.AllItemsSet)
            Destroy(judgmentCut);
        foreach (var judgmentCut in judgmentCutCrimsonPool.AllItemsSet)
            Destroy(judgmentCut);
        foreach (var judgmentCut in judgmentCutPart2Pool.AllItemsSet)
            Destroy(judgmentCut);
        foreach (var judgmentCut in judgmentCutCrimsonPart2Pool.AllItemsSet)
            Destroy(judgmentCut);
    }
}