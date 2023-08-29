using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuizMenuManager : MonoBehaviour
{
    [Header("Header")]
    [SerializeField] private GameObject safeArea;

    [Header("Top Parent")]
    [SerializeField] private GameObject TopParent;
    [SerializeField] private GameObject HomeButton;
    [SerializeField] private GameObject BackButton;

    [Header("Enter Name")]
    [SerializeField] private GameObject EnterNamePage;

    [Header("Quiz Menu")]
    [SerializeField] private GameObject QuizMenu;
    [SerializeField] private GameObject questionText;
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private Button Option1Button;
    [SerializeField] private Button Option2Button;
    [SerializeField] private Button Option3Button;
    [SerializeField] private Button Option4Button;

    [Header("Correct/Wrong Page")]
    [SerializeField] private GameObject CorrectWrongPage;
    [SerializeField] private TMP_Text CorrectWrongText;
    [SerializeField] private GameObject GJTAImage;
    [SerializeField] private GameObject NextQnButton;

    [Header("Quiz Over Menu")]
    [SerializeField] private GameObject QuizOverMenu;
    [SerializeField] private GameObject QuizOverBacking;
    [SerializeField] private Button QuizToHomeButton;
    [SerializeField] private Button TryAgainButton;
    
    [Header("Scripts")]
    public SceneChanger sceneChanger;
    public QuizManager quizManager;
    public AnswerScript answerScript;
    public QuizData quizData;
    public QuizTimer quizTimer;

    [Header("Others")]
    public TMP_Text ScoreText;
    public int qnNumber;

    [Header("Background Images")]
    public Sprite enterNameBG;
    public Sprite mainBG;
    public Sprite correctBG;
    public Sprite wrongBG;

    [Header("Good Job/Try Again Images")]
    public Sprite goodJobImage;
    public Sprite tryAgainImage;
    public Sprite nextQnRight;
    public Sprite nextQnWrong;

    public Color topParentColor;


    private int latestButtonIndex;

    void Start()
    {
        // start on enter name page
        TopParent.SetActive(true);
        EnterNamePage.SetActive(true);
        HomeButton.SetActive(false);
        BackButton.SetActive(true);
        QuizMenu.SetActive(false);
        CorrectWrongPage.SetActive(false);
        QuizOverMenu.SetActive(false);
        QuizOverBacking.SetActive(false);
        questionText.SetActive(false);
        qnNumber = 1;
        changeQuizBG(0);
        topParentColor.a = 255f;
        TopParent.GetComponent<Image>().color = topParentColor;
    }

    void Update()
    {
        // update question timer
        timerText.text = "Time Remaining: " + quizTimer.getTimeRemaining().ToString("#");
        if (quizTimer.getIsTimerMoving() == true && quizTimer.getTimeRemaining() > 0)
        {
            quizTimer.updateTimer();
        }

        // if timer reaches 0, timer is reset for next question and no point is rewarded
        if (quizTimer.getTimeRemaining() < 0)
        {
            quizTimer.resetTimer(); 
            quizManager.Wrong();
        }

        // if on enter name page or quiz menu page, have top parent be active
        if (EnterNamePage.activeInHierarchy == true || QuizMenu.activeInHierarchy == true)
        {
            topParentColor.a = 255f;
            TopParent.GetComponent<Image>().color = topParentColor;
        }
        // else no top parent
        else
        {
            topParentColor.a = 0f;
            TopParent.GetComponent<Image>().color = topParentColor;
        }
    }

    public void OnCancelClicked()
    {
        Debug.Log("Button id " + ButtonReferenceManager.Instance.storedButtonID);
        ButtonReferenceManager.Instance.storedIndex = 0;
        sceneChanger.ChangeToMainScene();
    }

    public void OnContinueClicked()
    {
        // make sure enter name input has input before letting user continue to quiz
        if (quizData.nameInput.text != "")
        {
            // save name playerprefs
            quizData.SaveNames();
            // continue to quiz
            BackButton.SetActive(true);
            EnterNamePage.SetActive(false);
            QuizMenu.SetActive(true);
            questionText.SetActive(true);
            changeQuizBG(1);

            // start timer when player starts quiz
            quizTimer.startTimer();
        }
    }

    public void SetLatestButtonIndex(int index)
    {
        latestButtonIndex = index;
    }

    public int GetLatestButtonIndex()
    {
        return latestButtonIndex;
    }
    public void OnOptionClicked()
    {
        TopParent.SetActive(false);
        QuizMenu.SetActive(false);

        // if on questions <= 10
        if (qnNumber <= 10)
        {
            // stop timer, increment qn number, go to correct/wrong page
            quizTimer.setIsTimerMoving(false);
            qnNumber += 1;
            CorrectWrongPage.SetActive(true);
        
            // check if answer is correct or wrong, set page elements according to that
            if (quizManager.CheckIfButtonIsCorrect(GetLatestButtonIndex()))
            {
                CorrectWrongText.text = "Good Job!";
                changeQuizBG(2);
                GJTAImage.GetComponent<Image>().sprite = goodJobImage;
                NextQnButton.GetComponent<Image>().sprite = nextQnRight;
            }
            else
            {
                CorrectWrongText.text = "Try Again!";
                changeQuizBG(3);
                GJTAImage.GetComponent<Image>().sprite = tryAgainImage;
                NextQnButton.GetComponent<Image>().sprite = nextQnWrong;
            }
        }
    }

    public void OnNextQnClicked()
    {
        CorrectWrongPage.SetActive(false);
        TopParent.SetActive(true);
        // go to next question if current question is not 10
        if (qnNumber <= 10)
        {
            QuizMenu.SetActive(true);
            changeQuizBG(1);
            // start timer after next question is pressed
            quizTimer.startTimer();
        }
        // if finished 10 questions 
        else if (qnNumber == 11)
        {
            // end quiz, go to quiz over menu
            QuizOver();
            QuizOverMenu.SetActive(true);
            QuizOverBacking.SetActive(true);

            if (QuizOverMenu.activeInHierarchy == true)
            {
                changeQuizBG(1);
            }
        }
    }

    public void QuizOver()
    {
        // end quiz, display and save player scores, stop timer
        BackButton.SetActive(false);
        QuizMenu.SetActive(false);
        questionText.SetActive(false);
        ScoreText.text = quizManager.score + "/10";
        quizData.SaveScores();
        quizData.LoadData();
        quizTimer.setIsTimerMoving(false);
    }

    #region when on score page
    public void OnTryAgainClicked()
    {
        // retry quiz
        TopParent.SetActive(true);
        QuizMenu.SetActive(true);
        QuizOverMenu.SetActive(false);
        QuizOverBacking.SetActive(false);
        quizManager.Retry();
    }
    #endregion
    public void changeQuizBG(int index)
    {
        if (index == 0)
        {
            safeArea.GetComponent<Image>().sprite = enterNameBG;
        }
        else if (index == 1)
        {
            safeArea.GetComponent<Image>().sprite = mainBG;
        }
        else if (index == 2)
        {
            safeArea.GetComponent<Image>().sprite = correctBG;
        }
        else if (index == 3)
        {
            safeArea.GetComponent<Image>().sprite = wrongBG;
        }
        //else if (index == 4)
        //{
        //    safeArea.GetComponent<Image>().sprite = scoreBG;
        //}
    }
}
