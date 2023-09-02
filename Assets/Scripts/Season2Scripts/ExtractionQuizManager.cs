////using System.Collections;
////using System.Collections.Generic;
////using UnityEngine;
////using TMPro;
////using UnityEngine.UI;
////using UnityEngine.SceneManagement;
////public class ExtractionQuizManager : MonoBehaviour
////{
////    public List<QnA> QnA;
////    public GameObject[] options;
////    public int currentQn;

////    private int testScore;
////    private bool Pass = false;
////    public bool polishTools;
////    public bool isQuizOver = false;
////    public float TimeLeft = 600.0f;

////    bool Resultsactive;
////    bool Quizactive;

////    [Header("GameDoneUI")]
////    [SerializeField] private GameObject ResultsPanel;
////    [SerializeField] private Button SettingsBtn;
////    [SerializeField] private TMP_Text ResultsTXT;

//<<<<<<< Updated upstream
////    [Header("QuizUI")]
////    [SerializeField] private GameObject QuizPanel;
////    //[SerializeField] private Button Option1Btn;
////    //[SerializeField] private Button Option2Btn;
////    //[SerializeField] private Button Option3Btn;
////    //[SerializeField] private Button Option4Btn;
////    //[SerializeField] private TMP_Text QuestionsTxt;
////    [SerializeField] private TMP_Text TimerTxt;
////    [SerializeField] private TMP_Text QuestionNumber;
////    [SerializeField] private TMP_Text QuestionText;
////    [SerializeField] private TMP_Text CorrectWrongText;
//=======
//    public bool polishTools; //for later
//>>>>>>> Stashed changes

////    void Update()
////    {

////        TimeLeft -= Time.deltaTime;

////        if (TimeLeft <= 0.0f)
////        {
////            QuizOver();
////        }
////    }

////    private void Correct()
////    {
////        testScore += 1;
////        QnA.RemoveAt(currentQn);
////        GenerateQn();
////    }

////    private void Wrong()
////    {
////        QnA.RemoveAt(currentQn);
////        GenerateQn();
////    }
////    void SetAnswers()
////    {
////        for (int i = 0; i < options.Length; i++)
////        {
////            options[i].GetComponent<AnswerScript>().isCorrect = false;
////            options[i].transform.GetChild(0).GetComponent<TMP_Text>().text = QnA[currentQn].answers[i];

////            if (QnA[currentQn].correctAnswer == i + 1)
////            {
////                options[i].GetComponent<AnswerScript>().isCorrect = true;
////            }
////        }

////        Debug.Log("Correct answer: " + QnA[currentQn].correctAnswer);
////    }

////    public bool CheckIfButtonIsCorrect(int index)
////    {
////        if (index == QnA[currentQn].correctAnswer)
////        {
////            return true;
////        }
////        else
////        {
////            return false;
////        }
////    }

////    void GenerateQn()
////    {
////        if (QnA.Count > 5) // number of questions - 10, if 50 questions, input "> 40"
////        {
////            currentQn = Random.Range(0, QnA.Count);
////            //QuestionNumber.text = "Question " + quizMenuManager.qnNumber.ToString();
////            //QuestionText.text = QnA[currentQn].question;
////            SetAnswers();
////        }
////        else
////        {
////            isQuizOver = true;
////            //quizMenuManager.QuizOver();
////        }
////    }

////    private void QuizOver()
////    {
////        ExtractionResults();
////        if (Resultsactive == false)
////        {
////            ResultsPanel.transform.gameObject.SetActive(true);
////            Resultsactive = true;
////        }
////        else
////        {
////            ResultsPanel.transform.gameObject.SetActive(false);
////            Resultsactive = false;
////        }
////        if (Quizactive == true)
////        {
////            QuizPanel.transform.gameObject.SetActive(false);
////            Quizactive = false;
////        }
////    }

////    private void ExtractionResults()
////    {
////        PlayerPrefs.SetInt("SavedExtractionResults", testScore);

////        if(testScore >= 7)
////        {
////            Pass = true;
////            StudentPassedQuiz();
////        }
////    }
////    private void StudentPassedQuiz()
////    {
////        polishTools = true;
////    }
////}
