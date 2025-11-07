using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;

// Simple EditMode unit test for GameManager.IncreaseScore
public class GameManagerTests
{
    [Test]
    public void IncreaseScore_IncrementsScoreAndUpdatesUI()
    {
        // Arrange
        var gameManagerGo = new GameObject("GameManager");
        var gameManager = gameManagerGo.AddComponent<GameManager>();

        var scoreTextGo = new GameObject("ScoreText");
        var scoreText = scoreTextGo.AddComponent<Text>();
        gameManager.scoreText = scoreText;

        // Provide a dummy Player so Awake()/PauseGame() don't cause NullReferenceException
        var playerGo = new GameObject("Player");
        var player = playerGo.AddComponent<Player>();
        gameManager.player = player;

        // Act
        gameManager.IncreaseScore();

        // Assert
        Assert.AreEqual("1", scoreText.text);

        // Cleanup
        Object.DestroyImmediate(gameManagerGo);
        Object.DestroyImmediate(scoreTextGo);
        Object.DestroyImmediate(playerGo);
    }
}
