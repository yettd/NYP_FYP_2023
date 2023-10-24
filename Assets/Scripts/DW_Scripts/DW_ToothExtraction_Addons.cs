using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DW_ToothExtraction_Addons
{
    #region MAIN
    public bool IsFailed_ExtractionLiveCheck(int index)
    {
        // Check for number of tooth needed to be extract
        switch (index)
        {
            case 1: // Only need to find a tooth for checking
                return SingleExtractionChecker();

            default: // Find all of them for checking
                return MultipleExtractionChecker();
        }
    }
    #endregion

    #region COMPONENT
    private bool SingleExtractionChecker()
    {
        // Find tooth which targeted for extraction
        GameObject sector = GameObject.FindGameObjectWithTag(TutorialGame_Script.thisScript.get_GameInfo[(int)GameTagPlacement.GumSection].props_tag_name);
        bool anestheicDosed = false;

        // Get tooth placement if found and check for dosed to process
        if (GameObject.FindGameObjectWithTag(TutorialGame_Script.thisScript.get_GameInfo[(int)GameTagPlacement.ToothPlacement].props_tag_name))
        {
            // Find if placement are set as for using it then output the result for verification
            if (sector && sector.GetComponent<DW_AnestheicPlacement>() != null)
                anestheicDosed = sector.GetComponent<DW_AnestheicPlacement>().IsAnestheicDosed();

            // Finalize the output
            return !anestheicDosed;
        }

        // Remain false until the check is found
        return anestheicDosed;
    }

    private bool MultipleExtractionChecker()
    {
        // Find tooth which have tooth extract
        GameObject[] multipleTooth = GameObject.FindGameObjectsWithTag(TutorialGame_Script.thisScript.get_GameInfo[(int)GameTagPlacement.ToothPlacement].props_tag_name);
        int anestheicDosed = 0;
        int totalAnestheicDosed = 0;

        // Get tooth individual component
        foreach (GameObject tooth in multipleTooth)
        {
            // Find if there are placement attract to it
            if (tooth && tooth.GetComponent<DW_ForcepsPlacement>() != null && tooth.GetComponent<DW_ForcepsPlacement>().IsExtractionComplete())
            {
                // Get the total amount of anestheic dosed
                totalAnestheicDosed++;

                // Get dosed and raise the amount by 1
                if (tooth.GetComponent<DW_AnestheicPlacement>().IsAnestheicDosed()) anestheicDosed++;
            }
        }

        // Finalize the output by comparing the tooth extract to the number of dosed
        return anestheicDosed < totalAnestheicDosed;
    }
    #endregion
}
