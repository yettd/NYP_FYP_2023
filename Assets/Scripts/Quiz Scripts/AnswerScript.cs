using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswerScript : MonoBehaviour
{
    public bool isCorrect = false;
    public QuizManager quizManager;
  
    // assigned to quiz options buttons, assigning which option is correct/wrong 
    public void Answer()
    {
        if (isCorrect)
        {
            quizManager.Correct();
        }
        else
        {
            quizManager.Wrong();
        }
    }

    public bool GetIsCorrect()
    {
        return isCorrect;
    }
}
