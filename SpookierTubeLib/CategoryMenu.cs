namespace SpookierTubeLib;

public abstract class CategoryMenu : MonoBehaviour
{
    public static VisualTreeAsset menuRootAsset;
    public event Action<ClickEvent> OnMenuOpen;
    public abstract string menuName { get; protected set; }
}
