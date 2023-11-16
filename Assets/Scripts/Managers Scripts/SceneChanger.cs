using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Marcus
public class SceneChanger : MonoBehaviour
{
    public static SceneChanger Instance { get; private set; }
    private void Awake()
    {
        // irfan
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }
    public void ChangeToScanScene()
    {
        SceneManager.LoadScene("AR Scan Scene"); 
    }
    public void ChangeToSpawnScene()
    {
        achivmen.instance.UnlockAchivement(8);
        SceneManager.LoadScene("AR Spawn Scene");
    }
    public void ChangeToVideoScene()
    {
        SceneManager.LoadScene("Demo Videos");
    }
    public void ChangeToMainScene()
    {
        SceneManager.LoadScene("Main Scene");
    }

    public void ChangeToQuizScene()
    {
        SceneManager.LoadScene("Quiz Scene");
    }
    public void ChangeToScoreScene()
    {
        SceneManager.LoadScene("Leaderboard");
    }
    public void ChangeToExploration()
    {
        SceneManager.LoadScene("Scene/AR Exploration (for next batch)/AR Exploration");
    }
}
