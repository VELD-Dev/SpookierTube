namespace SpookierTubeLib.Monobehaviours;

public class SpookierTubeManager : MonoBehaviour
{
    public static VisualTreeAsset MainMenu;
    public static Dictionary<string, CategoryMenu> Tabs = new();
    public UIDocument UiDoc;
    public GameObject FrontFilter { get; internal set; }
    public CursorVisual Cursor { get; internal set; }

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
        UiDoc = gameObject.transform.Find("McScreen").Find("Panel").Find("UI").gameObject.GetComponent<UIDocument>();
        UiDoc.enabled = true;
        UiDoc.visualTreeAsset = MainMenu;
        if(UiDoc.panelSettings is null)
        {
            Main.Logger.LogError("PanelSettings of the UI is not defined");
            return;
        }
        UiDoc.panelSettings.SetScreenToPanelSpaceFunction((screenPos) =>
        {
            var invPos = new Vector2(float.NaN, float.NaN);

            Ray camRay = Camera.main.ScreenPointToRay(new(Camera.main.pixelWidth / 2, Camera.main.pixelHeight / 2, 0f));

            RaycastHit hit;
            if (Physics.Raycast(camRay, out hit, 2.5f, LayerMask.GetMask("UI"))) {
                Main.Logger.LogWarning("Invalid position.");
                return invPos;
            }

            Vector2 pixUv = hit.textureCoord;
            pixUv.y = 1 - pixUv.y;
            pixUv.x *= UiDoc.panelSettings.targetTexture.width;
            pixUv.y *= UiDoc.panelSettings.targetTexture.height;

            return pixUv;
        });

        FrontFilter.transform.parent = gameObject.transform.Find("McScreen");
        FrontFilter.transform.SetLocalPositionAndRotation(new(0, 0, 0), Quaternion.Euler(new(0, 0, 270)));
        gameObject.transform.Find("McScreen").Find("Panel").localScale = new(1.0125f, 1.028f, 1f);

        Main.Logger.LogDebug("UIDocument set up.");
    }
}
