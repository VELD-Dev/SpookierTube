using BepInEx.Logging;
using SpookierTubeLib.Utils;

namespace SpookierTubeCore;

public class Main : BaseUnityPlugin
{

    new public static ManualLogSource Logger { get; private set; }

    void Awake()
    {
        Logger = base.Logger;
    }

    void Start()
    {
        MainMenuHelper.AddMenuTab("TestCategory", new GameObject());
    }
}
