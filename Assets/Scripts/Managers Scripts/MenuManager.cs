using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class MenuManager : MonoBehaviour
{
    [Header("Top Parent")]
    [SerializeField] private GameObject HomeButton;
    [SerializeField] private GameObject BackButton;
    [SerializeField] private GameObject Logo;

    [Header("Main Menu")]
    [SerializeField] private GameObject MainMenu;

    [Header("Tool Selection")]
    [SerializeField] private GameObject ToolSelectionMenu;
    [SerializeField] private GameObject AssessmentButton;

    [Header("Tool Info")]
    [SerializeField] private GameObject ToolInfoMenu;

    [Header("Settings")]
    [SerializeField] private GameObject SettingsMenu;

    [Header("Credits")]
    [SerializeField] private GameObject CreditsMenu;

    [Header("Toogle On if want to start from Menu")]
    [SerializeField] private bool startFromMenu;

    public SceneChanger sceneChanger;
    public SettingsManager settingsManager;
    void Start()
    {
        // only dt/dh, scoreboard and settings buttons active on start
        if (startFromMenu)
        {
            Logo.SetActive(true);
            HomeButton.SetActive(false);
            BackButton.SetActive(false);
            MainMenu.SetActive(true);
            ToolSelectionMenu.SetActive(false);
            ToolInfoMenu.SetActive(false);
            SettingsMenu.SetActive(false);
            CreditsMenu.SetActive(false);
        }
        AssessmentButton.SetActive(false);
        if (ButtonReferenceManager.Instance.storedButtonID == ButtonENUM.MAINSCENE)
        {
            HomeButton.SetActive(false);
            BackButton.SetActive(false);
            MainMenu.SetActive(true);
            ToolSelectionMenu.SetActive(false);
            ToolInfoMenu.SetActive(false);
            SettingsMenu.SetActive(false);
            CreditsMenu.SetActive(false);
            AssessmentButton.SetActive(false);
        }

        else if (ButtonReferenceManager.Instance.storedButtonID == ButtonENUM.TOOLSELECTION || ButtonReferenceManager.Instance.storedButtonID == ButtonENUM.TOOLINFO)//check if come from tool selection
        {
            Debug.Log("come from tool selection");
            OnDHorDTClicked();
            if (ButtonReferenceManager.Instance.storedButtonID == ButtonENUM.TOOLINFO)
            {
                OnToolClicked();
                ButtonReferenceManager.Instance.storedButtonID = ButtonENUM.TOOLSELECTION;
            }
            else
            {
                ButtonReferenceManager.Instance.storedButtonID = ButtonENUM.MAINSCENE;
                Debug.Log("show the tool info wth the index of  " + ButtonReferenceManager.Instance.storedIndex);

            }
        }
    }

    #region from main menu
    // click
    // or DH button from main menu
    public void OnDHorDTClicked()
    {
        Logo.SetActive(false);
        MainMenu.SetActive(false);
        ToolSelectionMenu.SetActive(true);
        BackButton.SetActive(true);
        HomeButton.SetActive(false);
        AssessmentButton.SetActive(true);

        //We had to take this out since it was unstable D;
        //Only show for DT but not for DH
        //// scan only for DT page
        //if (ButtonReferenceManager.Instance.storedDTHButtonID == DTHEnum.DT)
        //{
        //    //ScanButton.SetActive(true);
        //}
        //else if (ButtonReferenceManager.Instance.storedDTHButtonID == DTHEnum.DH)
        //{
        //    ScanButton.SetActive(false);
        //}
    }

    // from main menu to ar scene
    // NOT IN USE, PART OF EXPLORATION
    public void OnScanClicked()
    {
        sceneChanger.ChangeToScanScene();
    }

    // change to leaderboard scene
    public void OnScoresClicked()
    {
        sceneChanger.ChangeToScoreScene();
    }

    // go to settings page
    public void OnSettingsClicked()
    {
        Logo.SetActive(false);
        MainMenu.SetActive(false);
        SettingsMenu.SetActive(true);
        BackButton.SetActive(true);
        HomeButton.SetActive(false);
    }
    #endregion

    #region from tool selection
    //  from tool selection to assessment
    public void OnAssessmentClicked()
    {
        AudioPlayer.Instance.PlayAudioOneShot(0, .5f);
        sceneChanger.ChangeToQuizScene();
    }

    //  from tool selection to tool info
    public void OnToolClicked()
    {
        ToolSelectionMenu.SetActive(false);
        ToolInfoMenu.SetActive(true);
        BackButton.SetActive(true);
        HomeButton.SetActive(true);
        AssessmentButton.SetActive(false);
    }

    // change to spawn scene
    public void OnSpawnClicked()
    {
        AudioPlayer.Instance.PlayAudioOneShot(0, .5f);
        sceneChanger.ChangeToSpawnScene();
    }
    #endregion


    #region from tool info
    //  from tool info to tool selection
    public void FromInfoToSelection()
    {
        //AudioPlayer.Instance.PlayAudioOneShot(0, .5f);
        ToolSelectionMenu.SetActive(true);
        ToolInfoMenu.SetActive(false);
        BackButton.SetActive(true);
        HomeButton.SetActive(false);
        AssessmentButton.SetActive(true);
        ButtonReferenceManager.Instance.storedButtonID = ButtonENUM.MAINSCENE;
    }
    //  irfan note: from tool info to demo
    #endregion

    //  irfan note: NEED TO CHANGE CFM DOUBLE CFM
    // change to demo video scene
    public void OnDemoVidClicked()
    {
        AudioPlayer.Instance.PlayAudioOneShot(0, .5f);
        sceneChanger.ChangeToVideoScene();
    }

    //  Home button or go back to main menu
    public void OnHomeClicked()
    {
        ButtonReferenceManager.Instance.storedDTHButtonID = DTHEnum.NONE;
        Logo.SetActive(true);
        HomeButton.SetActive(false);
        BackButton.SetActive(false);
        MainMenu.SetActive(true);
        ToolSelectionMenu.SetActive(false);
        ToolInfoMenu.SetActive(false);
        AssessmentButton.SetActive(false);
        SettingsMenu.SetActive(false);
        CreditsMenu.SetActive(false);
    }

    // change to credits page
    public void OnCreditsClicked()
    {
        AudioPlayer.Instance.PlayAudioOneShot(0, .5f);
        SettingsMenu.SetActive(false);
        CreditsMenu.SetActive(true);
        BackButton.SetActive(true);
        HomeButton.SetActive(true);
    }

    // go back to settings from credits
    public void FromCreditsToSettings()
    {
        ButtonReferenceManager.Instance.storedDTHButtonID = DTHEnum.NONE;
        AudioPlayer.Instance.PlayAudioOneShot(0, .5f);
        SettingsMenu.SetActive(true);
        CreditsMenu.SetActive(false);
        BackButton.SetActive(true);
        HomeButton.SetActive(false);
        ButtonReferenceManager.Instance.storedButtonID = ButtonENUM.MAINSCENE;
    }
}