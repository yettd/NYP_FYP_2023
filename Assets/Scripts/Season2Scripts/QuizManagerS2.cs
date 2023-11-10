using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class QuizManagerS2 : MonoBehaviour
{
    private AudioManager audioManager;

    List<QnA> QnA;
    public GameObject[] options;
    public int CurrentQuestion;
    public TMP_Text highScoreTextOnMain;
    public TMP_Text QuestionTxt;

    public TMP_Text timerText;
    public TMP_Text title;
    public float quizDuration = 100f; 
    private float timeRemaining;

    public int score = 0;
    private float totalQuestionsAsked = 0;
    public TMP_Text FinalScoreText;
    public GameObject MainPanel;
    public GameObject ResultsPanel;
    public GameObject QuizPanel;
    private int highScore;

    //public bool polishExtractionTools; //for later

    private void Awake()
    {
        audioManager = AudioManager.Instance;

        if (audioManager == null)
        {
            Debug.LogError("AudioManager not found.");
        }

        if (audioManager != null)
        {
            audioManager.PlayBackgroundMusic(2);
        }
    }
    public void openMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    private void Start()
    {
        int savedHighScore = 0;
        if (ButtonReferenceManager.Instance.storeCollectionID==CollectionEnum.S)
        {
            QnA = Resources.Load<quiz>("Quiz/Scaling").QnA;

            savedHighScore = PlayerPrefs.GetInt("SHighScore", 0);

            title.text = "Scaling Quiz";
        }
        else if (ButtonReferenceManager.Instance.storeCollectionID == CollectionEnum.E)
        {

            QnA = Resources.Load<quiz>("Quiz/Extration").QnA;

            savedHighScore = PlayerPrefs.GetInt("EHighScore", 0);
            title.text = "Extration Quiz";
        }
        else if (ButtonReferenceManager.Instance.storeCollectionID == CollectionEnum.F)
        {

            QnA = Resources.Load<quiz>("Quiz/Filling").QnA;

            savedHighScore = PlayerPrefs.GetInt("FHighScore", 0);
            title.text = "Filling Quiz";
        }

        highScoreTextOnMain.text = "High Score: " + savedHighScore + "/10";

        MainPanel.SetActive(true);
        QuizPanel.SetActive(false);
        ResultsPanel.SetActive(false);
        GenerateQuestion();
        if (ButtonReferenceManager.Instance.storeCollectionID == CollectionEnum.S)
        {
            highScore = PlayerPrefs.GetInt("SHighScore", 0);
        }
        else if (ButtonReferenceManager.Instance.storeCollectionID == CollectionEnum.E)
        {

            highScore = PlayerPrefs.GetInt("EHighScore", 0);
        }
        else if (ButtonReferenceManager.Instance.storeCollectionID == CollectionEnum.F)
        {

            highScore = PlayerPrefs.GetInt("FHighScore", 0);
        }
    }

    public void StartQuiz()
    {
        MainPanel.SetActive(false);
        QuizPanel.SetActive(true);
        score = 0;

        totalQuestionsAsked = 0;
        GenerateQuestion();

        timeRemaining = quizDuration;
        UpdateTimerDisplay();
    }

    //public void RestartQuiz()
    //{
    //    score = 0;
    //    totalQuestionsAsked = 0;
    //    ResultsPanel.SetActive(false);
    //    QuizPanel.SetActive(true);
    //    GenerateQuestion();
    //}

    public void ReturnToMainPanel()
    {
        ResultsPanel.SetActive(false);
        MainPanel.SetActive(true);
        SceneManager.LoadScene("ExtractioQuiz");
        int savedHighScore = PlayerPrefs.GetInt("HighScore", 0);
        if (ButtonReferenceManager.Instance.storeCollectionID == CollectionEnum.S)
        {
            savedHighScore = PlayerPrefs.GetInt("SHighScore", 0);
        }
        else if (ButtonReferenceManager.Instance.storeCollectionID == CollectionEnum.E)
        {
            savedHighScore = PlayerPrefs.GetInt("EHighScore", 0);
        }
        else if (ButtonReferenceManager.Instance.storeCollectionID == CollectionEnum.F)
        {

            savedHighScore = PlayerPrefs.GetInt("FHighScore", 0);
        }
     
        highScoreTextOnMain.text = "High Score: " + savedHighScore + "/10";
    }

    public void Correct()
    {
        score++;
        audioManager.PlaySFX(4);
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
        audioManager.PlaySFX(4);
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
            options[i].GetComponent<AnswerScript>().isCorrect = false;
            options[i].transform.GetChild(0).GetComponent<TMP_Text>().text = QnA[CurrentQuestion].answers[i];

            if (QnA[CurrentQuestion].correctAnswer == i + 1)
            {
                options[i].GetComponent<AnswerScript>().isCorrect = true;
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

        if (score >= totalQuestionsAsked / 100 * 70)
        {
            //polishExtractionTools = true;
            cleanPolish();
        }
        if (score > highScore)
        {
            highScore = score;
            if (ButtonReferenceManager.Instance.storeCollectionID == CollectionEnum.S)
            {
                PlayerPrefs.SetInt("SHighScore", highScore);
            }
            else if (ButtonReferenceManager.Instance.storeCollectionID == CollectionEnum.E)
            {

                PlayerPrefs.SetInt("EHighScore", highScore);
            }
            else if (ButtonReferenceManager.Instance.storeCollectionID == CollectionEnum.F)
            {

                PlayerPrefs.SetInt("FHighScore", highScore);
            }

     
            PlayerPrefs.Save();
            FinalScoreText.text += "\nNew High Score!";
        }
        else
        {
            FinalScoreText.text += "\nHigh Score: " + highScore + "/10";
        }
    }

    private void Update()
    {
        if (QuizPanel.activeSelf && timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            UpdateTimerDisplay();

            if (timeRemaining <= 0)
            {
                timeRemaining = 0;
                DisplayFinalScore();
            }
        }
    }

    private  void cleanPolish()
    {
        if(ButtonReferenceManager.Instance.storeCollectionID== CollectionEnum.S)
        {
            foreach(DentistTool a in ButtonReferenceManager.Instance.S)
            {
                a.rusty = false;
            }
        }
        if (ButtonReferenceManager.Instance.storeCollectionID == CollectionEnum.E)
        {
            foreach (DentistTool a in ButtonReferenceManager.Instance.E)
            {
                a.rusty = false;
            }
        }
        if (ButtonReferenceManager.Instance.storeCollectionID == CollectionEnum.F)
        {
            foreach (DentistTool a in ButtonReferenceManager.Instance.F)
            {
                a.rusty = false;
            }
        }
    }

    private void UpdateTimerDisplay()
    {
        int minutes = Mathf.FloorToInt(timeRemaining / 60);
        int seconds = Mathf.FloorToInt(timeRemaining % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
