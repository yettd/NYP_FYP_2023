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
        // Find target of the tooth placement
        GameObject tooth = GameObject.FindGameObjectWithTag(TutorialGame_Script.thisScript.get_GameInfo[(int)GameTagPlacement.ToothPlacement].props_tag_name);
        bool anesthesicDosed = false;

        // Check on tooth placement is present
        if (tooth)
        {
            // Looking for anesthesic to all gum placement which available
            if (tooth.GetComponent<DW_AnestheicPlacement>().IsAnestheicDosed())
                anesthesicDosed = true;
            else 
                anesthesicDosed = false;

            // Extraction checker is completed
            return !anesthesicDosed;
        }

        // There is no placement found which can be checked
        return false;
    }

    private bool MultipleExtractionChecker()
    {
        // Find all target of the tooth placement
        GameObject[] tooths = GameObject.FindGameObjectsWithTag(TutorialGame_Script.thisScript.get_GameInfo[(int)GameTagPlacement.ToothPlacement].props_tag_name);
        int anesthesicDosed = 0;

        // Check on tooth placement is present
        if (tooths.Length != 0)
        {
            // Looking for any tooth which have anesthesic apply after tooth placement
            foreach (GameObject tooth in tooths)
            {
                if (tooth.GetComponent<DW_AnestheicPlacement>().IsAnestheicDosed()) anesthesicDosed++;
            }

            // Extraction checker is completed
            return !(anesthesicDosed >= tooths.Length);
        }

        // There is no placement found which can be checked
        return false;
    }
    #endregion
}
