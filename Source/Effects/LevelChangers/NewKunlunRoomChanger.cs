using System.Collections;
using PromisedEigong.ModSystem;

namespace PromisedEigong.LevelChangers;
using Shapes;
using UnityEngine;
using static PromisedEigongModGlobalSettings.NewKunlunBackground;

public class NewKunlunRoomChanger : MonoBehaviour
{
    bool ShouldSkipHub => PromisedEigongMain.shouldSkipHub.Value;
    bool hasSkipped;
    
    void Awake ()
    {
        ChangeProgressBar();
    }

    void Update ()
    {
        if (hasSkipped)
            return;

        StartCoroutine(AutoSkip());
        hasSkipped = true;
    }

    void ChangeProgressBar ()
    {
        var progressBarRef = GameObject.Find(PROGRESS_BAR);
        var rectangle = progressBarRef.GetComponent<Rectangle>();
        rectangle.Width = BAR_WIDTH;
        rectangle.FillColorStart = BAR_COLOR;
        rectangle.FillColorEnd = BAR_COLOR;
        rectangle.UseFill = true;
    }

    IEnumerator AutoSkip ()
    {
        yield return null;
        
        if (!ShouldSkipHub)
            yield break;
        
        Player.i.transform.position = SKIP_POSITION;
    }
}