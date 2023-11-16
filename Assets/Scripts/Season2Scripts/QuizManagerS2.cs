using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
    public float quizDuration = 60f; 
    private float timeRemaining;

    public int score = 0;
    private float totalQuestionsAsked = 0;
    public TMP_Text FinalScoreText;
    public GameObject MainPanel;
    public GameObject ResultsPanel;
    public GameObject QuizPanel;
    public GameObject correctImage;
    public GameObject wrongImage;
    public GameObject NoTouchPanel;

    public Image pristineTeethImage;
    public Image slightlyDecayedTeethImage;
    public Image fullyDecayedTeethImage;

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
    private void Update()
    {
        if(Input.GetKey(KeyCode.B))
        {

            PlayerPrefs.DeleteKey("SHighScore");
        }
        if (QuizPanel.activeSelf && timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            UpdateTeethImagesBasedOnTime(timeRemaining);
            UpdateTimerDisplay();

            if (timeRemaining <= 0)
            {
                timeRemaining = 0;
                DisplayFinalScore();
            }
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
        LoadAndShuffleQuestions();
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

    public IEnumerator ShowImage(GameObject gameObject)
    {
        gameObject.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        gameObject.SetActive(false);
    }

    public void Correct()
    {
        score++;
        audioManager.PlaySFX(4);
        StartCoroutine(ShowImage(correctImage));
        StartCoroutine(ShowImage(NoTouchPanel));

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
        score++;
        totalQuestionsAsked++;
        audioManager.PlaySFX(12);
        StartCoroutine(ShowImage(wrongImage));
        StartCoroutine(ShowImage(NoTouchPanel));
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
            options[i].GetComponent<AnswerScript>().isCorrect = (i == QnA[CurrentQuestion].correctAnswer - 1);
            options[i].transform.GetChild(0).GetComponent<TMP_Text>().text = QnA[CurrentQuestion].answers[i];
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
        ShuffleAnswers(QnA[CurrentQuestion]); // Shuffle answers
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
        if (score >= totalQuestionsAsked / 100 * 70)
        {
            cleanPolish();
            //polishExtractionTools = true;
            if (ButtonReferenceManager.Instance.storeCollectionID == CollectionEnum.S)
            {
                achivmen.instance.UnlockAchivement(9,"asdasd");
            }
            else if (ButtonReferenceManager.Instance.storeCollectionID == CollectionEnum.E)
            {
                achivmen.instance.UnlockAchivement(11, "asdasd");
            }
            else if (ButtonReferenceManager.Instance.storeCollectionID == CollectionEnum.F)
            {

                achivmen.instance.UnlockAchivement(10, "asdasd");
            }

            Debug.Log($"{score}: {highScore}");
       
        }
       
    }
    


    private  void cleanPolish()
    {
        
        CollectionBase CB;
        string b = Saving.save.LoadFromJson("collection");
        if (b != null)
        {
            CB = JsonUtility.FromJson<CollectionBase>(Saving.save.LoadFromJson("collection"));

            if (ButtonReferenceManager.Instance.storeCollectionID == CollectionEnum.S)
            {
                foreach (DentistTool a in ButtonReferenceManager.Instance.S)
                {
                    for (int i = 0; i < CB.toolsName.Count; i++)
                    {
                        Debug.Log($"{a.Name} || {CB.toolsName[i]}");
                        if(a.Name == CB.toolsName[i])
                        {

                            Debug.Log($"asdjoiajfdoiajfoiwejgwhesuigh");
                            CB.rusty[i] = false;
                            a.rusty = false;
                            break;
                        }
                    }
                }
            }
            if (ButtonReferenceManager.Instance.storeCollectionID == CollectionEnum.E)
            {
                foreach (DentistTool a in ButtonReferenceManager.Instance.E)
                {
                    for (int i = 0; i < CB.toolsName.Count; i++)
                    {
                        if (a.Name == CB.toolsName[i])
                        {

                            CB.rusty[i] = false;
                            a.rusty = false;
                            break;
                        }
                    }
                }
            }
            if (ButtonReferenceManager.Instance.storeCollectionID == CollectionEnum.F)
            {
                foreach (DentistTool a in ButtonReferenceManager.Instance.F)
                {
                    for (int i = 0; i < CB.toolsName.Count; i++)
                    {
                        if (a.Name == CB.toolsName[i])
                        {

                            CB.rusty[i] = false;
                            a.rusty = false;
                            break;
                        }
                    }
                }
            }
            Saving.save.saveToJson(CB, "collection");
        }



    }

    private void UpdateTimerDisplay()
    {
        int minutes = Mathf.FloorToInt(timeRemaining / 60);
        int seconds = Mathf.FloorToInt(timeRemaining % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    private void LoadAndShuffleQuestions()
    {
        if (ButtonReferenceManager.Instance.storeCollectionID == CollectionEnum.S)
        {
            QnA = new List<QnA>(Resources.Load<quiz>("Quiz/Scaling").QnA);
        }
        else if (ButtonReferenceManager.Instance.storeCollectionID == CollectionEnum.E)
        {
            QnA = new List<QnA>(Resources.Load<quiz>("Quiz/Extration").QnA);
        }
        else if (ButtonReferenceManager.Instance.storeCollectionID == CollectionEnum.F)
        {
            QnA = new List<QnA>(Resources.Load<quiz>("Quiz/Filling").QnA);
        }

        ShuffleQuestions(QnA);
    }

    private void ShuffleQuestions(List<QnA> questions)
    {
        for (int i = 0; i < questions.Count; i++)
        {
            QnA temp = questions[i];
            int randomIndex = Random.Range(i, questions.Count);
            questions[i] = questions[randomIndex];
            questions[randomIndex] = temp;
        }
    }

    private void ShuffleAnswers(QnA question)
    {
        int correctAnswerIndex = question.correctAnswer - 1; 
        string correctAnswer = question.answers[correctAnswerIndex];

        for (int i = 0; i < question.answers.Length; i++)
        {
            string temp = question.answers[i];
            int randomIndex = Random.Range(i, question.answers.Length);
            question.answers[i] = question.answers[randomIndex];
            question.answers[randomIndex] = temp;
        }
        for (int i = 0; i < question.answers.Length; i++)
        {
            if (question.answers[i] == correctAnswer)
            {
                correctAnswerIndex = i;
                break;
            }
        }
        question.correctAnswer = correctAnswerIndex + 1; 
    }

    private void UpdateTeethImagesBasedOnTime(float remainingTime)
    {
        float firstThreshold = quizDuration * 0.66f;
        float secondThreshold = quizDuration * 0.33f; 

        pristineTeethImage.color = new Color(1, 1, 1, remainingTime > firstThreshold ? 1 : 0);
        slightlyDecayedTeethImage.color = new Color(1, 1, 1, remainingTime <= firstThreshold && remainingTime > secondThreshold ? 1 : 0);
        fullyDecayedTeethImage.color = new Color(1, 1, 1, remainingTime <= secondThreshold ? 1 : 0);
    }
}
