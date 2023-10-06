using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DW_AnestheticTool : MonoBehaviour
{
    private DW_ElementCancelation element;
    private DW_ToolMarker marker;
    private const string targetForUse = "GumSection";
    private DW_MoveTools moveObject;

    void OnDestroy()
    {
        DeActivate();
    }

    void Update()
    {
        if (moveObject != null)
        {
            // Enable for moving
            moveObject.Drag();

            // Find the area for the use of tool
            RaycastHit detect;
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out detect, Mathf.Infinity))
            {
                if (detect.collider.gameObject.GetComponent<DW_AnestheicPlacement>() != null && !moveObject.get_isMove)
                {
                    detect.collider.gameObject.GetComponent<DW_AnestheicPlacement>().ApplyProduct();
                    DeActivate();
                }
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
        GrantAccessToUseAnestheicTool(true);
    }

    private void DeActivate()
    {
        // Cancel all active component
        element.DeActivate();

        // Disable used of tool
        GrantAccessToUseAnestheicTool(false);
    }
    #endregion

    #region COMPONENT
    private void GrantAccessToUseAnestheicTool(bool enable)
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
                if (enable) accessor.AddComponent<DW_AnestheicPlacement>();
                else Destroy(accessor.GetComponent<DW_AnestheicPlacement>());
            }
        }
    }
    #endregion
}
