namespace SpookierTubeLib;

public abstract class CategoryMenu : MonoBehaviour
{
    public abstract VisualTreeAsset menuRootAsset { get; protected set; }
    public abstract string menuName { get; protected set; }
    public virtual void OnMenuOpen() { }
}
