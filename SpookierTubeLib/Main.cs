using SpookierTubeLib.Patches;

namespace SpookierTubeLib;

[BepInPlugin(PluginInfo.GUID, PluginInfo.Name, PluginInfo.Version)]
public class Main : BaseUnityPlugin
{
    new public static BepInEx.Logging.ManualLogSource Logger { get; private set; }
    private static Harmony harmony;

    void Awake()
    {
        harmony = new Harmony(PluginInfo.GUID);
        Logger = base.Logger;
        Logger.LogInfo($"{PluginInfo.Name} v{PluginInfo.Version} ({PluginInfo.GUID}) is loading.");

        harmony.PatchAll(typeof(UploadVideoStation_Patches));
        Logger.LogDebug("Upload Video Station patches applied.");
    }

    void Start()
    {
        Logger.LogInfo($"Mod loaded. Thanks for using {PluginInfo.Name}!");
        new SpooktubeResources();
    }
}
