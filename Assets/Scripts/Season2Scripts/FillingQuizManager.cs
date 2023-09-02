using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class FillingQuizManager : MonoBehaviour
{
    public List<FillingQnA> QnA;
    public GameObject[] options;
    public int CurrentQuestion;
    public TMP_Text highScoreTextOnMain;

    public TMP_Text QuestionTxt;

    public int score = 0;
    private int totalQuestionsAsked = 0;
    public TMP_Text FinalScoreText;
    public GameObject MainPanel;
    public GameObject ResultsPanel;
    public GameObject QuizPanel;
    private int highScore;

    public bool polishFillingTools; //for later

    public void openMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    private void Start()
    {
        int savedHighScore = PlayerPrefs.GetInt("FillingHighScore", 0);
        highScoreTextOnMain.text = "High Score: " + savedHighScore + "/10";

        MainPanel.SetActive(true);
        QuizPanel.SetActive(false);
        ResultsPanel.SetActive(false);
        GenerateQuestion();
        highScore = PlayerPrefs.GetInt("FillingHighScore", 0);
    }
    public void StartQuiz()
    {
        MainPanel.SetActive(false);
        QuizPanel.SetActive(true);
        score = 0;
        totalQuestionsAsked = 0;
        GenerateQuestion();
    }
    public void RestartQuiz()
    {
        score = 0;
        totalQuestionsAsked = 0;
        ResultsPanel.SetActive(false);
        QuizPanel.SetActive(true);
        GenerateQuestion();
    }
    public void ReturnToMainPanel()
    {
        ResultsPanel.SetActive(false);
        MainPanel.SetActive(true);
        int savedHighScore = PlayerPrefs.GetInt("FillingHighScore", 0);
        highScoreTextOnMain.text = "High Score: " + savedHighScore + "/10";
    }

    public void Correct()
    {
        score++;

        totalQuestionsAsked++;
        if (totalQuestionsAsked < 10)
        {
            GenerateQuestion();
        }
        else
        {
            DisplayFinalScore();
        }
    }

    public void Wrong()
    {
        totalQuestionsAsked++;
        if (totalQuestionsAsked < 10)
        {
            GenerateQuestion();
        }
        else
        {
            DisplayFinalScore();
        }
    }

    public void setAnswers()
    {

        for (int i = 0; i < options.Length; i++)
        {
            options[i].GetComponent<FillingAnswerScript>().isFillingCorrect = false;
            options[i].transform.GetChild(0).GetComponent<TMP_Text>().text = QnA[CurrentQuestion].answers[i];

            if (QnA[CurrentQuestion].correctAnswer == i + 1)
            {
                options[i].GetComponent<FillingAnswerScript>().isFillingCorrect = true;
                Debug.Log("Correct answer: " + QnA[CurrentQuestion].correctAnswer);
            }
        }
    }

    public void GenerateQuestion()
    {
        if (QnA.Count == 0 || totalQuestionsAsked >= 10)
        {
            FinalScoreText.text = "Final Score: " + score + "/10";
            return;
        }

        CurrentQuestion = Random.Range(0, QnA.Count);
        QuestionTxt.text = QnA[CurrentQuestion].question;
        setAnswers();
        QnA.RemoveAt(CurrentQuestion);
    }

    private void DisplayFinalScore()
    {
        FinalScoreText.text = "Final Score: " + score + "/10";
        QuizPanel.SetActive(false);
        ResultsPanel.SetActive(true);

        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("FillingHighScore", highScore);
            PlayerPrefs.Save();
            FinalScoreText.text += "\nNew High Score!";
        }
        else
        {
            FinalScoreText.text += "\nHigh Score: " + highScore + "/10";
        }
    }
}
