using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // Singleton instance
    public static GameManager Instance { get; private set; }

    // Player's score
    private int score = 0;

    // UI Text element to display the score
    public Text scoreText;

    // UI elements for play button and game over screen
    public GameObject play;
    public GameObject gameOver;

    // Reference to the Player script
    public Player player;

    private void Awake()
    {
        // Singleton initialization
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Debug.LogWarning("Multiple GameManager instances detected. Destroying duplicate.");
            Destroy(gameObject);
            return;
        }

        Application.targetFrameRate = 120;
        PauseGame();
    }
    private void OnEnable()
    {
        // Subscribe to player events if assigned
        if (player != null)
        {
            player.OnDeath += GameOver;
            player.OnScore += IncreaseScore;
        }
    }

    private void OnDisable()
    {
        // Unsubscribe to avoid memory leaks
        if (player != null)
        {
            player.OnDeath -= GameOver;
            player.OnScore -= IncreaseScore;
        }
    }
    public void StartGame()
    {
        // Reset score and UI
        score = 0;
        scoreText.text = score.ToString();
        Time.timeScale = 1f;
        player.enabled = true;
        play.SetActive(false);
        gameOver.SetActive(false);

        // Return existing pipes to the pool (if present) or destroy them
        Pipes[] pipes = Object.FindObjectsByType<Pipes>(FindObjectsSortMode.None);
        for (int i = 0; i < pipes.Length; i++)
        {
            var pgo = pipes[i].gameObject;
            if (PipePool.Instance != null)
            {
                PipePool.Instance.Return(pgo);
            }
            else
            {
                Destroy(pgo);
            }
        }

    }

    public void PauseGame()
    {
        // Pause the game
        Time.timeScale = 0f;
        player.enabled = false;
        
    }
    
    public void GameOver()
    {
        // Show game over UI
        gameOver.SetActive(true);
        play.SetActive(true);

        PauseGame();
    }

    public void IncreaseScore()
    {
        // Increment score and update UI
        score++;
        scoreText.text = score.ToString();
    }
}
