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
    [SerializeField] GameObject mainScreen;
    [SerializeField] GameObject categoryScreen;
    [SerializeField] GameObject quizScreen;
    [SerializeField] GameObject EndScreen;
    [SerializeField] GameObject quitVerificationScreen;

    private void Start()
    {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        questionAPIController = FindObjectOfType<QuestionAPIController>();
        GetComponent<Quiz>().OnGameOver += OnGameOver;
    }

    public void ShowFinalScore()
    {
        finalScoreText.text = "Congratulations! \n Score: " + scoreKeeper.correctAnswers;
    }

    public void ShowMainScreen()
    {
        categoryScreen.SetActive(false);
        mainScreen.SetActive(true);        
    }

    public void ShowCategoryScene()
    {
        mainScreen.SetActive(false);
        categoryScreen.SetActive(true);
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitVerificationButton()
    {
        quitVerificationScreen.SetActive(true);
    }

    public void QuitVerificationNoButton()
    {
        quitVerificationScreen.SetActive(false);
    }


    public void Quit()
    {
        print("quit");
        Application.Quit();        
    }

    public void SetCategoryIndex(int index)
    {
        questionAPIController.SetQuizCategory(index);
        mainScreen.SetActive(false);
        categoryScreen.SetActive(false);
        quizScreen.SetActive(true);
    }

    void OnGameOver()
    {
        quizScreen.SetActive(false);
        ShowFinalScore();
        EndScreen.SetActive(true);
    }
}
