namespace PromisedEigong.LevelChangers;
#nullable disable

using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class WhiteScreen : MonoBehaviour
{
    public float ShowDuration => 0.07f;
    public float HideDuration => 0.007f;

    Canvas canvas;
    CanvasScaler canvasScaler;
    CanvasGroup canvasGroup;
    
    void Awake ()
    {
        canvas = gameObject.AddComponent<Canvas>();
        canvasScaler = gameObject.AddComponent<CanvasScaler>();
        canvasGroup = gameObject.AddComponent<CanvasGroup>();
        gameObject.AddComponent<CanvasRenderer>();
        gameObject.AddComponent<Image>();

        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.sortingOrder = 9999;
        canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        canvasScaler.referenceResolution = new Vector2(1920, 1080);
        gameObject.name = "WhiteScreen";
        canvasGroup.alpha = 0;
    }

    public void Show ()
    {
        var tween = DOTween.To(() => canvasGroup.alpha, x => canvasGroup.alpha = x, 1, ShowDuration);
        tween.Play();
    }

    public void Hide ()
    {
        var tween = DOTween.To(() => canvasGroup.alpha, x => canvasGroup.alpha = x, 0, HideDuration);
        tween.Play();
    }
}