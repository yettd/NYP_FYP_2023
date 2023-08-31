using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class QuizManagerS2 : MonoBehaviour
{
    public List<QnA> QnA;
    public GameObject[] options;
    public int CurrentQuestion;

    public TMP_Text QuestionTxt;  

    private void Start()
    {
        GenerateQuestion();
    }

    public void Correct()
    {
        //QnA.RemoveAt(CurrentQuestion);
        GenerateQuestion();
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
        CurrentQuestion = Random.Range(0, QnA.Count);

        QuestionTxt.text = QnA[CurrentQuestion].question;
        setAnswers();

        QnA.RemoveAt(CurrentQuestion);
    }
}
