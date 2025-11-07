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
        meshrenderer.material.mainTextureOffset += new Vector2(animationSpeed * Time.deltaTime, 0);
    }
}
