using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    // Player movement 
    private Vector3 direction;

    // Gravity affecting the player
    public float gravity = -9.81f;

    // Jump force applied to the player
    public float jumpForce = 5f;

    // SpriteRenderer component
    private SpriteRenderer spriteRenderer;

    // Sprites
    public Sprite[] sprites;

    // Current sprite index
    private int spriteIndex = 0;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    private void Start()
    {
        InvokeRepeating(nameof(AnimateSprite), 0.15f, 0.15f);
    }

    private void OnEnable()
    {
        // Reset position and direction when enabled
        Vector3 position = transform.position;
        position.y = 0f;
        transform.position = position;
        direction = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        // Keyboard (space) or left mouse button using the new Input System
        bool pressedJump = false;

        if (Keyboard.current != null && Keyboard.current.spaceKey.wasPressedThisFrame)
            pressedJump = true;

        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
            pressedJump = true;

        // Touchscreen (mobile) - primary touch press began
        if (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.wasPressedThisFrame)
            pressedJump = true;

        if (pressedJump)
        {
            direction = Vector3.up * jumpForce;
        }

        // Apply constant gravity to the player's direction
        direction.y += gravity * Time.deltaTime;

        // Move the player
        transform.position += direction * Time.deltaTime;
    }

    // Animate the sprite by cycling through the sprites array
    private void AnimateSprite()
    {
        spriteIndex++;
        if (spriteIndex >= sprites.Length)
        {
            spriteIndex = 0;
        }

        spriteRenderer.sprite = sprites[spriteIndex];
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Obstacle")
        {
            var gm = UnityEngine.Object.FindFirstObjectByType<GameManager>();
            if (gm != null)
            {
                gm.GameOver();
            }
        } else if (other.gameObject.tag == "Scoring")
        {
            var gm = UnityEngine.Object.FindFirstObjectByType<GameManager>();
            if (gm != null)
            {
                gm.IncreaseScore();
            }
        }
    }
}
