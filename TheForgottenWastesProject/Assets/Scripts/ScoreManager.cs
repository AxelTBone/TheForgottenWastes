using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public Text highScoreText;
    public Text scoreText;
    public Text suppliesText;
    public Text killsText;
    int highScore = 0;
    int score = 0;
    int supplies = 0;
    int kills;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        highScore = PlayerPrefs.GetInt("highScore", 0);
        highScoreText.text = "HIGHSCORE: " + highScore.ToString();
        scoreText.text = "SCORE: " + score.ToString();
        suppliesText.text = "SUPPLIES: " + supplies.ToString();
        killsText.text = "KILLS: " + kills.ToString();
    }

    public void FixedUpdate()
    {
        score = supplies + kills * 15;
        scoreText.text = "SCORE: " + score.ToString();
        if (highScore < score)
        {
            PlayerPrefs.SetInt("highScore", score);
        }
    }

    public void AddSupplies()
    {
        supplies += 10;
        suppliesText.text = "SUPPLIES: " + supplies.ToString();
    }

    public void AddKill()
    {
        kills++;
        killsText.text = "KILLS: " + kills.ToString();
    }
}