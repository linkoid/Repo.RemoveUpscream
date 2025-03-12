using HarmonyLib;
using System.Collections.Generic;
using UnityEngine;

namespace Linkoid.Repo.RemoveUpscream;

[HarmonyPatch(typeof(EnemyDirector))]
internal static class EnemyDirectorPatches
{
    [HarmonyPostfix, HarmonyPatch("Start")]
    private static void Start_Postfix(EnemyDirector __instance)
    {
        RemoveUpscream.Logger.LogInfo($"{nameof(UpscreamSlayer)} is hunting for humanoid quadrupeds...");

        int i = 1;
        foreach (var list in new[] { __instance.enemiesDifficulty1, __instance.enemiesDifficulty2, __instance.enemiesDifficulty3 })
        {
            var count = UpscreamSlayer.PurgeUpscream(__instance.enemiesDifficulty1);
            if (count > 0)
            {
                RemoveUpscream.Logger.LogInfo($"Purged {count} upscreams from enemiesDifficulty{i++}");
            }
        }
    }
}