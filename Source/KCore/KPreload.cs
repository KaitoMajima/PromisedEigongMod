//Thanks to jakobhellermann for the preloading logic!

namespace PromisedEigong.Core;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using NineSolsAPI;
using NineSolsAPI.Preload;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KPreload (Action<float> onProgress)
{
    public static bool IsPreloading;
    
    Dictionary<string, List<(string, IPreloadTarget)>> preloadTypes = new();
    bool preloaded;
    List<(GameObject, IPreloadTarget)> preloadObjs = new();
    List<AsyncOperation> preloadOperationQueue = new();
    List<AsyncOperation> inProgressLoads = new();
    List<AsyncOperation> inProgressUnloads = new();
    int target;

    public void AddPreload (string scene, string path, IPreloadTarget target)
    {
        List<(string, IPreloadTarget)> valueTupleList;
        if (!preloadTypes.TryGetValue(scene, out valueTupleList))
        {
            valueTupleList = new List<(string, IPreloadTarget)>();
            preloadTypes.Add(scene, valueTupleList);
        }

        valueTupleList.Add((path, target));
    }

    public void AddPreloadList (IEnumerable<(string, string)> paths, List<GameObject?> outList)
    {
        IPreloadTarget.ListPreloadTarget target = new IPreloadTarget.ListPreloadTarget(outList);
        foreach ((string, string) path in paths)
            AddPreload(path.Item1, path.Item2, target);
    }

    public void AddPreloadClass<T> (T obj)
    {
        if (IsPreloading)
        {
            KLog.Error("tried to call AddPreloadClass during preloading");
        }
        else
        {
            var fieldInfos = obj.GetType()
                .GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
            
            foreach (FieldInfo field in fieldInfos)
            {
                PreloadAttribute customAttribute = field.GetCustomAttribute<PreloadAttribute>();
                if (customAttribute != null)
                {
                    AddPreload(customAttribute.Scene, customAttribute.Path,
                        new IPreloadTarget.ReflectionPreloadTarget(obj, field));
                }
            }
        }
    }

    IEnumerator DoPreloadScene (string sceneName, List<(string, IPreloadTarget)> scenePreloads)
    {
        AsyncOperation loadOp = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        if (loadOp == null)
        {
            ToastManager.Toast("Error loading scene: " + sceneName);
            --target;
        }
        else
        {
            preloadOperationQueue.Add(loadOp);
            inProgressLoads.Add(loadOp);
            yield return loadOp;
            Scene sceneByName = SceneManager.GetSceneByName(sceneName);
            try
            {
                GameObject[] rootGameObjects = sceneByName.GetRootGameObjects();
                foreach (GameObject gameObject in rootGameObjects)
                    gameObject.SetActive(false);
                foreach ((string str, IPreloadTarget preloadTarget) in scenePreloads)
                {
                    GameObject gameObjectFromArray = KUtils.GetGameObjectFromArray(rootGameObjects, str);
                    if (gameObjectFromArray == null)
                    {
                        KLog.Error($"could not preload {str} in {sceneName}");
                        preloadTarget.Set(null, sceneName, str);
                    }
                    else
                    {
                        GameObject gameObject = UnityEngine.Object.Instantiate(gameObjectFromArray);
                        gameObject.SetActive(false);
                        UnityEngine.Object.DontDestroyOnLoad(gameObject);
                        AutoAttributeManager.AutoReference(gameObject);
                        preloadObjs.Add((gameObject, preloadTarget));
                        preloadTarget.Set(gameObject, sceneName, str);
                    }
                }
            }
            catch (Exception ex)
            {
                KLog.Error(ex);
            }

            AsyncOperation asyncOperation = SceneManager.UnloadSceneAsync(sceneName);
            inProgressUnloads.Add(asyncOperation);
            yield return asyncOperation;
            preloadOperationQueue.Remove(loadOp);
        }
    }

    IEnumerator DoPreload ()
    {
        if (preloadTypes.Count == 0) 
            yield break;
        
        KLog.Info($"Preloading {preloadTypes.Count} scenes");
        Stopwatch watch = Stopwatch.StartNew();
        IsPreloading = true;
        DestroyAllGameObjects.DestroyingAll = true;
        try
        {
            target = preloadTypes.Count;
            Dictionary<string, List<(string, IPreloadTarget)>>.Enumerator preloadsToDo =
                preloadTypes.GetEnumerator();
            float num1 = 0.0f;
            while (num1 < 1.0)
            {
                while (preloadOperationQueue.Count < 4 && preloadsToDo.MoveNext())
                {
                    (string str, List<(string, IPreloadTarget)> scenePreloads) = preloadsToDo.Current;
                    KCore.Main.StartCoroutine(DoPreloadScene(str, scenePreloads));
                    
                    if (inProgressLoads.Count % 4 != 0) 
                        continue;
                    
                    Stopwatch watchRes = Stopwatch.StartNew();
                    yield return Resources.UnloadUnusedAssets();
                    watchRes.Stop();
                    KLog.Info($"collecting resources in ${watchRes.ElapsedMilliseconds}");
                }

                yield return null;
                float num2 =
                    inProgressLoads.Sum(
                        loadOp => loadOp.progress * 0.5f) +
                    inProgressUnloads.Sum(
                        loadOp => loadOp.progress * 0.5f);
                num1 = target == 0 ? 1f : num2 / target;
                onProgress(num1);
                KLog.Info($"progress {num1}/1, in flight {preloadOperationQueue.Count}");
            }
        }
        finally
        {
            DestroyAllGameObjects.DestroyingAll = false;
            IsPreloading = false;
            preloaded = true;
            inProgressLoads.Clear();
            inProgressUnloads.Clear();
            preloadOperationQueue.Clear();
        }

        watch.Stop();
        KLog.Info($"Preloading done with {preloadObjs.Count} objects in {watch.ElapsedMilliseconds}ms");
    }

    internal IEnumerator Preload ()
    {
        if (!preloaded || SceneManager.GetActiveScene().name == "TitleScreenMenu")
            yield return DoPreload();
        onProgress(1f);
    }

    internal void Unload ()
    {
        foreach ((GameObject gameObject, IPreloadTarget preloadTarget) in preloadObjs)
        {
            if ((bool)(UnityEngine.Object)gameObject)
                UnityEngine.Object.Destroy(gameObject);
            GameObject preloaded = gameObject;
            preloadTarget.Unset(preloaded);
        }

        preloadObjs.Clear();
    }
}