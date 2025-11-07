using NUnit.Framework;
using UnityEngine;

public class PipeSpawnerTests
{
    // Test that SpawnPipe spawns a pipe within the specified height range
    [Test]
    public void SpawnPipe_PositionWithinRange()
    {
        // Arrange
        var poolGo = new GameObject("PipePool");
        poolGo.AddComponent<PipePool>();

        var spawnerGo = new GameObject("Spawner");
        var spawner = spawnerGo.AddComponent<PipeSpawner>();

        var prefab = new GameObject("PipePrefab");
        prefab.AddComponent<Pipes>();

        spawner.prefab = prefab;
        spawner.minHeight = -1f;
        spawner.maxHeight = 1f;
        spawner.transform.position = Vector3.zero;

        // Act
        var spawned = spawner.SpawnPipe();

        // Assert
        Assert.IsNotNull(spawned);
        Assert.GreaterOrEqual(spawned.transform.position.y, spawner.minHeight - 0.0001f);
        Assert.LessOrEqual(spawned.transform.position.y, spawner.maxHeight + 0.0001f);

        // cleanup
        Object.DestroyImmediate(poolGo);
        Object.DestroyImmediate(spawnerGo);
        Object.DestroyImmediate(prefab);
        Object.DestroyImmediate(spawned);
    }
}
