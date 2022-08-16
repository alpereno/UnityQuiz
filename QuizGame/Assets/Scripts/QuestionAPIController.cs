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

    // given a value to try. The index will change according to the selected category, the value assigned here does not matter.
    int categoryIndex = 23;

    IEnumerator GetDataAndArrange()
    {
        string multipleTypeString = "&type=multiple";
        string categoryString = "&category=";

        if (categoryIndex != 0)
        {
            categoryString = "&category=" + categoryIndex.ToString();
        }         

        string questionUrl = baseQuestionURL + categoryString + multipleTypeString;

        UnityWebRequest questionInfoRequest = UnityWebRequest.Get(questionUrl);

        yield return questionInfoRequest.SendWebRequest();

        if (questionInfoRequest.isNetworkError || questionInfoRequest.isHttpError)
        {
            print(questionInfoRequest.error);
            yield break;
        }

        // printing all json file
        //*print(questionInfoRequest.downloadHandler.text);

        // Converting to utf8 for clarity of question texts.

        //JSONNode questionInfo = JSON.Parse(questionInfoRequest.downloadHandler.text);
        JSONNode questionInfo = JSON.Parse(System.Text.Encoding.UTF8.GetString(questionInfoRequest.downloadHandler.data));
        print(JSON.Parse(System.Text.Encoding.UTF8.GetString(questionInfoRequest.downloadHandler.data)));
        //string myString = questionInfoRequest.downloadHandler.data;
        //byte[] bytes = questionInfoRequest.downloadHandler.data;
        //string myString = System.Text.Encoding.UTF8.GetString(bytes);
        //System.Text.Encoding.Convert(System.Text.Encoding.Default, System.Text.Encoding.UTF8, System.Text.Encoding.Default.GetBytes())


        // first one is questions info(its mean without response code [0] is response code),
        // second dimension is question number in this example 3rd question [0,1,2]
        // third dimension is questions info (its mean category, type, question text, correct and incorrect answers)

        //print(questionInfo[1][2]); //giving 3.questions
        //print(questionInfo[1][2][0]); // giving 3.quesion's category
        //print(questionInfo[1][2][1]); // giving 3.questions's type (multiple) 2 is = difficulty 
        //print(questionInfo[1][2][3]); // giving 3questions questionText

        //print(questionInfo[1][2][4]); // correct answers
        //print(questionInfo[1][2][5]); // incorrect answers
        //print(questionInfo[1][2][5][0]); // incorrect answer one

        // after that it will be null

        CreateScOArray(questionInfo);

        yield return null;
    }


    // creates Question Scriptable Object in run time and attach info required information to them
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

    // public method which is interacting with buttons. Assign the value of category index
    public void SetQuizCategory(int _categoryIndex)
    {
        // check restrictions (valid category index) then set the categoryIndex        
        categoryIndex = _categoryIndex;
        StartCoroutine(GetDataAndArrange());        
    }
}
