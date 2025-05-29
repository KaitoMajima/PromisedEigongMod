namespace PromisedEigong.Localization;

using HarmonyLib;
using I2.Loc;
using static PromisedEigongModGlobalSettings.EigongLocs;
using static PromisedEigongModGlobalSettings.EigongTitle;

[HarmonyPatch]
public class LocalizationPatches
{
    [HarmonyPostfix]
    [HarmonyPatch(typeof(LocalizationManager), "GetTranslation")]
    static void ChangeEigongName (string Term, ref string __result)
    {
        if (Term != EIGONG_TITLE_LOC)
            return;

        __result = GetEigongName(LocalizationManager.CurrentLanguageCode, __result);
    }

    [HarmonyPostfix]
    [HarmonyPatch(typeof(LocalizationManager), "GetTranslation")]
    static void ChangeRootProgressText (string Term, ref string __result)
    {
        if (Term == ROOT_PROGRESS_LOC)
            __result = ROOT_PROGRESS_TEXT;
    }
}