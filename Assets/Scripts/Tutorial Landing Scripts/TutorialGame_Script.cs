using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameTagPlacement
{ 
    DW_Camera,
    DW_Tool,
    TeethSection,
    GumSection,
    DamagedTooth,
    ToothPlacement,
    NotTagged,
}

[System.Serializable]
public class GameSetup_Data
{
    public GameObject[] props;
    public string props_tag_name;
}

public class TutorialGame_Script : MonoBehaviour
{
    public static TutorialGame_Script thisScript;

    [SerializeField] private GameSetup_Data[] gameInfo;
    public GameSetup_Data[] get_GameInfo { get { return gameInfo; } }

    private DW_ToothExtraction extraction;

    private DataManageScript data;
    private const string fileDirectory = "savefile_tutorial_";

    void Start()
    {
        thisScript = this;
        SetGamePropReady();
        SetInstructionObject();
    }

    #region SETUP
    private void UpdateInstructionStatus(bool cleared)
    {
        InstructionManual temp;
        data = new DataManageScript(data.GetPath("Assets/Resources/TutorialLevel/meta/"), fileDirectory + TutorialNagivatorScript.Instance().get_manual.name + ".txt");

        if (data.FindFilePath()) // file existing will be overwritten to a new one
            temp = data.LoadInfoThroughJson2<InstructionManual>();

        else // file doesn't exist will be created and written to a new one
            temp = TutorialNagivatorScript.Instance().get_manual;

        temp.cleared.completed = cleared;
        data.SaveInfoAsNewJson(temp);
    }

    private void SetInstructionObject()
    {
        switch (TutorialNagivatorScript.Instance().get_manual.toolAccessId)
        {
            case 1:
                extraction = new DW_ToothExtraction();
                extraction.Begin();
                break;

            default:
                break;
        }
    }

    private void SetGamePropReady()
    {
        foreach (GameSetup_Data info in gameInfo)
            foreach (GameObject prop in info.props)
                prop.tag = info.props_tag_name;
    }
    #endregion

    #region MAIN
    public void Back()
    {
        SetClearedCondition(true);
        if (TutorialNagivatorScript.getScript != null) SceneManager.LoadScene(TutorialNagivatorScript.Instance().GetTitleScene());
        else { SceneManager.LoadScene(SceneManager.GetActiveScene().name); }
    }

    public void SetClearedCondition(bool condition)
    {
        if (TutorialNagivatorScript.getScript != null)
        {
            UpdateInstructionStatus(condition);
            Debug.Log("Game Condition have been set to " + (condition ? "COMPLETE" : "NOT COMPLETE"));
        }
    }
    #endregion
}
