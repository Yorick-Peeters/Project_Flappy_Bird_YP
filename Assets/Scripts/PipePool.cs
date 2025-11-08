using System.Collections.Generic;
using UnityEngine;

// Simple generic pool keyed by prefab GameObject. Keeps inactive instances in queues.
public class PipePool : MonoBehaviour
{
    public static PipePool Instance { get; private set; }

    private readonly Dictionary<GameObject, Queue<GameObject>> pools = new Dictionary<GameObject, Queue<GameObject>>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    public GameObject Get(GameObject prefab)
    {
        // Null check
        if (prefab == null)
            return null;

        // Check if we have a pool for this prefab
        if (!pools.TryGetValue(prefab, out var queue))
        {
            queue = new Queue<GameObject>();
            pools[prefab] = queue;
        }

        // Return an existing object if available
        if (queue.Count > 0)
        {
            var go = queue.Dequeue();
            go.SetActive(true);
            return go;
        }

        // Otherwise, instantiate a new one
        var newGo = Instantiate(prefab);
        var pooled = newGo.GetComponent<PooledObject>();
        if (pooled == null)
        {
            pooled = newGo.AddComponent<PooledObject>();
        }
        pooled.prefab = prefab;
        return newGo;
    }

    public void Return(GameObject go)
    {
        if (go == null)
            return;

        var pooled = go.GetComponent<PooledObject>();
        if (pooled == null || pooled.prefab == null)
        {
            // Not a pooled object (or no prefab reference) - destroy as fallback
            Destroy(go);
            return;
        }

        go.SetActive(false);

        if (!pools.TryGetValue(pooled.prefab, out var queue))
        {
            queue = new Queue<GameObject>();
            pools[pooled.prefab] = queue;
        }

        queue.Enqueue(go);
    }
}
