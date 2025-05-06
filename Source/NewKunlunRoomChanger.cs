namespace PromisedEigong;
using Shapes;
using UnityEngine;
using static PromisedEigongModGlobalSettings.NewKunlunBackground;

public class NewKunlunRoomChanger : MonoBehaviour
{
    void Awake ()
    {
        ChangeProgressBar();
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
}