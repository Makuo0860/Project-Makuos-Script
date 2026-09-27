using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    [SerializeField] private TMP_Text scoreText;

    private int score = 0;

    void Awake()
    {
        Instance = this;
        UpdateScore();
    }

    public void AddScore(int value)
    {
        score += value;
        UpdateScore();
    }

    void UpdateScore()
    {
        scoreText.text = "SCORE : " + score;
    }

    public int Score
    {
        get { return score; }
    }
}