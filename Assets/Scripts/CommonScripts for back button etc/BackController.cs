using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackController : MonoBehaviour
{
    public MenuManager menuManager;

    void Start()
    {
        ////menuManager = GetComponent<MenuManager>();
        //sceneChanger = GetComponent<SceneChanger>();
        //SceneManager.sceneLoaded += OnSceneLoaded;
    }

    

    public void GoBackTo()
    {
        if (checkIfInMainScene())
        {
            //In the same scene
            TurnOnOffGameObj(ButtonReferenceManager.Instance.storedButtonID, ButtonReferenceManager.Instance.storedDTHButtonID);
        }
        else
        {
            changeBackToOldScene();
        }
    }

    private void changeBackToOldScene()
    {
        //ButtonReferenceManager.Instance.storedDTHButtonID = DTHEnum.NONE;

        SceneChanger.Instance.ChangeToMainScene();
    }

    private void TurnOnOffGameObj(ButtonENUM storedButtonID, DTHEnum storedDTHButtonID)
    {
        switch (storedButtonID)
        {
            case ButtonENUM.MAINSCENE:
                //Debug.Log("go back to MAINSCENE");
                ButtonReferenceManager.Instance.storedDTHButtonID = DTHEnum.NONE;
                menuManager.OnHomeClicked();
                break;
            case ButtonENUM.TOOLSELECTION:
                //Debug.Log("go back to SELECTIONSCREEN");
                menuManager.FromInfoToSelection();
                break;
            case ButtonENUM.ARBIT:
                //Debug.Log("go back to ARBIT");
                SceneChanger.Instance.ChangeToScanScene();
                break;
            case ButtonENUM.TOOLINFO:
                //Debug.Log("go back to TOOLINFO");
                SceneChanger.Instance.ChangeToMainScene();
                menuManager.OnToolClicked();
                break;
            case ButtonENUM.ASSESSMENT:
                //Debug.Log("go back to ASSESSMENT");
                SceneChanger.Instance.ChangeToQuizScene();
                break;
            case ButtonENUM.DEMOVID:
                //Debug.Log("go back to DEMOVID");
                SceneChanger.Instance.ChangeToVideoScene();
                break;
            case ButtonENUM.SETTINGS:
                menuManager.FromCreditsToSettings();
                break;
        }
    }

    private bool checkIfInMainScene()
    {
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Main Scene"))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}


