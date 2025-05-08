namespace PromisedEigong.Effects;
using NineSolsAPI;
using AppearanceChangers;
using PoolObjectWrapper;
using UnityEngine;
using static PromisedEigongModGlobalSettings.EigongColors;

public class EffectsManager
{
    public void Setup ()
    {
        AddListeners();
    }

    void AddListeners ()
    {
        PoolObjectPatches.OnFoundTaiDanger += HandleTaiDangerFound;
        PoolObjectPatches.OnFoundJieChuanFireParticles += HandleJieChuanFireParticlesFound;
        PoolObjectPatches.OnFoundJieChuanFireImage += HandleJieChuanFireImageFound;
        PoolObjectPatches.OnFoundFooExplosionSprite += HandleFooExplosionSpriteFound;
        PoolObjectPatches.OnFoundFooSprite += HandleFooSpriteFound;
        PoolObjectPatches.OnFoundCrimsonGeyserSprite += HandleCrimsonGeyserSpriteFound;
        PoolObjectPatches.OnFoundCrimsonPillarSprite += HandleCrimsonPillarSpriteFound;
    }

    public void ChangeCharacterEigongColors ()
    {
        BurningFXApplier.ApplyBurningEffect(EIGONG_CHARACTER_BODY, EIGONG_BURNING_FX_STRENGTH);
        BurningFXApplier.ApplyBurningEffect(EIGONG_CHARACTER_BODY_SHADOW, EIGONG_BURNING_FX_STRENGTH);
    }

    public void ChangeFixedEigongColors ()
    {
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
}