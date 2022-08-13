using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Quiz : MonoBehaviour
{
    public event System.Action OnAnswered;
    public event System.Action OnCorrectAnswer;

    [Header("Questions")]
    [SerializeField] TextMeshProUGUI questionText;
    [SerializeField] List<QuestionSO> questions = new List<QuestionSO>();
    QuestionSO currentQuestion;

    [Header("Answers")]
    [SerializeField] GameObject[] answerButtons;

    [Header("Button Colors")]
    [SerializeField] Sprite defaultAnswerSprite;
    [SerializeField] Sprite correctAnswerSprite;

    [Header("Progress Bar")]
    [SerializeField] Slider progressBar;

    [SerializeField] GameObject quizObject;
    [SerializeField]GameObject EndScreenObject;

    //Timer timer;
    int answersLength;
    int correctAnswerIndex;
    public bool isComplete;

    Timer timer;

    private void Start()
    {
        timer = FindObjectOfType<Timer>();
        progressBar.maxValue = questions.Count;
        progressBar.value = 0;
        if (timer != null)
        {
            timer.OnTimeIsUp += OnTimeIsUp;
            timer.OnNextQuestion += OnNextQuestion;
        }
        NextQuestion();
    }

    //private void Update()
    //{
    //    timerImage.fillAmount = timer.percent;
    //    if (timer.nextQuestion)
    //    {
    //        hasAnswered = false;
    //        GetNextQuestion();
    //        timer.nextQuestion = true;
    //    }
    //    else if (!hasAnswered && !timer.answeringQuestion)
    //    {
    //        DisplayAnswer(-1);
    //        //SetButtonState(false);
    //    }
    //}

    void NextQuestion()
    {
        if (questions.Count > 0)
        {
            SetButtonState(true);
            SetDefaultButtonSprites();
            GetRandomQuestion();
            DisplayQuestion();
            progressBar.value++;
        }
        else
        {
            print("Game over in next question end of question");
            quizObject.SetActive(false);
            EndScreenObject.SetActive(true);
        }

        // else call an event game is over congrt. etc.
        // time stop
    }

    void DisplayQuestion()
    {
        answersLength = answerButtons.Length;
        questionText.text = currentQuestion.GetQuestion();
        for (int i = 0; i < answersLength; i++)
        {
            TextMeshProUGUI buttonText = answerButtons[i].GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = currentQuestion.GetAnswer(i);
        }
    }

    void GetRandomQuestion()
    {
        int randomNumber = Random.Range(0, questions.Count);
        currentQuestion = questions[randomNumber];

        if (questions.Contains(currentQuestion))
        {
            questions.Remove(currentQuestion);
        }
    }

    void SetButtonState(bool state)
    {
        for (int i = 0; i < answersLength; i++)
        {
            Button button = answerButtons[i].GetComponent<Button>();
            button.interactable = state;
        }
    }

    void SetDefaultButtonSprites()
    {
        //for (int i = 0; i < answersLength; i++)
        //{
        //    Image changedButtonImage = answerButtons[i].GetComponent<Image>();
        //    changedButtonImage.sprite = defaultAnswerSprite;
        //}
            Image changedButtonImage = answerButtons[correctAnswerIndex].GetComponent<Image>();
            changedButtonImage.sprite = defaultAnswerSprite;
    }

    // connect this method to all of answers. Need to know wich button pressed so with index variable
    public void OnAnswerSelected(int index)
    {
        DisplayAnswer(index);
        SetButtonState(false);
        if (OnAnswered != null)
        {
            OnAnswered();
        }
        if (progressBar.value==progressBar.maxValue)
        {
            isComplete = true;
            print("game over after answer selected progress bar = max value");
        }
    }

    void DisplayAnswer(int index)
    {
        correctAnswerIndex = currentQuestion.GetCorrectAnswerIndex();
        Image correctAnswerButtonImage = answerButtons[correctAnswerIndex].GetComponent<Image>();

        if (index == correctAnswerIndex)
        {
            questionText.text = "Correct!";
            if (OnCorrectAnswer != null)
            {
                OnCorrectAnswer();
            }
        }
        else
        {
            string correctAnswer = currentQuestion.GetAnswer(correctAnswerIndex);
            questionText.text = "Sorry, the correct answer was; \n" + correctAnswer;
        }
        correctAnswerButtonImage.sprite = correctAnswerSprite;
        SetButtonState(false);
    }

    void OnTimeIsUp()
    {
        print("Time is up for answering!!!");
        DisplayAnswer(-1);
    }

    void OnNextQuestion()
    {
        print("Answer Showing time is over!!!");
        NextQuestion();
    }
}
