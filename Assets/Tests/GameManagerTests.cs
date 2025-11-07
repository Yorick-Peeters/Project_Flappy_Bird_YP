using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;

// Simple EditMode unit test for GameManager.IncreaseScore
public class GameManagerTests
{
    // Test that IncreaseScore correctly increments the score and updates the scoreText
    [Test]
    public void IncreaseScore_UpdatesText()
    {
        // Arrange
        var go = new GameObject("GM");
        var gm = go.AddComponent<GameManager>();

        var textGo = new GameObject("ScoreText");
        var text = textGo.AddComponent<Text>();
        // assign a default font to avoid null warnings in editor tests
        text.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
        gm.scoreText = text;

        // Act
        gm.IncreaseScore();

        // Assert
        Assert.AreEqual("1", gm.scoreText.text);

        // cleanup
        Object.DestroyImmediate(go);
        Object.DestroyImmediate(textGo);
    }
}
