using UnityEngine;

public class Pipes : MonoBehaviour
{
    // Speed at which the pipes move to the left
    public float speed = 5;

    private float leftBound;

    private void Start()
    {
        // Calculate the left boundary based on the camera view
        leftBound = Camera.main.ScreenToWorldPoint(Vector3.zero).x - 1f;
    }

    // Update is called once per frame
    void Update()
    {
        // Move the pipes to the left
        transform.position += Vector3.left * speed * Time.deltaTime;

        // Destroy the pipes if they go out of bounds
        if (transform.position.x < leftBound)
        {
            Destroy(gameObject);
        }
    }
}
