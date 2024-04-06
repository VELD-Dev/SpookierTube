namespace SpookierTubeLib.Utils;

public static class MainMenuHelper
{
    private static ManualLogSource Logger => Main.Logger;
    public static readonly Dictionary<string, CategoryMenu> Categories = new Dictionary<string, CategoryMenu>();


    public static void AddMenuTab(string categoryNameId, GameObject categoryType)
    {
        if (!categoryType.TryGetComponent<CategoryMenu>(out CategoryMenu menu))
        {
            Main.Logger.LogError($"Couldn't add category {categoryNameId} ({categoryType.GetType().FullName}) from mod {categoryType.GetType().Assembly.GetName()}.");
            return;
        }

        bool hasAdded = Categories.TryAdd(categoryNameId, menu);
        if (!hasAdded)
        {
            Main.Logger.LogError($"Couldn't add the category {categoryNameId}. Maybe the name is already used by another mod.");
            return;
        }


    }
}
