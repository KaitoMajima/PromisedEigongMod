namespace PromisedEigong.AppearanceChangers;

using UnityEngine;

public static class ColorChanger
{
    public static void ChangeColors (
        string objectPath, 
        float hueShift, 
        float? saturation, 
        float? brightness,
        bool isLit = true
    )
    {
        var gameObj = GameObject.Find(objectPath);
        Process(hueShift, saturation, brightness, isLit, gameObj);
    }
    
    public static void ChangeColors (
        GameObject gameObj,
        float hueShift, 
        float? saturation, 
        float? brightness,
        bool isLit = true
    )
    {
        Process(hueShift, saturation, brightness, isLit, gameObj);
    }

    static void Process (float hueShift, float? saturation, float? brightness, bool isLit, GameObject gameObj)
    {
        if (isLit)
            ApplyEffectsToLitShader(gameObj, hueShift, saturation, brightness);
        else
            ApplyEffectsToUnLitShader(gameObj, hueShift, saturation, brightness);
    }

    static void ApplyEffectsToLitShader (
        GameObject gameObj,
        float hueShift, 
        float? saturation, 
        float? brightness
    )
    {
        var colorChangeFx = gameObj.AddComponent<_2dxFX_ColorChange>();
        colorChangeFx._HueShift = hueShift;
        if (saturation != null)
            colorChangeFx._Saturation = saturation.Value;
        if (brightness != null)
            colorChangeFx._ValueBrightness = brightness.Value;
    }
    
    static void ApplyEffectsToUnLitShader (
        GameObject gameObj,
        float hueShift, 
        float? saturation, 
        float? brightness
    )
    {
        var colorChangeFx = gameObj.AddComponent<_2dxFX_AL_ColorChange>();
        colorChangeFx._HueShift = hueShift;
        if (saturation != null)
            colorChangeFx._Saturation = saturation.Value;
        if (brightness != null)
            colorChangeFx._ValueBrightness = brightness.Value;
    }
}