using UnityEngine;

namespace PromisedEigong.AppearanceChangers;

public class BurningFXApplier
{
    public static void ApplyBurningEffect (
        string objectPath, 
        float effectStrength
    )
    {
        var gameObj = GameObject.Find(objectPath);
        Process(effectStrength, gameObj);
    }
    
    public static void ApplyBurningEffect (
        GameObject gameObj,
        float effectStrength
    )
    {
        Process(effectStrength, gameObj);
    }

    static void Process (float effectStrength, GameObject gameObj)
    {
        var burningFx = gameObj.AddComponent<_2dxFX_BurningFX>();
        burningFx.Colors = effectStrength;
    }
}