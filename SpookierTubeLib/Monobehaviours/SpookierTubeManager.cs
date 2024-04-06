namespace SpookierTubeLib.Monobehaviours;

public class SpookierTubeManager : MonoBehaviour
{
    public static VisualTreeAsset MainMenu;
    public static Dictionary<string, CategoryMenu> Tabs = new();
    public UIDocument UiDoc;

    private void Awake()
    {
        if(MainMenu is null)
        {
            Main.Logger.LogDebug("HomeMenu was null.");
            if (!SpooktubeResources.TryGetAsset<VisualTreeAsset>("HomeMenu", out MainMenu))
            {
                Main.Logger.LogError("Couldn't load HomeMenu UI Document.");
                return;
            }
        }
        Main.Logger.LogDebug("HomeMenu have been defiend.");
    }

    private void Start()
    {
        UiDoc = gameObject.AddComponent<UIDocument>();
        UiDoc.enabled = true;
        UiDoc.visualTreeAsset = MainMenu;

        Main.Logger.LogDebug("UIDocument set up.");
    }
}
