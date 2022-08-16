using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreKeeper : MonoBehaviour
{
    // When the answer buttons are selected, the score is calculated with Events.
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
