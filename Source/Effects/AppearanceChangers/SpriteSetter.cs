using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace PromisedEigong.AppearanceChangers;

public class SpriteSetter : MonoBehaviour
{
    public SpriteRenderer ReferenceSpriteRenderer;
    
    SpriteRenderer thisSpriteRenderer;

    void Awake ()
    {
        thisSpriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update ()
    {
        if (thisSpriteRenderer == null)
            return;
        
        if (ReferenceSpriteRenderer == null)
            return;
        
        thisSpriteRenderer.sprite = ReferenceSpriteRenderer.sprite;
    }
}