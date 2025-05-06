using BepInEx.Logging;

#nullable enable
namespace NineSolsAPI;

static class KLog
{
    static ManualLogSource logSource;
    static string PREFIX = "KLOG! ";

    internal static void Init (ManualLogSource logSource) => KLog.logSource = logSource;

    internal static void Debug(object data) => logSource.LogDebug(PREFIX + data);

    internal static void Error(object data) => logSource.LogError(PREFIX + data);

    internal static void Fatal(object data) => logSource.LogFatal(PREFIX + data);

    internal static void Info(object data) => logSource.LogInfo(PREFIX + data);

    internal static void Message(object data) => logSource.LogMessage(PREFIX + data);

    internal static void Warning(object data) => logSource.LogWarning(PREFIX + data);
}