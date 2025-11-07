using UnityEngine;

public class Pipes : MonoBehaviour
{
    // Speed at which the pipes move to the left
    public float speed = 5;

    private float leftBound;
    private Camera mainCamera;

    private void Start()
    {
        // Cache Camera.main (costly) and calculate the left boundary based on the camera view
        mainCamera = Camera.main;
        if (mainCamera != null)
            leftBound = mainCamera.ScreenToWorldPoint(Vector3.zero).x - 1f;
        else
            leftBound = -10f; // fallback
    }

    // Update is called once per frame
    void Update()
    {
        // Move the pipes to the left
        transform.position += Vector3.left * speed * Time.deltaTime;

        // Destroy the pipes if they go out of bounds
        if (transform.position.x < leftBound)
        {
            // Return to pool when available to avoid allocation churn; fall back to Destroy.
            if (PipePool.Instance != null)
            {
                PipePool.Instance.Return(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
