using NineSolsAPI;

namespace PromisedEigong.Effects;
using AppearanceChangers;
using PoolObjectWrapper;
using UnityEngine;
using static PromisedEigongModGlobalSettings.EigongColors;
using static PromisedEigongModGlobalSettings.EigongVFX;

public class EffectsManager
{
    public void Setup ()
    {
        AddListeners();
    }

    void AddListeners ()
    {
        PoolObjectPatches.OnFoundTaiDanger += HandleTaiDangerFound;
        PoolObjectPatches.OnFoundSimpleTaiDanger += HandleSimpleTaiDangerFound;
        PoolObjectPatches.OnFoundJieChuanFireParticles += HandleJieChuanFireParticlesFound;
        PoolObjectPatches.OnFoundJieChuanFireImage += HandleJieChuanFireImageFound;
        PoolObjectPatches.OnFoundFooExplosionSprite += HandleFooExplosionSpriteFound;
        PoolObjectPatches.OnFoundFooSprite += HandleFooSpriteFound;
        PoolObjectPatches.OnFoundCrimsonGeyserSprite += HandleCrimsonGeyserSpriteFound;
        PoolObjectPatches.OnFoundCrimsonPillarSprite += HandleCrimsonPillarSpriteFound;
        PoolObjectPatches.OnFoundJudgmentCutLine += HandleJudgmentCutLineFound;
        PoolObjectPatches.OnFoundJudgmentCutShape1 += HandleJudgmentCutShape1Found;
        PoolObjectPatches.OnFoundJudgmentCutShape2 += HandleJudgmentCutShape2Found;
        PoolObjectPatches.OnFoundJudgmentCutEigongBody += HandleFoundJudgmentCutBody;
        PoolObjectPatches.OnFoundJudgmentCutEigongHair += HandleFoundJudgmentCutHair;
        PoolObjectPatches.OnFoundJudgmentCutEigongTianhuoHair += HandleFoundJudgmentCutTianhuoHair;
    }
    void RemoveListeners ()
    {
        PoolObjectPatches.OnFoundTaiDanger -= HandleTaiDangerFound;
        PoolObjectPatches.OnFoundSimpleTaiDanger -= HandleSimpleTaiDangerFound;
        PoolObjectPatches.OnFoundJieChuanFireParticles -= HandleJieChuanFireParticlesFound;
        PoolObjectPatches.OnFoundJieChuanFireImage -= HandleJieChuanFireImageFound;
        PoolObjectPatches.OnFoundFooExplosionSprite -= HandleFooExplosionSpriteFound;
        PoolObjectPatches.OnFoundFooSprite -= HandleFooSpriteFound;
        PoolObjectPatches.OnFoundCrimsonGeyserSprite -= HandleCrimsonGeyserSpriteFound;
        PoolObjectPatches.OnFoundCrimsonPillarSprite -= HandleCrimsonPillarSpriteFound;
        PoolObjectPatches.OnFoundJudgmentCutLine -= HandleJudgmentCutLineFound;
        PoolObjectPatches.OnFoundJudgmentCutShape1 -= HandleJudgmentCutShape1Found;
        PoolObjectPatches.OnFoundJudgmentCutShape2 -= HandleJudgmentCutShape2Found;
        PoolObjectPatches.OnFoundJudgmentCutEigongBody -= HandleFoundJudgmentCutBody;
        PoolObjectPatches.OnFoundJudgmentCutEigongHair -= HandleFoundJudgmentCutHair;
        PoolObjectPatches.OnFoundJudgmentCutEigongTianhuoHair -= HandleFoundJudgmentCutTianhuoHair;
    }

    public void ChangeCharacterEigongColors ()
    {
        BurningFXApplier.ApplyBurningEffect(EIGONG_CHARACTER_BODY, EIGONG_BURNING_FX_STRENGTH);
        BurningFXApplier.ApplyBurningEffect(EIGONG_CHARACTER_BODY_SHADOW, EIGONG_BURNING_FX_STRENGTH);
    }

    public void ChangeFixedEigongColors ()
    {
        BurningFXApplier.ApplyBurningEffect(EIGONG_CUTSCENE_BODY, EIGONG_BURNING_FX_STRENGTH);
        ColorChanger.ChangeColors(
            EIGONG_CUTSCENE_SWORD, 
            EIGONG_CHARACTER_SWORD_EFFECT_SHIFT, 
            EIGONG_SWORD_SATURATION,
            EIGONG_CHARACTER_SWORD_VALUE_BRIGHTNESS
        );
        ColorChanger.ChangeColors(
            EIGONG_CHARACTER_SWORD, 
            EIGONG_CHARACTER_SWORD_EFFECT_SHIFT, 
            EIGONG_SWORD_SATURATION,
            EIGONG_CHARACTER_SWORD_VALUE_BRIGHTNESS
        );
        ColorChanger.ChangeColors(
            EIGONG_CHARACTER_FOO, 
            EIGONG_CHARACTER_SWORD_EFFECT_SHIFT, 
            null,
            EIGONG_CHARACTER_SWORD_VALUE_BRIGHTNESS
        );
        ColorChanger.ChangeColors(
            EIGONG_CHARACTER_SWORD_EFFECT, 
            EIGONG_CHARACTER_SWORD_EFFECT_SHIFT, 
            null,
            EIGONG_CHARACTER_SWORD_VALUE_BRIGHTNESS
        );
        ColorChanger.ChangeColors(
            EIGONG_CHARACTER_CRIMSON_SLAM_EFFECT, 
            EIGONG_CHARACTER_SWORD_EFFECT_SHIFT, 
            null,
            EIGONG_CHARACTER_SWORD_VALUE_BRIGHTNESS
        );
        ColorChanger.ChangeColors(
            EIGONG_CHARACTER_CRIMSON_GEYSER_EFFECT, 
            EIGONG_CHARACTER_SWORD_EFFECT_SHIFT, 
            EIGONG_SWORD_SATURATION,
            EIGONG_CHARACTER_SWORD_VALUE_BRIGHTNESS
        );
        ColorChanger.ChangeColors(
            FIRE_TRAIL_PATH, 
            EIGONG_CHARACTER_SWORD_EFFECT_SHIFT, 
            EIGONG_SWORD_SATURATION,
            EIGONG_CHARACTER_SWORD_VALUE_BRIGHTNESS
        );
        ColorChanger.ChangeColors(
            EIGONG_CHARGE_WAVE_CRIMSON, 
            EIGONG_CHARACTER_SWORD_EFFECT_SHIFT, 
            null,
            EIGONG_CHARACTER_SWORD_VALUE_BRIGHTNESS
        );
        ColorChanger.ChangeColors(
            EIGONG_CHARGE_WAVE_CRIMSON_GLOW, 
            EIGONG_CHARACTER_SWORD_EFFECT_SHIFT, 
            null,
            EIGONG_CHARACTER_SWORD_VALUE_BRIGHTNESS
        );
        ColorChanger.ChangeColors(
            EIGONG_CHARGE_WAVE_CRIMSON_2, 
            EIGONG_CHARACTER_SWORD_EFFECT_SHIFT, 
            null,
            EIGONG_CHARACTER_SWORD_VALUE_BRIGHTNESS
        );
        ColorChanger.ChangeColors(
            EIGONG_CHARGE_WAVE_CRIMSON_2_GLOW, 
            EIGONG_CHARACTER_SWORD_EFFECT_SHIFT, 
            null,
            EIGONG_CHARACTER_SWORD_VALUE_BRIGHTNESS
        );
        ParticleColorSetter.ChangeColors(EIGONG_CHARACTER_FOO_EFFECT_1, FIRE_COLOR);
        ParticleColorSetter.ChangeColors(EIGONG_CHARACTER_FOO_EFFECT_2, FIRE_COLOR);
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
        overlaySpriteRenderer.sortingOrder = EIGONG_OVERLAY_SORTING_ORDER;
        overlaySpriteRenderer.sortingLayerName = "Monster";
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
        overlaySpriteRenderer.sortingOrder = EIGONG_OVERLAY_SORTING_ORDER;
        overlaySpriteRenderer.sortingLayerName = "Monster";
        var sprites = spriteRenderer.GetComponentsInChildren<SpriteRenderer>();
        
        foreach (var sprite in sprites)
        {
            if (sprite.name == EIGONG_TIANHUO_HAIR_GLOW)
                sprite.enabled = false;
        }
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
        colorChange._ValueBrightness = EIGONG_TRANSFORM_SHADOW_COLORCHANGE_VALUE_BRIGHTNESS;
        overlay._Color1 = EIGONG_TRANSFORM_SHADOW_OVERLAY_COLOR_UPPER;
        overlay._Color2 = EIGONG_TRANSFORM_SHADOW_OVERLAY_COLOR_LOWER;
        overlay._Color3 = EIGONG_TRANSFORM_SHADOW_OVERLAY_COLOR_LOWER;
        overlay._Color4 = EIGONG_TRANSFORM_SHADOW_OVERLAY_COLOR_LOWER;
        overlay.BlendMode = EIGONG_TRANSFORM_OVERLAY_BLEND_MODE;
        overlay._Alpha = EIGONG_TRANSFORM_OVERLAY_ALPHA;
    }
    
    void HandleTaiDangerFound (SpriteRenderer sprite)
    {
        ColorChanger.ChangeColors(
            sprite.gameObject, 
            EIGONG_CHARACTER_SWORD_EFFECT_SHIFT,
            null,
            EIGONG_TAI_DANGER_VALUE_BRIGHTNESS,
            isLit: false
        );
    }
    
    void HandleSimpleTaiDangerFound (SpriteRenderer sprite)
    {
        ColorChanger.ChangeColors(
            sprite.gameObject, 
            EIGONG_CHARACTER_SWORD_EFFECT_SHIFT,
            SIMPLE_DANGER_VFX_SATURATION,
            SIMPLE_DANGER_VFX_VALUE_BRIGHTNESS,
            isLit: false
        );
    }
    
    void HandleJieChuanFireParticlesFound (ParticleSystem particleSystem)
    {
        ParticleColorSetter.ChangeColors(particleSystem.gameObject, FIRE_COLOR);
    }
    
    void HandleJieChuanFireImageFound (SpriteRenderer sprite)
    {
        RGBColorSetter.SetRGB(sprite.gameObject, FIRE_LIGHT_COLOR);
    }

    void HandleFooExplosionSpriteFound (SpriteRenderer sprite)
    {
        ColorChanger.ChangeColors(
            sprite.gameObject, 
            EIGONG_CHARACTER_SWORD_EFFECT_SHIFT, 
            null,
            EIGONG_TAI_DANGER_VALUE_BRIGHTNESS,
            isLit: false
        );
    }
    
    void HandleFooSpriteFound (SpriteRenderer sprite)
    {
        ColorChanger.ChangeColors(
            sprite.gameObject, 
            EIGONG_CHARACTER_SWORD_EFFECT_SHIFT, 
            null,
            EIGONG_TAI_DANGER_VALUE_BRIGHTNESS,
            isLit: false
        );
    }
    
    void HandleCrimsonGeyserSpriteFound (SpriteRenderer sprite)
    {
        ColorChanger.ChangeColors(
            sprite.gameObject, 
            EIGONG_CHARACTER_SWORD_EFFECT_SHIFT, 
            null,
            EIGONG_TAI_DANGER_VALUE_BRIGHTNESS,
            isLit: false
        );
    }
    
    void HandleCrimsonPillarSpriteFound (SpriteRenderer sprite)
    {
        ColorChanger.ChangeColors(
            sprite.gameObject, 
            EIGONG_CHARACTER_SWORD_EFFECT_SHIFT, 
            null,
            EIGONG_TAI_DANGER_VALUE_BRIGHTNESS,
            isLit: false
        );
    }
    
    void HandleJudgmentCutLineFound (SpriteRenderer sprite)
    {
        ColorChanger.ChangeColors(
            sprite.gameObject, 
            EIGONG_CHARACTER_SWORD_EFFECT_SHIFT, 
            null,
            EIGONG_CRIMSON_CUT_VALUE_BRIGHTNESS,
            isLit: false
        );
    }
    
    void HandleJudgmentCutShape1Found (SpriteRenderer sprite)
    {
        ColorChanger.ChangeColors(
            sprite.gameObject, 
            EIGONG_CHARACTER_SWORD_EFFECT_SHIFT, 
            null,
            EIGONG_CRIMSON_CUT_VALUE_BRIGHTNESS,
            isLit: false
        );
    }

    void HandleJudgmentCutShape2Found (SpriteRenderer sprite)
    {
        ColorChanger.ChangeColors(
            sprite.gameObject, 
            EIGONG_CHARACTER_SWORD_EFFECT_SHIFT, 
            null,
            EIGONG_CRIMSON_CUT_VALUE_BRIGHTNESS,
            isLit: false
        );
    }

    public void Dispose ()
    {
        RemoveListeners();
    }
}