using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialGame_Script : MonoBehaviour
{
    public static TutorialGame_Script thisScript;

    private const string dwTool = "DW_Tool";
    public string get_dwTool { get { return dwTool; } }

    private const string teethTag = "TeethSection";
    public string get_teethTag { get { return teethTag; } }

    private const string gumTag = "GumSection";
    public string get_gumTag { get { return gumTag; } }

    private const string damagedTooth = "DamagedTooth";
    public string get_damagedTooth { get { return damagedTooth; } }

    private const string toothplacement = "ToothPlacement";
    public string get_toothplacement { get { return toothplacement; } }

    private DW_ToothExtraction extraction;

    private DataManageScript data;
    private const string fileDirectory = "savefile_tutorial_";

    void Start()
    {
        thisScript = this;
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
