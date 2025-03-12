using HarmonyLib;
using System.Collections.Generic;
using UnityEngine;

namespace Linkoid.Repo.RemoveUpscream;

[HarmonyPatch(typeof(LevelGenerator))]
internal static class LevelGeneratorPatches
{
    [HarmonyPrefix, HarmonyPatch(nameof(LevelGenerator.EnemySpawn))]
    private static bool EnemySpawn_Prefix(ref EnemySetup enemySetup)
    {
        if (UpscreamSlayer.SpawnsUpscream(enemySetup))
        {
            RemoveUpscream.Logger.LogInfo($"Slayed 1 upscream that attempted to spawn from {enemySetup.name}");
            return false;
        }
        return true;
    }
}