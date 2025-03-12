using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Linkoid.Repo.RemoveUpscream;

internal static class UpscreamSlayer
{
    public static int PurgeUpscream(List<EnemySetup> list)
    {
        int count = 0;
        foreach (var enemySetup in list.ToArray())
        {
            if (enemySetup.SpawnsUpscream())
            {
                list.Remove(enemySetup);
                count++;
            }
        }
        return count;
    }

    public static bool SpawnsUpscream(this EnemySetup enemySetup)
    {
        if (enemySetup == null) return false;

        RemoveUpscream.Logger.LogDebug($"enemySetup.name = {enemySetup.name}");
        if (enemySetup.name.Contains("upscream", StringComparison.OrdinalIgnoreCase)) return true;

        var spawnedEnemyNames = enemySetup.GetSpawnedEnemyNames();
        //foreach (var spawnedEnemyName in spawnedEnemyNames)
        //{
        //    RemoveUpscream.Logger.LogDebug($"spawnedEnemyName = {spawnedEnemyName}");
        //}

        if (spawnedEnemyNames.FirstOrDefault(static name => name.Contains("upscream", StringComparison.OrdinalIgnoreCase)) != null)
            return true;
        return false;
    }

    public static IEnumerable<string> GetSpawnedEnemyNames(this EnemySetup enemySetup)
    {
        if (enemySetup == null) return Array.Empty<string>();
        return (
            from spawnObject in enemySetup.spawnObjects
            let parent = spawnObject.GetComponent<EnemyParent>()
            where parent != null
            select parent.enemyName
        );
    }
}
