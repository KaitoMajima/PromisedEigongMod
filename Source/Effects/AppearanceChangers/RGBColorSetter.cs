namespace PromisedEigong.AppearanceChangers;

using UnityEngine;

public static class RGBColorSetter
{
    public static void SetRGB (
        string objectPath, 
        Color color
    )
    {
        var gameObj = GameObject.Find(objectPath);
        Process(color, gameObj);
    }
    
    public static void SetRGB (
        GameObject gameObj, 
        Color color
    )
    {
        Process(color, gameObj);
    }

    static void Process (Color color, GameObject gameObj)
    {
        var rgbFx = gameObj.AddComponent<_2dxFX_ColorRGB>();
        rgbFx._ColorR = color.r;
        rgbFx._ColorG = color.g;
        rgbFx._ColorB = color.b;
    }
}