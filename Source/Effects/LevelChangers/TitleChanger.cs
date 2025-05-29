using I2.Loc;

namespace PromisedEigong.LevelChangers;
#nullable disable

using TMPro;
using ModSystem;
using UnityEngine;
using static PromisedEigongModGlobalSettings.EigongRefs;
using static PromisedEigongModGlobalSettings.EigongTitle;

public class TitleChanger : MonoBehaviour
{
    PromisedEigongMain MainInstance => PromisedEigongMain.Instance;

    EigongWrapper eigongWrapper;

    void Awake ()
    {
        AddListeners();
    }

    void AddListeners ()
    {
        MainInstance.OnEigongWrapperUpdated += HandleEigongWrapperUpdated;
    }

    void RemoveListeners ()
    {
        MainInstance.OnEigongWrapperUpdated -= HandleEigongWrapperUpdated;
    }
    
    void HandleEigongWrapperUpdated ()
    {
        if (eigongWrapper != null)
            eigongWrapper.OnCurrentEigongPhaseChangedPostAnimation -= HandleEigongPhaseUpdated;

        eigongWrapper = MainInstance.EigongWrapper;
        eigongWrapper.OnCurrentEigongPhaseChangedPostAnimation += HandleEigongPhaseUpdated;
    }

    void HandleEigongPhaseUpdated (int phase)
    {
        if (phase == 2)
        {
            var title = GameObject.Find(EIGONG_TITLE_NAME_PATH);
            var titleComp = title.GetComponent<RubyTextMeshProUGUI>();
            
            titleComp.text = GetTianhuoAvatarName(LocalizationManager.CurrentLanguageCode);
        }
    }

    void OnDestroy ()
    {
        RemoveListeners();
    }
}