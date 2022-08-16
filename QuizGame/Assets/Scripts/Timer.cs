using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{                   
    public event System.Action OnTimeIsUp;          // when time is up this event will be called also subscriptions method are to.
                                                    // dipslaying correct answer.

    public event System.Action OnNextQuestion;      // on next question event will be called to score and get next question

    // the variable that showing player's answering phase
    public bool answeringQuestion;
    public float percent;
    public bool nextQuestion;

    [SerializeField] Image timerImage;
    [SerializeField] float fullTime = 30;
    [SerializeField] float timeToShowCorrectAnswer = 3.5f;
    
    float timerValue;
    bool gameOver;
    bool gameStarted;

    public void OnStart()
    {
        FindObjectOfType<Quiz>().OnAnswered += OnAnsweredQuestion;
        FindObjectOfType<Quiz>().OnGameOver += OnGameOver;
        gameStarted = true;
        answeringQuestion = true;
        timerValue = fullTime;
    }

    private void Update()
    {
        if (gameStarted && !gameOver)
        {
            Countdown();
        }
    }

    void Countdown()
    {
        timerValue -= Time.deltaTime;

        if (answeringQuestion)
        {
            if (timerValue > 0)
            {
                percent = timerValue / fullTime;
                timerImage.fillAmount = percent;
            }
            else
            {
                answeringQuestion = false;
                timerValue = timeToShowCorrectAnswer;

                if (OnTimeIsUp != null)
                {
                    OnTimeIsUp();
                }
            }
        }
        else
        {
            if (timerValue > 0)
            {
                percent = timerValue / timeToShowCorrectAnswer;
                timerImage.fillAmount = percent;
            }
            else
            {
                answeringQuestion = true;
                timerValue = fullTime;
                if (OnNextQuestion != null)
                {
                    OnNextQuestion();
                }
            }
        }
    }

    void OnAnsweredQuestion()
    {
        answeringQuestion = false;
        timerValue = timeToShowCorrectAnswer;
    }

    void OnGameOver()
    {
        gameOver = true;
        timerValue = 1;
    }
}
