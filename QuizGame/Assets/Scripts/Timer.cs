using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public event System.Action OnTimeIsUp;
    public event System.Action OnNextQuestion;

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

    //public void CancelTimer()
    //{
    //    timerValue = 0;
    //}

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
                // this line should run 1 time too
                // call an event for time is up for answering question to show answering 
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
                //nextQuestion = true;
                // this line should run 1 time
                // call an event time is up for showing answer and call the next question
                if (OnNextQuestion != null)
                {
                    OnNextQuestion();
                }
            }
        }
        //print(answeringQuestion + " : " + timerValue + " = " + percent);
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
