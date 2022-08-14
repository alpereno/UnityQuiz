using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Quiz Question", fileName = "New Question")]
public class QuestionSO : ScriptableObject
{
    [TextArea(2, 6)]
    [SerializeField] string question = "Enter new question text here";
    [SerializeField] string[] answers = new string[4];
    [SerializeField] string questionCategory;

    [Range(0, 3)]
    [SerializeField] int correctAnswerIndex;


    public string GetQuestion()
    {
        return question;
    }

    public int GetCorrectAnswerIndex()
    {
        return correctAnswerIndex;
    }

    public string GetAnswer(int index)
    {
        return answers[index];
    }

    public string GetQuestionCategory()
    {
        return questionCategory;
    }

    public void SetQuestionCategory(string category)
    {
        questionCategory = category;
    }

    public void SetQuestion(string _question)
    {
        question = _question;
    }

    public void SetAnswers(string[] _answers)
    {
        int randomIndex = Random.Range(0, _answers.Length);
        answers[randomIndex] = _answers[0];
        for (int i = 1,j = 0; i < answers.Length; i++,j++)
        {
            if (j == randomIndex)
            {
                i--;
                continue;
            }
            answers[j] = _answers[i];
        }
        correctAnswerIndex = randomIndex;        
    }
}
