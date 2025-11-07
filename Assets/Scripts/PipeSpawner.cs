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

    // Made public so it can be called from tests and so pooled-spawn can be reused elsewhere.
    public GameObject SpawnPipe()
    {
        GameObject pipes = null;

        if (PipePool.Instance != null)
        {
            pipes = PipePool.Instance.Get(prefab);
        }
        else
        {
            pipes = Instantiate(prefab, transform.position, Quaternion.identity);
        }

        if (pipes != null)
        {
            pipes.transform.position = transform.position + Vector3.up * Random.Range(minHeight, maxHeight);
            pipes.SetActive(true);
        }

        return pipes;
    }
}
