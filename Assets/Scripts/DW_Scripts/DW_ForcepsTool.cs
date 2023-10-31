using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DW_ForcepsTool : MonoBehaviour
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
                    if (detect.collider.gameObject.GetComponent<DW_ForcepsPlacement>() != null)
                    {
                        // Perform any use of product available in the placement itself
                        detect.collider.gameObject.GetComponent<DW_ForcepsPlacement>().ApplyProduct();

                        // Done
                        GetComponent<MeshRenderer>().enabled = false;
                        Destroy(GetComponent<DW_ForcepsTool>());
                    }
                }
            }

            else
            {
                // Detect any possible area for interaction
                if (Physics.Raycast(accessory.GetToolPosition(), accessory.GetToolPointedDirection(), out detect, Mathf.Infinity))
                {
                    // Pull out interact found!
                    bool checkForPlacement = detect.collider.gameObject.GetComponent<DW_ForcepsPlacement>() != null &&
                        !detect.collider.gameObject.GetComponent<DW_ForcepsPlacement>().IsExtractionComplete();

                    // Display selection marker reader
                    marker.ToolMarkerPossible(checkForPlacement);
                }
                else
                    marker.ToolMarkerPossible(false);
            }

            // Pin down the possible area of interaction
            accessory.DisplayToolPositionOrgin();
            accessory.DisplayToolPositionWithOffset();

            // Refresh feedback
            TutorialGame_Script.thisScript.RefreshFeedbackContent(InterfaceFeedBack.CurrentlyInUsed);
        }
    }

    #region SETUP
    public void Activate()
    {
        // Set marker
        marker = new DW_ToolMarker(TutorialGame_Script.thisScript.get_GameInfo[(int)GameTagPlacement.DamagedTooth].props_tag_name);

        // Set the object to move
        moveObject = new DW_MoveTools();

        // Set additional tool value for uses
        accessory = GetComponent<DW_ToolAccessory>();

        // Cancel out all unwanted script
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
        // Init accessible tool and other
        if (enable)
        {
            // Init the object to move
            if (moveObject != null) moveObject.StartMove();

            // Cancel out all unwanted component that cause clash to each other
            if (element != null) element.Activate();
        }

        // Define marker usage
        if (marker != null)
        {
            // Selection marker
            marker.DisplayMarker(enable);

            // GUI Status: Read it
            TutorialGame_Script.thisScript.UseFeedbackDisplay(InterfaceFeedBack.CurrentlyInUsed, enable, string.Empty);

            // Make the object interactable for the use of tool and target destination
            if (enable)
            {
                foreach (GameObject accessor in marker.getAccessors)
                {
                    // This will allow product to be used multiple time as the component still active
                    if (accessor.GetComponent<DW_ForcepsPlacement>() == null) accessor.AddComponent<DW_ForcepsPlacement>();
                }
            }
        }
    }
    #endregion
}
