using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
// Marcus
// this script is used to save and load player names and scores for leaderboard
public class QuizData : MonoBehaviour
{
    public TMP_InputField nameInput;
    public TMP_Text scoreText;

    public QuizManager quizManager;
    public HighScoreTable highScoreTable;

    public void SaveNames()
    {
        PlayerPrefs.SetString("Names" , nameInput.text);
    }

    public void SaveScores()
    {
        PlayerPrefs.SetInt("Scores" , quizManager.score);
    }

    public void LoadData()
    {
        highScoreTable.AddHighscoreEntry(PlayerPrefs.GetInt("Scores"), PlayerPrefs.GetString("Names"));
        //Debug.Log("load data with name " + PlayerPrefs.GetString("Names") + " with score " + PlayerPrefs.GetInt("Scores"));
    }
}
