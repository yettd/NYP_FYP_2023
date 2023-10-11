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
        gameObject.tag = TutorialGame_Script.thisScript.get_toothplacement;
        GetComponent<MeshRenderer>().enabled = false;

        // Get tooth out off there
        MarkToothPlacement();
    }

    private void MarkToothPlacement()
    {
        // Make a copy of the tooth been extracted
        GameObject toothPlacement = Instantiate(gameObject);

        // Pick up the tooth by attach it on the tool
        toothPlacement.transform.SetParent(GetToolObject().transform);
    }

    private GameObject GetToolObject()
    {
        // Find the tool for the uses of placement
        return GameObject.FindGameObjectWithTag(TutorialGame_Script.thisScript.get_dwTool);
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
    private bool IsExtractionComplete()
    {
        // Finding of tooth been pulled out and doesn't shown its present
        return GetComponent<MeshRenderer>().enabled == false;
    }
    #endregion
}
