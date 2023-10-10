using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DW_ForcepsPlacement : MonoBehaviour
{
    private bool isApply = false;

    #region SETUP
    private void ExtractToothPlacement()
    {
        // Find tool and set it for use
        GameObject tool = GetToolObject();

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
        return GameObject.FindGameObjectWithTag(TutorialGame_Script.thisScript.get_dwTool);
    }
    #endregion

    #region MAIN
    public void ApplyProduct()
    {
        if (!isApply && !IsExtractionComplete())
        {
            ExtractToothPlacement();
            isApply = true;
        }
    }
    #endregion

    #region COMPONENT
    private bool IsExtractionComplete()
    {
        return GetComponent<MeshRenderer>().enabled == false;
    }
    #endregion
}
