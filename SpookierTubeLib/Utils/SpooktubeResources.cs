namespace SpookierTubeLib.Utils;

public class SpooktubeResources
{
    public static SpooktubeResources Singleton { get; private set; }

    internal AssetBundle assetBundle;
    public string AssetBundlePath { get; internal set; } = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "spookiertubelib.assets");
    internal readonly Dictionary<string, UnityEngine.Object> assets = new();

    public SpooktubeResources()
    {
        assetBundle = AssetBundle.LoadFromFile(AssetBundlePath);
        if(assetBundle is null)
        {
            Main.Logger.LogError($"AssetBundle not found at address '{AssetBundlePath}'. Please make sure the assets file is next to the dll.");
            return;
        }
        Singleton = this;

        UnityEngine.Object[] allAssets = assetBundle.LoadAllAssets();
        foreach(UnityEngine.Object asset in allAssets)
        {
            var fullname = $"{asset.GetType().Name}.{asset.name}";

            if (!assets.TryAdd(fullname, asset))
            {
                Main.Logger.LogWarning($"An asset with the same defname {asset.name} ({asset.GetType().Name}/{fullname}) already exists.");
                continue;
            }

            Main.Logger.LogDebug($"Asset {asset.name} ({asset.GetType().Name}/{fullname}) have been added.");
        }
    }

    public static T GetAsset<T>(string name) where T: UnityEngine.Object
    {
        if (Singleton is null)
        {
            Main.Logger.LogError($"SpookierTube Lib resources manager was not initialized correctly. Unable to fetch asset {name}.");
            return null;
        }

        if(!Singleton.assets.TryGetValue($"{typeof(T).Name}.{name}", out var asset))
        {
            Main.Logger.LogError($"Couldn't find asset {name} ({typeof(T).Name}.{name}) in SpookierTubeLib assets.");
            return null;
        }

        return (T)asset;
    }

    public static bool TryGetAsset<T>(string name, out T asset) where T: UnityEngine.Object
    {
        if (Singleton is null)
        {
            Main.Logger.LogError($"SpookierTube Lib resources manager was not initialized correctly. Unable to fetch asset {name}.");
            asset = null;
            return false;
        }

        if(!Singleton.assets.TryGetValue($"{typeof(T).Name}.{name}", out var filteredAsset))
        {
            Main.Logger.LogWarning($"Couldn't find '{typeof(T).Name}.{name}', will search for other assets with similar allAssets.");
            var filter2 = Singleton.assets.Where((kvp) => (kvp.Key == name || kvp.Key == $"{typeof(T).Name}.{name}") && kvp.Value.GetType() == typeof(T));
            if (filter2.Count() < 1)
            {
                Main.Logger.LogWarning($"Couldn't find any asset with defname {name} and type {typeof(T).Name}");
                asset = null;
                return false;
            }
            filteredAsset = filter2.First().Value;
        }

        Main.Logger.LogDebug($"Asset with defname {name} and type {typeof(T).Name} have been found.");
        asset = filteredAsset as T;
        return true;
    }
}
