using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DW_ForcepsPlacement : MonoBehaviour
{
    private bool isApply = false;

    #region SETUP
    private void ExtractToothPlacement()
    {
        // Set a tag which define to extract them and hide its existence from been seen by the user
        gameObject.tag = TutorialGame_Script.thisScript.get_GameInfo[(int)GameTagPlacement.ToothPlacement].props_tag_name;
        GetComponent<MeshRenderer>().enabled = false;

        // Perform tool
        if (GetComponent<DW_ForcepsAdvancement>() == null) gameObject.AddComponent<DW_ForcepsAdvancement>().PerformTool(gameObject);
        else GetComponent<DW_ForcepsAdvancement>().PerformTool(gameObject);
    }
    #endregion

    #region MAIN
    public void ApplyProduct()
    {
        // Only called once when extraction are successful
        if (!isApply && !IsExtractionComplete())
        {
            // Perform extract
            ExtractToothPlacement();

            // Mark it down apply when this perform
            isApply = true;
        }
    }
    #endregion

    #region COMPONENT
    public bool IsExtractionComplete()
    {
        // Finding of tooth been pulled out and doesn't shown its present
        return GetComponent<MeshRenderer>().enabled == false;
    }
    #endregion
}
