using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DW_ToolMarker
{
    private string targetForUse;
    private GameObject[] accessors;
    public GameObject[] getAccessors { get { return accessors; } }

    public DW_ToolMarker(string tag)
    {
        targetForUse = tag;
    }

    #region COMPONENT
    private void SetMarkerSelection(GameObject accessor)
    {
        if (accessor.GetComponent<DW_SelectionComponent>() == null) accessor.AddComponent<DW_SelectionComponent>().Select();
        else accessor.GetComponent<DW_SelectionComponent>().Select();
    }

    private void DisableMarkerSelection(GameObject accessor)
    {
        if (accessor.GetComponent<DW_SelectionComponent>() != null) accessor.GetComponent<DW_SelectionComponent>().DeSelect();
    }
    #endregion

    #region MAIN
    public void DisplayMarker(bool enable)
    {
        // Selection reference of target area
        accessors = GameObject.FindGameObjectsWithTag(targetForUse);

        foreach (GameObject accessor in accessors)
        {
            if (enable) SetMarkerSelection(accessor);
            else DisableMarkerSelection(accessor);
        }
    }

    public void ToolMarkerPossible(bool enable)
    {
        GameObject tool = GameObject.FindGameObjectWithTag(TutorialGame_Script.thisScript.get_GameInfo[(int)GameTagPlacement.DW_Tool].props_tag_name);
        tool.transform.GetComponent<Renderer>().material.color = enable ? Color.green : Color.red;
    }
    #endregion
}
