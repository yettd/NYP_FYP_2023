using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DW_ForcepsTool : MonoBehaviour
{
    private DW_ElementCancelation element;
    private DW_ToolMarker marker;
    private const string targetForUse = "DamagedTooth";
    private DW_MoveTools moveObject;

    void OnDestroy()
    {
        DeActivate();
    }

    void Update()
    {
        // Enable of moving
        if (moveObject != null) moveObject.Drag();

        // Find the area for the use of tool
        RaycastHit detect;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out detect, Mathf.Infinity))
        {
            if (detect.collider.gameObject.GetComponent<DW_ForcepsPlacement>() != null)
            {
                detect.collider.gameObject.GetComponent<DW_ForcepsPlacement>().ApplyProduct();
                DeActivate();
            }
        }
    }

    #region SETUP
    public void Activate()
    {
        // Set marker
        marker = new DW_ToolMarker(targetForUse);
        moveObject = new DW_MoveTools();
        element = new DW_ElementCancelation();

        // Enable used of tool
        GrantAccessToUseForcespTool(true);
    }

    private void DeActivate()
    {
        // Cancel all active component
        element.DeActivate();

        // Disable used of tool
        GrantAccessToUseForcespTool(false);
    }
    #endregion

    #region COMPONENT
    private void GrantAccessToUseForcespTool(bool enable)
    {
        if (enable)
        {
            // Move the object
            if (moveObject != null) moveObject.StartMove();

            // Cancel all active component
            if (element != null) element.Activate();
        }

        // Define marker usage
        if (marker != null)
        {
            // Selection marker
            marker.DisplayMarker(enable);

            // Selection object
            foreach (GameObject accessor in marker.getAccessors)
            {
                if (enable) accessor.AddComponent<DW_ForcepsPlacement>();
                else Destroy(accessor.GetComponent<DW_ForcepsPlacement>());
            }
        }
    }
    #endregion
}
