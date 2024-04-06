using SpookierTubeLib.Monobehaviours;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpookierTubeLib.Patches;

[HarmonyPatch(typeof(UploadVideoStation))]
internal class UploadVideoStation_Patches
{
    [HarmonyPatch("Start")]
    [HarmonyPrefix]
    private static void Start(UploadVideoStation __instance)
    {
        var McScreen = __instance.gameObject.transform.Find("McScreen");
        if(McScreen is null)
        {
            Main.Logger.LogError("Couldn't find the McScreen!!");
            return;
        }

        for(int i = 0; i < McScreen.transform.childCount; i++)
        {
            var child = McScreen.transform.GetChild(i);
            Main.Logger.LogDebug(child.name);
            if (child.name == "Front") continue;
            child.gameObject.SetActive(false);
            UnityEngine.Object.Destroy(child);
        }

        if(!SpooktubeResources.TryGetAsset<GameObject>("SpookierTubeUI", out var go))
        {
            Main.Logger.LogError("Unable to find the SpookierTubeUI prefab. Aborting.");
            return;
        }
        go.AddComponent<SpookierTubeManager>();
        go.transform.parent = McScreen.transform;
        go.transform.SetSiblingIndex(go.transform.GetSiblingIndex() - 1);
    }
}
