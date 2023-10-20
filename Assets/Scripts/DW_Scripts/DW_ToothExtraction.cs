using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DW_ToothExtraction
{
    private DW_ToothExtraction_Addons addons;
    private int toothExtract = 0;

    #region MAIN
    public void Begin()
    {
        // Setup extraction addons
        addons = new DW_ToothExtraction_Addons();

        // Find tooth extraction length
        toothExtract = GameObject.FindGameObjectsWithTag(TutorialGame_Script.thisScript.get_GameInfo[(int)GameTagPlacement.DamagedTooth].props_tag_name).Length;
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
