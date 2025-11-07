using UnityEngine;

public class PipeSpawner : MonoBehaviour
{
    // Pipe prefab to spawn
    public GameObject prefab;

    // Spawn rate in seconds
    public float spawnRate = 2f;

    // Height range for spawning pipes
    public float minHeight = -1f;
    public float maxHeight = 1f;

    private void OnEnable()
    {
        InvokeRepeating(nameof(SpawnPipe), spawnRate, spawnRate);
    }

    private void OnDisable()
    {
        CancelInvoke(nameof(SpawnPipe));
    }

    private void SpawnPipe()
    {
        GameObject pipes = Instantiate(prefab, transform.position, Quaternion.identity);
        pipes.transform.position += Vector3.up * Random.Range(minHeight, maxHeight);
    }
}
