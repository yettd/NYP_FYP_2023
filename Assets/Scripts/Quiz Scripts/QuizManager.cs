using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class QuizManager : MonoBehaviour
{
    //public List<QnA> QnA;
    //public GameObject[] options;
    //public int currentQn;

    //public TMP_Text QuestionNumber;
    //public TMP_Text QuestionText;
    //public TMP_Text CorrectWrongText;

    //public int totalQuestions = 0;
    //public int score = 0;
    //public bool isQuizOver = false;
    //public bool correctAnsPressed;

    //[Header("QuizMenuManager")]
    //public QuizMenuManager quizMenuManager;

    //[Header("Answer Script")]
    //public AnswerScript answerScript;

    //private void Start()
    //{
    //    totalQuestions = QnA.Count;
    //    GenerateQn();
    //}

    //// reload scene when user retries quia
    //public void Retry()
    //{
    //    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    //}

    //// add score if answer is correct
    //public void Correct()
    //{
    //    score += 1;
    //    QnA.RemoveAt(currentQn);
    //    GenerateQn();
    //    AudioPlayer.Instance.PlayAudioOneShot(1);
    //}

    //// dont add score and generate next qn
    //public void Wrong()
    //{
    //    QnA.RemoveAt(currentQn);
    //    GenerateQn();
    //    AudioPlayer.Instance.PlayAudioOneShot(2);
    //}

    //void SetAnswers()
    //{
    //    // set 3 options to wrong answer, 1 correct
    //    for (int i = 0; i < options.Length; i++)
    //    {
    //        options[i].GetComponent<AnswerScript>().isCorrect = false;
    //        options[i].transform.GetChild(0).GetComponent<TMP_Text>().text = QnA[currentQn].answers[i];

    //        if (QnA[currentQn].correctAnswer == i + 1)
    //        {
    //            options[i].GetComponent<AnswerScript>().isCorrect = true;
    //        }
    //    }

    //   Debug.Log("Correct answer: " + QnA[currentQn].correctAnswer);
    //}

    //// check if correct answer is clicked
    //public bool CheckIfButtonIsCorrect(int index)
    //{
    //    if (index == QnA[currentQn].correctAnswer)
    //    {
    //        //Debug.Log("quizManager.CheckIfButtonIsCorrect == true");
    //        return true;
    //    }
    //    else
    //    {
    //        //Debug.Log("quizManager.CheckIfButtonIsCorrect == false");
    //        return false;
    //    }
    //}

    //// generate random question
    //void GenerateQn()
    //{
    //    if (QnA.Count > 5) // number of questions - 10, if 50 questions, input "> 40"
    //    {
    //        currentQn = Random.Range(0, QnA.Count);
    //        QuestionNumber.text = "Question " + quizMenuManager.qnNumber.ToString();
    //        QuestionText.text = QnA[currentQn].question;
    //        SetAnswers();
    //    }
    //    else
    //    {
    //        isQuizOver = true;
    //        quizMenuManager.QuizOver();
    //    }
    //}
}
