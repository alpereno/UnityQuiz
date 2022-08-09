using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Quiz : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI questionText;    
    [SerializeField] QuestionSO question;
    [SerializeField] GameObject[] answerButtons;
    int correctAnswerIndex;
    [SerializeField] Sprite defaultAnswerSprite;
    [SerializeField] Sprite correctAnswerSprite;

    int answersLength;


    private void Start()
    {
        //GetNextQuestion();
        DisplayQuestion();
    }

    void DisplayQuestion()
    {
        answersLength = answerButtons.Length;
        questionText.text = question.GetQuestion();
        for (int i = 0; i < answersLength; i++)
        {
            TextMeshProUGUI buttonText = answerButtons[i].GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = question.GetAnswer(i);
        }
    }

    void GetNextQuestion()
    {
        SetButtonState(true);
        DisplayQuestion();
    }

    // connect this method to all of answers. Need to know wich button pressed so with index variable
    public void OnAnswerSelected(int index)
    {
        correctAnswerIndex = question.GetCorrectAnswerIndex();
        Image correctAnswerButtonImage = answerButtons[correctAnswerIndex].GetComponent<Image>();

        if (index == correctAnswerIndex)
        {
            questionText.text = "Correct!";
        }
        else
        {
            string correctAnswer = question.GetAnswer(correctAnswerIndex);
            questionText.text = "Sorry, the correct answer was; \n" + correctAnswer;
        }
        correctAnswerButtonImage.sprite = correctAnswerSprite;
        SetButtonState(false);
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
        Image changedButtonImage = answerButtons[correctAnswerIndex].GetComponent<Image>();
        changedButtonImage.sprite = defaultAnswerSprite;        
    }
}
