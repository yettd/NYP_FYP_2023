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
        data = new DataManageScript("Assets/Resources/TutorialLevel/meta/", fileDirectory + TutorialNagivatorScript.Instance().get_manual.name + ".txt");

        if (data.FindFilePath()) // file existing will be overwritten to a new one
            temp = data.LoadInfoThroughJson2<InstructionManual>();

        else // file doesn't exist will be created and written to a new one
            if (PlayerPrefs.HasKey(data.GetPath() + data.GetDirectoryName())) temp = data.LoadInfoThroughJson2_1<InstructionManual>();
            else temp = TutorialNagivatorScript.Instance().get_manual;

        temp.cleared.completed = cleared;
        try { data.SaveInfoAsNewJson(temp); } catch { data.SaveInfoAsNewJson2(temp); } // ???
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
