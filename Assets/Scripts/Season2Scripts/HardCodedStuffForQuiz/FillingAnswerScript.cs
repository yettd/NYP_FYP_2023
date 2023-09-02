using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FillingAnswerScript : MonoBehaviour
{
    public bool isFillingCorrect;
    public FillingQuizManager quizManager;

    public void Answer()
    {
        if (isFillingCorrect)
        {
            Debug.Log("Correct Answer");
            quizManager.Correct();
        }
        else
        {
            Debug.Log("Wrong Answer");
            quizManager.Wrong();
        }
    }
}
