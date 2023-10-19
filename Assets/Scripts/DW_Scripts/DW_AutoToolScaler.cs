using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ToolScale_Data
{
    public Camera cameraRef;
    public float camDepthOffset;
    public float toolScale;
}

public class DW_AutoToolScaler : MonoBehaviour
{
    [SerializeField] private ToolScale_Data[] scaleData;
    public ToolScale_Data[] get_scaleData { get { return scaleData; } }

    #region SETUP
    private Vector3 GetScaleDataForUse()
    {
        // Find scale data is been currently used
        if (scaleData != null)
        {
            // Scan through the data that been store
            foreach (ToolScale_Data cam in scaleData)

                // Finding of active cam and reference to its interal data
                if (cam.cameraRef.gameObject.activeInHierarchy)

                    // Value then go by toolScale and return to its assigned variable
                    return new Vector3(cam.toolScale, cam.toolScale, cam.toolScale);
        }

        // Return to its default value
        return new Vector3(1, 1, 1);
    }

    private GameObject GetToolTarget()
    {
        // Finding the tool that been used currently
        return GameObject.FindGameObjectWithTag(TutorialGame_Script.thisScript.get_GameInfo[(int)GameTagPlacement.DW_Tool].props_tag_name);
    }
    #endregion

    #region MAIN
    public void AutoToolScale()
    {
        // Update the tool scale value
        UpdateToolScale();
    }
    #endregion

    #region COMPONENT
    private void UpdateToolScale()
    {
        // Update the scale value only if the current scale isn't match
        if (GetToolTarget())
        {
            // Use the current one to change the scale of the tool
            GetToolTarget().transform.localScale = GetToolTarget().transform.position + GetScaleDataForUse();
        }
    }
    #endregion
}
