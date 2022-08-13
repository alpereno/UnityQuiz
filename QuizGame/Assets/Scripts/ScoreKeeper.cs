using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreKeeper : MonoBehaviour
{
    [SerializeField] TMP_Text scoreText;
    public int correctAnswers { get; private set; } = 0;
    public int questionsSeen { get; private set; } = 0;

    int score;

    private void Start()
    {
        FindObjectOfType<Timer>().OnNextQuestion += IncrementQuestionSeen;
        FindObjectOfType<Quiz>().OnCorrectAnswer += IncrementCorrectAnswers;
    }

    void IncrementCorrectAnswers()
    {
        correctAnswers++;
        score = CalculateScore(); ;
        asd();
    }

    void IncrementQuestionSeen()
    {
        questionsSeen++;
        score = CalculateScore();
        asd();
    }

    public int CalculateScore()
    {
        int score = Mathf.RoundToInt(correctAnswers / (float)questionsSeen * 100);
        return score;
    }

    public void asd()
    {
        scoreText.text = "Score: " + score + "%";
    }
}
