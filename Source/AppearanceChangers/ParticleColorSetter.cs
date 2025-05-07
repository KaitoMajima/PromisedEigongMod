namespace PromisedEigong.AppearanceChangers;

using UnityEngine;

public static class ParticleColorSetter
{
    public static void ChangeColors (
        string objectPath, 
        Color color
    )
    {
        var gameObj = GameObject.Find(objectPath);
        Process(color, gameObj);
    }
    
    public static void ChangeColors (
        GameObject gameObj, 
        Color color
    )
    {
        Process(color, gameObj);
    }

    static void Process (Color color, GameObject gameObj)
    {
        var particleSystem = gameObj.GetComponent<ParticleSystem>();
        var main = particleSystem.main;
        var colorOverLifetime = particleSystem.colorOverLifetime;
        var colorBySpeed = particleSystem.colorBySpeed;

        colorOverLifetime.color = color;
        colorBySpeed.color = color;
        main.startColor = color;
    }
}