using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // Player's score
    private int score = 0;

    public Text scoreText;

    public GameObject play;
    public GameObject gameOver;

    public Player player;

    private void Awake()
    {
        Application.targetFrameRate = 120;
        PauseGame();
    }
    public void StartGame()
    {
        score = 0;
        scoreText.text = score.ToString();
        Time.timeScale = 1f;
        player.enabled = true;
        play.SetActive(false);
        gameOver.SetActive(false);

        Pipes[] pipes = Object.FindObjectsByType<Pipes>(FindObjectsSortMode.None);
        for (int i = 0; i < pipes.Length; i++)
        {
            Destroy(pipes[i].gameObject);
        }

    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        player.enabled = false;
        
    }
    
    public void GameOver()
    {
        Debug.Log("Game Over! Final Score: " + score);
        gameOver.SetActive(true);
        play.SetActive(true);

        PauseGame();
    }

    public void IncreaseScore()
    {
        score++;
        scoreText.text = score.ToString();
    }
}
