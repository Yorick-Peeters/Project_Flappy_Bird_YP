using UnityEngine;

public class Parallax : MonoBehaviour
{
    private MeshRenderer meshrenderer;

    public float animationSpeed = 0.1f;

    private void Awake()
    {
        meshrenderer = GetComponent<MeshRenderer>();
    }

    private void Update()
    {
        // Avoid allocating a new Vector2 each frame
        Vector2 offset = meshrenderer.material.mainTextureOffset;
        offset.x += animationSpeed * Time.deltaTime;
        meshrenderer.material.mainTextureOffset = offset;
    }
}
