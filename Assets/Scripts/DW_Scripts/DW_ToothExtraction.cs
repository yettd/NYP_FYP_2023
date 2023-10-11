using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DW_ToothExtraction
{
    private int teethIndex = 0;

    #region SETUP
    private void SetDamagedTooth()
    {
        teethIndex = Random.Range(0, GetTeeths().Length);
        GetTeeths()[teethIndex].transform.GetChild(Random.Range(0, GetTeeths()[teethIndex].transform.childCount)).tag = TutorialGame_Script.thisScript.get_damagedTooth;
    }

    private void RefreshToothStatus()
    {
        foreach (GameObject teeth in GetTeeths())
        {
            for (int select = 0; select < teeth.transform.childCount; select++)
            {
                if (GetTeeths()[teethIndex].transform.GetChild(select).tag == TutorialGame_Script.thisScript.get_damagedTooth)
                    GetTeeths()[teethIndex].transform.GetChild(select).GetComponent<Renderer>().material = Resources.Load<Material>("TutorialAssets/Selected_DamagedTooth");
                else
                    teeth.transform.GetChild(select).GetComponent<Renderer>().material = Resources.Load<Material>("TutorialAssets/Selected_CleanTooth");
            }
        }
    }

    private GameObject[] GetTeeths()
    {
        return GameObject.FindGameObjectsWithTag(TutorialGame_Script.thisScript.get_teethTag);
    }
    #endregion

    #region MAIN
    public void Begin()
    {
        // Set random damaged tooth
        SetDamagedTooth();

        // Update status
        RefreshToothStatus();
    }
    #endregion
}
