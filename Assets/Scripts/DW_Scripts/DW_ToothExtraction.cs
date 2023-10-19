using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DW_ToothExtraction
{
    private DW_ToothExtraction_Addons addons;

    private const string damagedtooth_assetPath = "TutorialAssets/Selected_DamagedTooth";
    private const string cleantooth_assetPath = "TutorialAssets/Selected_CleanTooth";

    private int teethIndex = 0;
    private int toothExtract = 0;
    
    #region SETUP
    private void SetDamagedTooth()
    {
        // Get random teeth section
        teethIndex = Random.Range(0, GetTeeths().Length);

        // Get random tooth amount to be extract
        toothExtract = 1;

        // Mark a tooth of the selected teeth section and tag it
        for (int damaged = 0; damaged < toothExtract; damaged++)
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
        // Setup extraction addons
        addons = new DW_ToothExtraction_Addons();

        // Set random damaged tooth
        //

        // Update status
        //
    }

    public bool IsCompleted()
    {
        // Finding of damaged tooth and tooth placement
        bool condition = GameObject.FindGameObjectWithTag(TutorialGame_Script.thisScript.get_GameInfo[(int)GameTagPlacement.DamagedTooth].props_tag_name) ||
            GameObject.FindGameObjectWithTag(TutorialGame_Script.thisScript.get_GameInfo[(int)GameTagPlacement.ToothPlacement].props_tag_name);

        // This game tag must not be found unfinished
        return !condition;
    }

    public bool IsFailed()
    {
        // Live Checker: Extraction Task
        bool condition = addons.IsFailed_ExtractionLiveCheck(toothExtract);

        // Not all the step are met as its required
        return condition;
    }

    public string GetExtractionProgressStatus()
    {
        // Gather the total extracted tooth in tag
        int currentProgress = GameObject.FindGameObjectsWithTag(TutorialGame_Script.thisScript.get_GameInfo[(int)GameTagPlacement.ToothExtracted].props_tag_name).Length;

        // Gather the total tag with tooth in progress 
        int currentGoal = GameObject.FindGameObjectsWithTag(TutorialGame_Script.thisScript.get_GameInfo[(int)GameTagPlacement.DamagedTooth].props_tag_name).Length 
            + GameObject.FindGameObjectsWithTag(TutorialGame_Script.thisScript.get_GameInfo[(int)GameTagPlacement.ToothPlacement].props_tag_name).Length
            + GameObject.FindGameObjectsWithTag(TutorialGame_Script.thisScript.get_GameInfo[(int)GameTagPlacement.ToothExtracted].props_tag_name).Length;

        // With the total extracted tooth and the require extract tooth. You can tell how much is need to win
        return currentProgress + "/" + currentGoal;
    }
    #endregion
}
