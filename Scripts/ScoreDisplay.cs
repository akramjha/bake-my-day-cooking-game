using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ScoreDisplay : MonoBehaviour
{
    public Image scoreImage;
    public Image circleImage;
    public Image squareImage;

    private void Start()
    {
        DisplayScore();
    }

    private void DisplayScore()
    {
        if (scoreImage == null)
        {
            Debug.LogError("scoreImage is not assigned!");
            return;
        }

        int finalScore = PlayerPrefs.GetInt("FinalScore", 0);

        if (finalScore == 2000)
        {
            scoreImage.sprite = circleImage.sprite;
        }
        else if (finalScore == 10000)
        {
            scoreImage.sprite = squareImage.sprite;
        }
        else
        {
            Debug.LogWarning("No image found for score: " + finalScore);
        }
    }
}
