namespace PromisedEigong;

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
        if (Term == EIGONG_TITLE_LOC)
            __result = EIGONG_TITLE;
    }

    [HarmonyPostfix]
    [HarmonyPatch(typeof(LocalizationManager), "GetTranslation")]
    static void ChangeRootProgressText (string Term, ref string __result)
    {
        if (Term == ROOT_PROGRESS_LOC)
            __result = ROOT_PROGRESS_TEXT;
    }
}