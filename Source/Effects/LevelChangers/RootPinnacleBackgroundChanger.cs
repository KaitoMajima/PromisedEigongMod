using System;
using System.Collections;
using NineSolsAPI;

namespace PromisedEigong.LevelChangers;

using ModSystem;
using PreloadObjectHandlers;
using BlendModes;
using UnityEngine;
using static PromisedEigongModGlobalSettings.EigongBackground;
using static PromisedEigongModGlobalSettings.EigongRefs;

public class RootPinnacleBackgroundChanger : MonoBehaviour
{
    PromisedEigongMain MainInstance => PromisedEigongMain.Instance;
    WhiteScreen WhiteScreen => MainInstance.WhiteScreen;
    GameObject? PreloadingManagerBigBad => MainInstance.PreloadingManager.BigBad;

    EigongWrapper eigongWrapper;

    GameObject instantiatedBigBad;
    
    void Awake ()
    {
        SpawnBigBad();
        ChangeBackground();
        AddListeners();
    }

    void AddListeners ()
    {
        MainInstance.OnEigongWrapperUpdated += HandleEigongWrapperUpdated;
    }
    
    void RemoveListeners ()
    {
        MainInstance.OnEigongWrapperUpdated -= HandleEigongWrapperUpdated;
    }
    
    void SpawnBigBad ()
    {
        if (PreloadingManagerBigBad == null)
            return;
        Transform bgMasterTransform = 
            GameObject.Find(BG_MASTER_TRANSFORM).transform;
        instantiatedBigBad = Instantiate(PreloadingManagerBigBad, bgMasterTransform);
        instantiatedBigBad.transform.localPosition = Vector3.zero;
        instantiatedBigBad.AddComponent<BigBadHandler>();
    }

    void ChangeBackground ()
    {
        var animatorRef = GameObject.Find(ANIMATOR_REF);
        animatorRef.GetComponent<Animator>().enabled = false;

        var redFade = GameObject.Find(RED_FX);
        var greenFade = GameObject.Find(GREEN_FX);
        var blackFade = GameObject.Find(BLACK_FX);
        var black2Fade = GameObject.Find(BLACK_2_FX);
        var blueFade = GameObject.Find(BLUE_FX);
        var donutFade = GameObject.Find(DONUT_FX);
        var platformLightFade = GameObject.Find(PLATFORM_LIGHT_FX);
        var giantBall = GameObject.Find(GIANT_BALL);
        var coreVineParent = GameObject.Find(CORE_VINES_PARENT);

        donutFade.SetActive(true);

        redFade.GetComponent<SpriteRenderer>().color = RED_COLOR;
        redFade.GetComponent<BlendModeEffect>().BlendMode = RED_BLEND_MODE;
        greenFade.GetComponent<SpriteRenderer>().color = GREEN_COLOR;
        blackFade.GetComponent<SpriteRenderer>().color = BLACK_COLOR;
        black2Fade.GetComponent<SpriteRenderer>().color = BLACK_2_COLOR;
        blueFade.GetComponent<SpriteRenderer>().color = BLUE_COLOR;
        blueFade.GetComponent<BlendModeEffect>().BlendMode = BLUE_BLEND_MODE;
        donutFade.GetComponent<SpriteRenderer>().color = DONUT_COLOR;
        platformLightFade.GetComponent<Light>().color = PLATFORM_LIGHT_COLOR;
        giantBall.GetComponent<Animator>().speed = GIANT_BALL_SPEED_1;
        var coreVines = coreVineParent.GetComponentsInChildren<Animator>();
        foreach (var animator in coreVines)
            animator.speed = CORE_VINES_SPEED_1;
    }
    
    void HandleEigongWrapperUpdated ()
    {
        if (eigongWrapper != null)
            eigongWrapper.OnCurrentEigongPhaseChanged -= HandleEigongPhaseUpdated;

        eigongWrapper = MainInstance.EigongWrapper;
        eigongWrapper.OnCurrentEigongPhaseChanged += HandleEigongPhaseUpdated;
    }

    void HandleEigongPhaseUpdated (int phaseIndex)
    {
        if (phaseIndex == 1)
        {
            var giantBall = GameObject.Find(GIANT_BALL);
            var coreVineRoot = GameObject.Find(CORE_VINES_ROOT);
            var coreVineParent = GameObject.Find(CORE_VINES_PARENT);
            var infectedVineParent = GameObject.Find(CORE_VINES_INFECTED_PARENT);
            var noOutlineMaterial = coreVineParent.GetComponentInChildren<SkinnedMeshRenderer>().material;
            var infectedVines = infectedVineParent.GetComponentsInChildren<SkinnedMeshRenderer>();
            
            foreach (var vineRenderer in infectedVines)
                vineRenderer.material = noOutlineMaterial;
            
            giantBall.GetComponent<Animator>().speed = GIANT_BALL_SPEED_2;
            giantBall.GetComponent<Animator>().Play("[Cutscene] Fight End", 0, 0.45f);
            coreVineRoot.GetComponent<Animator>().enabled = false;
            coreVineParent.SetActive(false);
            infectedVineParent.SetActive(true);
        }

        if (phaseIndex == 2)
        {
            StopAllCoroutines();
            StartCoroutine(ApplyPhase3Effects());
        }
    }

    IEnumerator ApplyPhase3Effects ()
    {
        WhiteScreen.Show();
        yield return new WaitForSeconds(WhiteScreen.ShowDuration);
        var giantBall = GameObject.Find(GIANT_BALL);
        var infectedVineParent = GameObject.Find(CORE_VINES_INFECTED_PARENT);
        var infectedVineParentFinal = GameObject.Find(CORE_VINES_INFECTED_PARENT_FINAL);
        giantBall.SetActive(false);
        infectedVineParent.SetActive(false);
        infectedVineParentFinal.SetActive(true);
        instantiatedBigBad.SetActive(true);
        yield return new WaitForSeconds(WhiteScreen.HideDuration);
        WhiteScreen.Hide();
    }

    void OnDestroy ()
    {
        RemoveListeners();
    }
}