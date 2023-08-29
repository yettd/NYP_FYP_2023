using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ARExplorationSceneChanger : MonoBehaviour
{
    public void GoToMainMenu()
    {
        SceneManager.LoadScene("Main Scene");
    }
    public void GoToExploration()
    {
        SceneManager.LoadScene("Scene/AR Exploration (for next batch)/AR Exploration");
    }
    public void GoToImageScan()
    {
        SceneManager.LoadScene("Scene/AR Exploration (for next batch)/AR ImageScan");
    }
    public void GoToMultiDiffImg()
    {
        SceneManager.LoadScene("Scene/AR Exploration (for next batch)/AR MultDifferentImage");
    }
    public void GoToSameimageDiffColor()
    {
        SceneManager.LoadScene("Scene/AR Exploration (for next batch)/AR SameimageDiffColor");
    }
    public void GoToTextRecognition()
    {
        SceneManager.LoadScene("Scene/AR Exploration (for next batch)/AR TextRecognition");
    }
    public void GoToModelTargetRecognition()
    {
        SceneManager.LoadScene("Scene/AR Exploration (for next batch)/AR ModelTargetRecognition");
    }
    public void GoToModelSpawn()
    {
        SceneManager.LoadScene("Scene/AR Exploration (for next batch)/AR ModelSpawn");
    }
}
