using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class MenuManager: MonoBehaviour
{
    [SerializeField] TMP_Text finalScoreText;
    ScoreKeeper scoreKeeper;
    QuestionAPIController questionAPIController;
    [SerializeField] GameObject categoryScreen;
    [SerializeField] GameObject quizScreen;

    private void Start()
    {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        questionAPIController = FindObjectOfType<QuestionAPIController>();
    }

    public void ShowFinalScore()
    {
        finalScoreText.text = "Congratulations! \n Score: " + scoreKeeper.CalculateScore() + "%";
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void SetCategoryIndex(int index)
    {
        questionAPIController.SetQuizCategory(index);
        categoryScreen.SetActive(false);
        quizScreen.SetActive(true);
    }
}
