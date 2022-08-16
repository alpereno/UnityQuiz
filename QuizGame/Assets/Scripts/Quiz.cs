using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Quiz : MonoBehaviour
{
                                                    // I useed event to reduce interclass dependency
    public event System.Action OnAnswered;          // when time is up this event will be called also subscriptions method are to.
    public event System.Action OnCorrectAnswer;     // the correct answer number should increase on scorekeeper
    public event System.Action OnGameOver;          // when game is over bring the gameOver screen etc.

    [Header("Questions")]
    [SerializeField] TextMeshProUGUI questionText;
    [SerializeField] TMP_Text categoryText;
    [HideInInspector] public List<QuestionSO> questions = new List<QuestionSO>();   // list which is holding all given questions
    QuestionSO currentQuestion;                                                     // to show screen chosen question

    [Header("Answers")]
    [SerializeField] GameObject[] answerButtons;

    [Header("Button Colors")]
    [SerializeField] Sprite defaultAnswerSprite;
    [SerializeField] Sprite correctAnswerSprite;

    [Header("Progress Bar")]
    [SerializeField] Slider progressBar;

    int answersLength;
    int correctAnswerIndex;
    Timer timer;

    public void OnStart()
    {
        timer = FindObjectOfType<Timer>();
        progressBar.maxValue = questions.Count;
        progressBar.value = 0;
        if (timer != null)
        {
            timer.OnTimeIsUp += OnTimeIsUp;
            timer.OnNextQuestion += OnNextQuestion;
            timer.OnStart();
        }
        NextQuestion();
    }

    void NextQuestion()
    {
        if (questions.Count > 0)
        {
            answersLength = answerButtons.Length;
            SetButtonState(true);
            SetDefaultButtonSprites();
            GetRandomQuestion();
            DisplayQuestion();
            progressBar.value++;
        }
        else
        {
            if (OnGameOver != null)
            {
                OnGameOver();
            }
        }
    }

    void DisplayQuestion()
    {
        questionText.text = currentQuestion.GetQuestion();
        categoryText.text = currentQuestion.GetQuestionCategory();

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

    // bind this method to all of answers. Need to know wich button pressed so with index variable
    public void OnAnswerSelected(int index)
    {
        DisplayAnswer(index);
        SetButtonState(false);
        if (OnAnswered != null)
        {
            OnAnswered();
        }
    }

    void DisplayAnswer(int index)
    {
        correctAnswerIndex = currentQuestion.GetCorrectAnswerIndex();
        Image correctAnswerButtonImage = answerButtons[correctAnswerIndex].GetComponent<Image>();

        if (index == correctAnswerIndex)
        {
            //questionText.text = "Correct!";
            if (OnCorrectAnswer != null)
            {
                OnCorrectAnswer();
            }
        }
        else
        {
            string correctAnswer = currentQuestion.GetAnswer(correctAnswerIndex);
            //questionText.text = "Sorry, the correct answer was; \n" + correctAnswer;
        }
        correctAnswerButtonImage.sprite = correctAnswerSprite;
        SetButtonState(false);
    }

    void OnTimeIsUp()
    {
        //print("Time is up for answering!!!");
        DisplayAnswer(-1);
    }

    void OnNextQuestion()
    {
        //print("Answer Showing time is over!!!");
        NextQuestion();
    }
}
