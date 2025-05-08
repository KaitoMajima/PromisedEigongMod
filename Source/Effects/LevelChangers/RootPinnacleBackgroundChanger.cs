namespace PromisedEigong.LevelChangers;

using BlendModes;
using UnityEngine;
using static PromisedEigongModGlobalSettings.EigongBackground;

public class RootPinnacleBackgroundChanger : MonoBehaviour
{
    void Awake ()
    {
        ChangeBackground();
    }

    void ChangeBackground ()
    {
        var animatorRef = GameObject.Find(ANIMATOR_REF);
        animatorRef.GetComponent<Animator>().enabled = false;

        var redFade = GameObject.Find(RED_FX);
        var greenFade = GameObject.Find(GREEN_FX);
        var blackFade = GameObject.Find(BLACK_FX);
        var black2Fade = GameObject.Find(BLACK_2_FX);
        var blueFade = GameObject.Find(BLUE_FX);
        var donutFade = GameObject.Find(DONUT_FX);
        var platformLightFade = GameObject.Find(PLATFORM_LIGHT_FX);
        donutFade.SetActive(true);

        redFade.GetComponent<SpriteRenderer>().color = RED_COLOR;
        redFade.GetComponent<BlendModeEffect>().BlendMode = RED_BLEND_MODE;
        greenFade.GetComponent<SpriteRenderer>().color = GREEN_COLOR;
        blackFade.GetComponent<SpriteRenderer>().color = BLACK_COLOR;
        black2Fade.GetComponent<SpriteRenderer>().color = BLACK_2_COLOR;
        blueFade.GetComponent<SpriteRenderer>().color = BLUE_COLOR;
        blueFade.GetComponent<BlendModeEffect>().BlendMode = BLUE_BLEND_MODE;
        donutFade.GetComponent<SpriteRenderer>().color = DONUT_COLOR;
        platformLightFade.GetComponent<Light>().color = PLATFORM_LIGHT_COLOR;
    }
}