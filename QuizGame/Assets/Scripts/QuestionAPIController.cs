using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using SimpleJSON;

public class QuestionAPIController : MonoBehaviour
{
    public readonly string baseQuestionURL = "https://opentdb.com/api.php?amount=10";
    public List<QuestionSO> questions = new List<QuestionSO>();
    public Quiz quiz;

    // category 27 = animals
    // category 23 = history
    int categoryIndex = 23;

    //private void Awake()
    //{
    //    StartCoroutine(GetDataAndArrange());
    //}

    IEnumerator GetDataAndArrange()
    {
        // https://opentdb.com/api.php?amount=10&category=27&type=multiple
        string multipleTypeString = "&type=multiple";
        string categoryString = "&category=";

        if (categoryIndex != 0)
        {
            categoryString = "&category=" + categoryIndex.ToString();
        }         

        string questionUrl = baseQuestionURL + categoryString + multipleTypeString;

        UnityWebRequest questionInfoRequest = UnityWebRequest.Get(questionUrl);

        yield return questionInfoRequest.SendWebRequest();

        print(questionInfoRequest.downloadHandler.text);
        // sitenin hepsini yaziyor.

        JSONNode questionInfo = JSON.Parse(questionInfoRequest.downloadHandler.text);
        // first one is questions info(its mean without response code [0] is response code),
        // second dimension is question number in this example 3rd question [0,1,2]
        // third dimension is questions info (its mean category, type, question text, correct and incorrect answers)

        //print(questionInfo[1][2]); //giving 3.questions
        //print(questionInfo[1][2][0]); // giving 3.quesion's category
        //print(questionInfo[1][2][1]); // giving 3.questions's type (multiple) 2 is = difficulty 
        print(questionInfo[1][2][3]); // giving questions
        print(questionInfo[1][2][4]);
        print(questionInfo[1][2][5]);
                                      //print(questionInfo[1][2][4]); // correct answers
                                      //print(questionInfo[1][2][5]); // incorrect answers
                                      //print(questionInfo[1][2][5][0]); // incorrect answer one
                                      // after that it will be null

        print("----");
        CreateScOArray(questionInfo);

        yield return null;

        //string questionText = questionInfo["question"];
        // set ui object..
    }


    void CreateScOArray(JSONNode questionInfo)
    {
        string questionCategory = "";
        string questionText = "";
        string[] answers = new string[4];
        for (int i = 0; i < 10; i++)
        {
            var tempSO = ScriptableObject.CreateInstance<QuestionSO>();

            questionCategory = questionInfo[1][i][0];
            questionText = questionInfo[1][i][3];
            answers[0] = questionInfo[1][i][4];
            answers[1] = questionInfo[1][i][5][0];
            answers[2] = questionInfo[1][i][5][1];
            answers[3] = questionInfo[1][i][5][2];


            tempSO.SetQuestionCategory(questionCategory);
            tempSO.SetQuestion(questionText);
            tempSO.SetAnswers(answers);

            questions.Add(tempSO);
        }
        quiz.questions = questions;

        quiz.OnStart();
    }

    public void SetQuizCategory(int _categoryIndex)
    {
        // check restrictions (valid category index) then set the categoryIndex        
        categoryIndex = _categoryIndex;
        StartCoroutine(GetDataAndArrange());        
    }
}
