using System.Collections;
using NineSolsAPI;
using PromisedEigong.AppearanceChangers;
using PromisedEigong.PoolObjectWrapper;

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

//TODO: Join this class and EffectsManager class that I separated for some reason
public class GameplayEffectManager : IDisposable
{
    bool IsHighContrast => PromisedEigongMain.highContrastEigong.Value;
    
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
        PoolObjectPatches.OnFoundJudgmentCutEigongBody -= HandleFoundJudgmentCutBody;
        PoolObjectPatches.OnFoundJudgmentCutEigongHair -= HandleFoundJudgmentCutHair;
        PoolObjectPatches.OnFoundJudgmentCutEigongTianhuoHair -= HandleFoundJudgmentCutTianhuoHair;
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
            runner.StartCoroutine(SlowStarterMultipleVFX(0.15f));
    }
    
    void HandleEigongTransformed ()
    {
        ChangeBodyColor();
        ChangeHairColor();
        ChangeTianhuoHairColor();
    }
    
    void HandleFoundJudgmentCutBody (SpriteRenderer spriteRenderer)
    {
        GetTransformationComponents(
            spriteRenderer.gameObject,
            out var eigongBodyRenderer,
            out _,
            out var spriteFollower,
            out var spriteSetter,
            out var colorChange,
            out var overlay
        );
        ApplyTransformationColorChange(spriteFollower, eigongBodyRenderer, spriteSetter, colorChange, overlay);
    }
    
    void HandleFoundJudgmentCutHair (SpriteRenderer spriteRenderer)
    {
        GetTransformationComponents(
            spriteRenderer.gameObject,
            out var eigongHairRenderer,
            out var overlaySpriteRenderer,
            out var spriteFollower,
            out var spriteSetter,
            out var colorChange,
            out var overlay
        );
        ApplyTransformationColorChange(spriteFollower, eigongHairRenderer, spriteSetter, colorChange, overlay);
        wrapper.StartCoroutine(UpdateSortingOrderNextFrame(overlaySpriteRenderer,
            EIGONG_OVERLAY_SORTING_ORDER));
    }
    
    void HandleFoundJudgmentCutTianhuoHair (SpriteRenderer spriteRenderer)
    {
        GetTransformationComponents(
            spriteRenderer.gameObject,
            out var eigongHairRenderer,
            out var overlaySpriteRenderer,
            out var spriteFollower,
            out var spriteSetter,
            out var colorChange,
            out var overlay
        );
        ApplyTransformationColorChange(spriteFollower, eigongHairRenderer, spriteSetter, colorChange, overlay);
        wrapper.StartCoroutine(UpdateSortingOrderNextFrame(overlaySpriteRenderer,
            EIGONG_OVERLAY_SORTING_ORDER));
    }

    void ChangeBodyColor ()
    {
        var eigongTargetPiece = GameObject.Find(EIGONG_CHARACTER_BODY);
        GetTransformationComponents(
            eigongTargetPiece,
            out var eigongBodyRenderer, 
            out _, 
            out var spriteFollower,
            out var spriteSetter, 
            out var colorChange, 
            out var overlay
        );
        ApplyTransformationColorChange(spriteFollower, eigongBodyRenderer, spriteSetter, colorChange, overlay);
    }
    
    void ChangeHairColor ()
    {
        var eigongTargetPiece = GameObject.Find(EIGONG_CHARACTER_HAIR_SPRITE);
        GetTransformationComponents(
            eigongTargetPiece,
            out var eigongHairRenderer, 
            out var overlaySpriteRenderer, 
            out var spriteFollower,
            out var spriteSetter, 
            out var colorChange, 
            out var overlay
        );
        ApplyTransformationColorChange(spriteFollower, eigongHairRenderer, spriteSetter, colorChange, overlay);
        wrapper.StartCoroutine(UpdateSortingOrderNextFrame(overlaySpriteRenderer, EIGONG_OVERLAY_SORTING_ORDER));
    }
    
    void ChangeTianhuoHairColor ()
    {
        var eigongTargetPiece = GameObject.Find(EIGONG_CHARACTER_TIANHUO_HAIR_SPRITE);
        GetTransformationComponents(
            eigongTargetPiece,
            out var eigongHairRenderer, 
            out var overlaySpriteRenderer, 
            out var spriteFollower,
            out var spriteSetter, 
            out var colorChange, 
            out var overlay
        );
        ApplyTransformationColorChange(spriteFollower, eigongHairRenderer, spriteSetter, colorChange, overlay);
        wrapper.StartCoroutine(UpdateSortingOrderNextFrame(overlaySpriteRenderer, EIGONG_OVERLAY_SORTING_ORDER));
    }

    void GetTransformationComponents (
        GameObject eigongTargetPiece,
        out SpriteRenderer eigongPieceRenderer,
        out SpriteRenderer overlaySpriteRenderer, 
        out SpriteFollower spriteFollower, 
        out SpriteSetter spriteSetter,
        out _2dxFX_AL_ColorChange colorChange, 
        out _2dxFX_AL_4Gradients overlay
    )
    {
        eigongPieceRenderer = eigongTargetPiece.GetComponent<SpriteRenderer>();
        var eigongTransformOverlayObj = new GameObject();
        eigongTransformOverlayObj.transform.SetParent(eigongTargetPiece.transform);
        overlaySpriteRenderer = eigongTransformOverlayObj.AddComponent<SpriteRenderer>();
        spriteFollower = eigongTransformOverlayObj.AddComponent<SpriteFollower>();
        spriteSetter = eigongTransformOverlayObj.AddComponent<SpriteSetter>();
        colorChange = eigongTargetPiece.AddComponent<_2dxFX_AL_ColorChange>();
        overlay = eigongTransformOverlayObj.AddComponent<_2dxFX_AL_4Gradients>();
    }

    void ApplyTransformationColorChange (
        SpriteFollower spriteFollower, 
        SpriteRenderer eigongHairRenderer,
        SpriteSetter spriteSetter, 
        _2dxFX_AL_ColorChange colorChange, 
        _2dxFX_AL_4Gradients overlay
    )
    {
        spriteFollower.followRenderer = eigongHairRenderer;
        spriteSetter.ReferenceSpriteRenderer = eigongHairRenderer;
        colorChange._Saturation = EIGONG_TRANSFORM_COLORCHANGE_SATURATION;
        colorChange._ValueBrightness = IsHighContrast ? EIGONG_TRANSFORM_COLORCHANGE_VALUE_BRIGHTNESS_HC : EIGONG_TRANSFORM_COLORCHANGE_VALUE_BRIGHTNESS;
        overlay._Color1 = EIGONG_TRANSFORM_OVERLAY_COLOR_UPPER;
        overlay._Color2 = EIGONG_TRANSFORM_OVERLAY_COLOR_LOWER;
        overlay._Color3 = EIGONG_TRANSFORM_OVERLAY_COLOR_LOWER;
        overlay._Color4 = EIGONG_TRANSFORM_OVERLAY_COLOR_LOWER;
        overlay.BlendMode = EIGONG_TRANSFORM_OVERLAY_BLEND_MODE;
        overlay._Alpha = IsHighContrast ? EIGONG_TRANSFORM_OVERLAY_ALPHA_HC : EIGONG_TRANSFORM_OVERLAY_ALPHA;
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
    
    IEnumerator UpdateSortingOrderNextFrame (SpriteRenderer spriteRenderer, int value)
    {
        var spriteRendererObj = spriteRenderer.gameObject;
        while (!spriteRendererObj.activeInHierarchy)
        {
            yield return null;
        }
        yield return null;
        spriteRenderer.sortingOrder = value;
        spriteRenderer.sortingLayerName = "Monster";
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