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
    bool IsLevel0 => !Player.i.mainAbilities.RollDodgeInAirUpgrade.IsAcquired;
    
    bool hasGotTitleReferences;
    bool hasSpawnedLevel0Title;
    EigongWrapper eigongWrapper;
    GameObject titleObj;
    RubyTextMeshProUGUI titleText;

    void Awake ()
    {
        AddListeners();
    }

    void Update ()
    {
        if (!hasGotTitleReferences)
        {
            titleObj = GameObject.Find(EIGONG_TITLE_NAME_PATH);
            titleText = titleObj.GetComponent<RubyTextMeshProUGUI>();
            hasGotTitleReferences = true;
        }

        if (!hasSpawnedLevel0Title && IsLevel0)
        {
            SpawnLvl0ChallengeText(titleObj);
            hasSpawnedLevel0Title = true;
        }
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
            titleText.text = GetTianhuoAvatarName(LocalizationManager.CurrentLanguageCode);
    }

    void SpawnLvl0ChallengeText (GameObject title)
    {
        var newTitle = Instantiate(title, title.transform.parent);
        var newTitleComp = newTitle.GetComponent<RubyTextMeshProUGUI>();
        newTitleComp.transform.localPosition = LVL_0_CHALLENGE_LOCAL_POS;
        newTitleComp.text = LVL_O_CHALLENGE_TEXT;
        newTitleComp.fontSize = LVL_0_CHALLENGE_FONT_SIZE;
        newTitleComp.color = LVL_O_CHALLENGE_TEXT_COLOR;
    }

    void OnDestroy ()
    {
        RemoveListeners();
    }
}