using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DW_AnestheticTool : MonoBehaviour
{
    private DW_ElementCancelation element;
    private DW_ToolMarker marker;
    private DW_MoveTools moveObject;
    private DW_ToolAccessory accessory;

    void OnDestroy()
    {
        DeActivate();
    }

    void Update()
    {
        // Define accessible component
        if (moveObject != null)
        {
            // Find the area for the use of tool
            RaycastHit detect;

            // Move the object until its inactive
            if (!moveObject.GetCurrentDragStyle())
            {
                // Detect any possible area which are targeted as placement
                if (Physics.Raycast(accessory.GetToolPosition(), accessory.GetToolPointedDirection(), out detect, Mathf.Infinity))
                {
                    if (detect.collider.gameObject.GetComponent<DW_AnestheicPlacement>() != null && 
                        !detect.collider.gameObject.GetComponent<DW_AnestheicPlacement>().IsAnestheicDosed())
                    {
                        // Perform any use of product available in the placement itself
                        detect.collider.gameObject.GetComponent<DW_AnestheicPlacement>().ApplyProduct();

                        // Done
                        marker.DisplayMarker(false);
                        marker.ToolMarkerPossible(false);
                    }
                }
            }

            else
            {
                // Detect any possible area for interaction
                if (Physics.Raycast(accessory.GetToolPosition(), accessory.GetToolPointedDirection(), out detect, Mathf.Infinity))
                {
                    // Anestheic interact found!
                    bool checkForPlacement = detect.collider.gameObject.GetComponent<DW_AnestheicPlacement>() != null &&
                        !detect.collider.gameObject.GetComponent<DW_AnestheicPlacement>().IsAnestheicDosed();

                    // Display selection marker reader
                    marker.ToolMarkerPossible(checkForPlacement);
                }
                else
                {
                    // Display selection marker reader
                    marker.ToolMarkerPossible(false);
                }
            }

            // Pin down the possible area of interaction
            accessory.DisplayToolPositionOrgin();
            accessory.DisplayToolPositionWithOffset();
        }
    }

    #region SETUP
    public void Activate()
    {
        // Use of advance scripts: Anestheic Tool
        if (GetComponent<DW_AdvanceAnestheicTool>() == null)
            gameObject.AddComponent<DW_AdvanceAnestheicTool>().UseAdvanceScript();

        // Set marker
        marker = new DW_ToolMarker(TutorialGame_Script.thisScript.get_GameInfo[(int)GameTagPlacement.GumSection].props_tag_name);

        // Set the object to move
        moveObject = new DW_MoveTools();

        // Set additional tool value for uses
        accessory = GetComponent<DW_ToolAccessory>();

        // Cancel out all unwanted script
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
        // Init accessible tool and other
        if (enable)
        {
            // Cancel out all unwanted component that cause clash to each other
            if (element != null) element.Activate();
        }

        // Define marker usage
        if (marker != null)
        {
            // Selection marker
            marker.DisplayMarker(enable);

            // Make the object interactable for the use of tool and target destination
            if (enable)
            {
                foreach (GameObject accessor in marker.getAccessors)
                {
                    // This will allow product to be used multiple time as the component still active
                    if (accessor.GetComponent<DW_AnestheicPlacement>() == null) accessor.AddComponent<DW_AnestheicPlacement>();
                }
            }
        }
    }
    #endregion
}
