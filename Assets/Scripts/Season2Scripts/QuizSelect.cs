using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class QuizSelect : MonoBehaviour
{
    public void openExtraction()
    {
        SceneManager.LoadScene(1);
    }
    public void openFilling()
    {
        SceneManager.LoadScene(2);
    }
    public void openScaling()
    {
        SceneManager.LoadScene(3);
    }
    //public void openExtraction(string ExtractioQuiz)
    //{
    //    SceneManager.LoadScene(ExtractioQuiz);
    //}
    //public void openFilling(string FillingQuiz)
    //{
    //    SceneManager.LoadScene(FillingQuiz);
    //}
    //public void openScaling(string ScalingQuiz)
    //{
    //    SceneManager.LoadScene(ScalingQuiz);
    //}
}
