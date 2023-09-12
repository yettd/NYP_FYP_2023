using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialGame_Script : MonoBehaviour
{
    private DataManageScript data;
    private const string fileDirectory = "savefile_tutorial_";

    #region SETUP
    private void UpdateInstructionStatus(bool cleared)
    {
        InstructionManual temp;
        data = new DataManageScript("Assets/Resources/TutorialLevel/meta/", fileDirectory + TutorialNagivatorScript.thisScript.get_manual.name + ".txt");

        if (data.FindFilePath()) // file existing will be overwritten to a new one
            temp = data.LoadInfoThroughJson<InstructionManual>();

        else // file doesn't exist will be created and written to a new one
            temp = TutorialNagivatorScript.thisScript.get_manual;

        temp.cleared.completed = cleared;
        data.SaveInfoAsNewJson(temp);
    }
    #endregion

    #region MAIN
    public void Back()
    {
        SetClearedCondition(true);
        SceneManager.LoadScene(TutorialNagivatorScript.thisScript.GetTitleScene());
    }

    public void SetClearedCondition(bool condition)
    {
        UpdateInstructionStatus(condition);
        Debug.Log("Game Condition have been set to " + (condition ? "COMPLETE" : "NOT COMPLETE"));
    }
    #endregion
}
