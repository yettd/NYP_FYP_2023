using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtractionAnswerScript : MonoBehaviour
{
    public bool isCorrect;
    public ExtractionQuizManager quizManager;

    public void Answer()
    {
        if (isCorrect)
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
