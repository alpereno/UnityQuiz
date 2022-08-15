using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreKeeper : MonoBehaviour
{
    [SerializeField] TMP_Text scoreText;
    public int correctAnswers { get; private set; } = 0;
    public int questionsSeen { get; private set; } = 1;

    bool gameOver;

    private void Start()
    {
        FindObjectOfType<Timer>().OnNextQuestion += IncrementQuestionSeen;
        FindObjectOfType<Quiz>().OnCorrectAnswer += IncrementCorrectAnswers;
    }

    void IncrementCorrectAnswers()
    {
        correctAnswers++;
        DisplayScore();
    }

    void IncrementQuestionSeen()
    {
        if (!gameOver)
        {
            questionsSeen++;
            DisplayScore();
        }
    }

    public void DisplayScore()
    {
        scoreText.text = "Score: " + correctAnswers + "/" + questionsSeen;
    }
}
