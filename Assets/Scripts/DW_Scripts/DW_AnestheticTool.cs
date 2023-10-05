using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DW_AnestheticTool : MonoBehaviour
{
    private DW_ElementCancelation element;
    private const string targetForUse = "GumSection";
    private DW_MoveTools moveable;

    private void OnDestroy()
    {
        DeActivate();
    }

    private void OnMouseDrag()
    {
        if (moveable != null) moveable.Drag();
    }

    #region SETUP
    public void Activate()
    {
        // Move
        moveable = new DW_MoveTools();

        // Cancel all active component
        element = new DW_ElementCancelation();
        element.Activate();

        // Enable used of tool
        GrantAccessToUseAnestheicTool(true);
    }

    private void DeActivate()
    {
        // Cancel all active component
        element.DeActivate();

        // Disable used of tool
        GrantAccessToUseAnestheicTool(false);
    }

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

    #region COMPONENT
    private void TargetSelectionArea()
    {
        // Enable used of tool
        GrantAccessToUseAnestheicTool(true);
    }

    private void GrantAccessToUseAnestheicTool(bool enable)
    {
        // Selection reference of target area
        GameObject[] accessors = GameObject.FindGameObjectsWithTag(targetForUse);

        foreach (GameObject accessor in accessors)
        {
            if (enable) SetMarkerSelection(accessor.transform.GetChild(0).gameObject);
            else DisableMarkerSelection(accessor.transform.GetChild(0).gameObject);
        }
    }
    #endregion
}
