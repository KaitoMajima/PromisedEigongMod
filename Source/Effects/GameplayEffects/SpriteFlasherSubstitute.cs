using UnityEngine;

namespace PromisedEigong.Effects.GameplayEffects;

public class SpriteFlasherSubstitute : MonoBehaviour
{
    SpriteFlasher spriteFlasher;
    SpriteRenderer substituteRenderer;
    
    public void Setup (SpriteFlasher spriteFlasher, SpriteRenderer substituteRenderer)
    {
        this.spriteFlasher = spriteFlasher;
        this.substituteRenderer = substituteRenderer;
    }

    void Update ()
    {
        if (spriteFlasher == null || substituteRenderer == null)
            return;

        substituteRenderer.color = new Color(1, 1, 1, spriteFlasher.EffectColor.r);
    }
}