//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class QuizTimer : MonoBehaviour
//{
//    private float timeRemaining;
//    private bool isTimerMoving = false;

//    [Header("Scripts")]
//    public QuizMenuManager quizMenuManager;
//    public QuizManager quizManager;
//    public void startTimer()
//    {
//        timeRemaining = 15;
//        isTimerMoving = true;
//    }

//    // used when timer expires on a question 
//    public void resetTimer()
//    {
//        // set timer back to default value and pause first
//        timeRemaining = 15;
//        isTimerMoving = false;
//        quizMenuManager.OnOptionClicked();
//        quizManager.QuestionNumber.text = "Question " + quizMenuManager.qnNumber.ToString();
//    }

//    public void updateTimer()
//    {
//        timeRemaining -= Time.deltaTime;
//    }

//    #region timer moving getter and setter
//    public bool getIsTimerMoving()
//    {
//        return isTimerMoving;
//    }
//    public void setIsTimerMoving(bool timerMoving)
//    {
//        isTimerMoving = timerMoving;
//    }
//    #endregion

//    public float getTimeRemaining()
//    {
//        return timeRemaining;
//    }
//}
