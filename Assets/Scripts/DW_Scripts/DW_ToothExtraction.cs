using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DW_ToothExtraction
{
    private const string damagedtooth_assetPath = "TutorialAssets/Selected_DamagedTooth";
    private const string cleantooth_assetPath = "TutorialAssets/Selected_CleanTooth";

    private int teethIndex = 0;

    #region SETUP
    private void SetDamagedTooth()
    {
        // Get random teeth section
        teethIndex = Random.Range(0, GetTeeths().Length);

        // Mark a tooth of the selected teeth section and tag it
        GetTeeths()[teethIndex].transform.GetChild(Random.Range(0, GetTeeths()[teethIndex].transform.childCount)).tag = TutorialGame_Script.thisScript.get_GameInfo[(int)GameTagPlacement.DamagedTooth].props_tag_name;
    }

    private void RefreshToothStatus()
    {
        // Teeth Section
        foreach (GameObject teeth in GetTeeths())
            // Tooth Section
            for (int select = 0; select < teeth.transform.childCount; select++)
                // Damaged Tooth: Tag
                if (GetTeeths()[teethIndex].transform.GetChild(select).tag == TutorialGame_Script.thisScript.get_GameInfo[(int)GameTagPlacement.DamagedTooth].props_tag_name)
                    // Damage Tooth: Appearance
                    GetTeeths()[teethIndex].transform.GetChild(select).GetComponent<Renderer>().material = Resources.Load<Material>(damagedtooth_assetPath);
                else
                    // Clean Tooth : Appearance
                    teeth.transform.GetChild(select).GetComponent<Renderer>().material = Resources.Load<Material>(cleantooth_assetPath);
    }

    private GameObject[] GetTeeths()
    {
        // Locate the teeth section and select them in array form
        return GameObject.FindGameObjectsWithTag(TutorialGame_Script.thisScript.get_GameInfo[(int)GameTagPlacement.TeethSection].props_tag_name);
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
