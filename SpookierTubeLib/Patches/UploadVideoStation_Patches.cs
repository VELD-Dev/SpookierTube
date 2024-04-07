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

        if(!SpooktubeResources.TryGetAsset<GameObject>("SpookierTubeUI", out var spooktubePrefab))
        {
            Main.Logger.LogError("Unable to find the SpookierTubeUI prefab. Aborting.");
            return;
        }

        var stm = spooktubePrefab.AddComponent<SpookierTubeManager>();
        stm.FrontFilter = McScreen.transform.Find("Front").gameObject;
        McScreen.gameObject.SetActive(false);
        spooktubePrefab.transform.parent = __instance.transform;
        spooktubePrefab.transform.SetSiblingIndex(spooktubePrefab.transform.GetSiblingIndex() - 1);
    }
}
