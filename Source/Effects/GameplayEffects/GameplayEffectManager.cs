using System.Collections;
using NineSolsAPI;
using PromisedEigong.AppearanceChangers;

namespace PromisedEigong.Effects.GameplayEffects;
#nullable disable

using System;
using Gameplay;
using Gameplay.AttackFactories;
using ModSystem;
using UnityEngine;
using static PromisedEigongModGlobalSettings.EigongVFX;
using static PromisedEigongModGlobalSettings.EigongSFX;
using static PromisedEigongModGlobalSettings.EigongAttacks;
using static PromisedEigongModGlobalSettings.EigongColors;

public class GameplayEffectManager : IDisposable
{
    MonsterBase loadedEigong;
    ICoroutineRunner runner;
    EigongWrapper wrapper;
    
    public void Setup (MonsterBase loadedEigong, EigongWrapper wrapper, ICoroutineRunner runner)
    {
        this.loadedEigong = loadedEigong;
        this.runner = runner;
        this.wrapper = wrapper;
    }
    
    public void Initialize ()
    {
        AddListeners();
    }

    void AddListeners ()
    {
        GeneralGameplayPatches.OnAttackStartCalled += HandleEigongStateChanged;
        wrapper.OnEigongTransformed += HandleEigongTransformed;
    }

    void RemoveListeners ()
    {
        GeneralGameplayPatches.OnAttackStartCalled -= HandleEigongStateChanged;
        wrapper.OnEigongTransformed -= HandleEigongTransformed;
    }
    
    void HandleEigongStateChanged (BossStateIdentifier currentState)
    {
        if (currentState.IdName == ATTACK9_STARTER)
        {
            PlaySoundEffect(SIMPLE_DANGER_SFX_PATH, STARTER_SIMPLE_DANGER_SFX_BOOST);
            SpawnVFX(
                DANGER_VFX_PATH, 
                SIMPLE_DANGER_VFX_NAME, 
                STARTER_SIMPLE_DANGER_VFX_OFFSET_L,
                STARTER_SIMPLE_DANGER_VFX_OFFSET_R, 
                SIMPLE_DANGER_VFX_SCALE, 
                SIMPLE_DANGER_VFX_HIDE_TIME
            );
        }
        if (currentState.IdName == ATTACK22_NEW_CHAIN_SLOW_STARTER)
        {
            runner.StartCoroutine(SlowStarterMultipleVFX(0.15f));
        }
    }
    
    void HandleEigongTransformed ()
    {
        var eigongBody = GameObject.Find(EIGONG_CHARACTER_BODY);
        GameObject.Destroy(eigongBody.GetComponent<_2dxFX_BurningFX>());
        var eigongBodyRenderer = eigongBody.GetComponent<SpriteRenderer>();
        var eigongTransformOverlayObj = GameObject.Instantiate(new GameObject(), eigongBody.transform);
        var spriteFollower = eigongTransformOverlayObj.AddComponent<SpriteFollower>();
        var spriteSetter = eigongTransformOverlayObj.AddComponent<SpriteSetter>();
        var colorChange = eigongBody.AddComponent<_2dxFX_AL_ColorChange>();
        var overlay = eigongTransformOverlayObj.AddComponent<_2dxFX_AL_4Gradients>();
        spriteFollower.followRenderer = eigongBodyRenderer;
        spriteSetter.ReferenceSpriteRenderer = eigongBodyRenderer;
        colorChange._Saturation = 0;
        colorChange._ValueBrightness = 4.7f;
        //TODO: Change Saturation
        overlay._Color1 = new Color(1, 0, 0, 1);
        overlay._Color2 = new Color(1, 0, 0, 1);
        overlay._Color3 = new Color(1, 0, 0.5f, 1);
        overlay._Color4 = new Color(1, 0, 0.5, 1);
        overlay.BlendMode = 3;
        overlay._Alpha = 2.7f;
    }
    
    void PlaySoundEffect (string sfxPath, int boost)
    {
        var simpleDangerSFX = GameObject.Find(sfxPath);
        var soundPlayer = simpleDangerSFX.GetComponent<SoundPlayer>();
        for (int i = 0; i < boost; i++)
            soundPlayer.SimplePlay();
    }
    
    void SpawnVFX (string vfxPath, string vfxName, Vector3 vfxOffsetL, Vector3 vfxOffsetR, Vector3 localScale, float hideTime)
    {
        var dangerVFX = GameObject.Find(vfxPath);
        var dangerVFXPoolObj = dangerVFX.GetComponent<FxPlayer>().EmitPoolObject;
        //TODO: Pool
        var isEigongFacingRight = loadedEigong.Facing is Facings.Right;
        var dangerVFXInstance = GameObject.Instantiate(dangerVFXPoolObj.gameObject,
            isEigongFacingRight
                ? loadedEigong.gameObject.transform.position + vfxOffsetL
                : loadedEigong.gameObject.transform.position + vfxOffsetR, 
            Quaternion.identity);
        GameObject.Destroy(dangerVFXInstance.GetComponent<PoolObject>());
        var dangerVFXPoolObject = dangerVFXInstance.AddComponent<CustomPoolObject>();
        dangerVFXInstance.transform.localScale = localScale;
        dangerVFXInstance.name = vfxName;
        var spriteRenderers = dangerVFXInstance.GetComponentsInChildren<SpriteRenderer>();
            
        foreach (var spriteRenderer in spriteRenderers)
            spriteRenderer.forceRenderingOff = true;
        runner.StartCoroutine(ShowAfterDelay(hideTime, dangerVFXPoolObject, spriteRenderers));
    }

    IEnumerator SlowStarterMultipleVFX (float interval)
    {
        PlaySoundEffect(SIMPLE_DANGER_SFX_PATH, SLOW_STARTER_SIMPLE_DANGER_SFX_BOOST_1);
        SpawnVFX(
            DANGER_VFX_PATH, 
            SIMPLE_DANGER_VFX_NAME, 
            SLOW_STARTER_SIMPLE_DANGER_VFX_OFFSET_L_1,
            SLOW_STARTER_SIMPLE_DANGER_VFX_OFFSET_R_1, 
            SIMPLE_DANGER_VFX_SCALE, 
            SIMPLE_DANGER_VFX_HIDE_TIME
        );
        yield return new WaitForSeconds(interval);
        PlaySoundEffect(SIMPLE_DANGER_SFX_PATH, SLOW_STARTER_SIMPLE_DANGER_SFX_BOOST_1);
        SpawnVFX(
            DANGER_VFX_PATH, 
            SIMPLE_DANGER_VFX_NAME, 
            SLOW_STARTER_SIMPLE_DANGER_VFX_OFFSET_L_2,
            SLOW_STARTER_SIMPLE_DANGER_VFX_OFFSET_R_2, 
            SIMPLE_DANGER_VFX_SCALE, 
            SIMPLE_DANGER_VFX_HIDE_TIME
        );
        yield return new WaitForSeconds(interval);
        PlaySoundEffect(SIMPLE_DANGER_SFX_PATH, SLOW_STARTER_SIMPLE_DANGER_SFX_BOOST_2);
        SpawnVFX(
            DANGER_VFX_PATH, 
            SIMPLE_DANGER_VFX_NAME, 
            SLOW_STARTER_SIMPLE_DANGER_VFX_OFFSET_L_3,
            SLOW_STARTER_SIMPLE_DANGER_VFX_OFFSET_R_3, 
            SIMPLE_DANGER_VFX_SCALE, 
            SIMPLE_DANGER_VFX_HIDE_TIME
        );
    }

    IEnumerator ShowAfterDelay (float delay, CustomPoolObject customPoolObj, SpriteRenderer[] spriteRenderers)
    {
        yield return new WaitForSeconds(delay);
        foreach (var spriteRenderer in spriteRenderers)
            spriteRenderer.forceRenderingOff = false;
        customPoolObj.EnterLevelAwake();
    }

    public void Dispose ()
    {
        RemoveListeners();
    }
}