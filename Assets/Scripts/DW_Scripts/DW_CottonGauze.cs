using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DW_CottonGauze : MonoBehaviour
{
    private DW_ElementCancelation element;
    private DW_ToolMarker marker;
    private DW_MoveTools moveObject;
    private DW_ToolAccessory accessory;

    private bool isClearing = false;

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
            if (moveObject.GetCurrentDragStyle())
            {
                // Detect any possible area which are targeted as placement
                if (Physics.Raycast(accessory.GetToolPosition(), accessory.GetToolPointedDirection(), out detect, Mathf.Infinity))
                {
                    if (detect.collider.gameObject.GetComponent<DW_CottonGauzePlacement>() != null)
                    {
                        // Perform any use of product available in the placement itself
                        if (!detect.collider.gameObject.GetComponent<DW_CottonGauzePlacement>().IsCleaningDone())
                        {
                            detect.collider.gameObject.GetComponent<DW_CottonGauzePlacement>().ApplyProduct();
                            GetComponent<Renderer>().enabled = false;
                            isClearing = true;
                        }

                        // Done
                        else
                        {
                            TutorialGame_Script.thisScript.getTasking.GetStepClearance(name);
                            marker.ToolMarkerPossible(false);
                        }
                    }
                }

                // Detect any possible area for interaction
                if (Physics.Raycast(accessory.GetToolPosition(), accessory.GetToolPointedDirection(), out detect, Mathf.Infinity))
                    marker.ToolMarkerPossible(detect.collider.gameObject.GetComponent<DW_CottonGauzePlacement>() != null && !detect.collider.gameObject.GetComponent<DW_CottonGauzePlacement>().IsCleaningDone());
                else
                {
                    marker.ToolMarkerPossible(false);
                    GetComponent<Renderer>().enabled = true;
                    isClearing = false;
                }
            }

            // Pin down the possible area of interaction
            accessory.DisplayToolPositionOrgin();
            accessory.DisplayToolPositionWithOffset();

            // Refresh feedback
            if (!isClearing) TutorialGame_Script.thisScript.RefreshFeedbackContent(InterfaceFeedBack.CurrentlyInUsed);
        }
    }

    #region SETUP
    public void Activate()
    {
        // Set marker
        marker = new DW_ToolMarker(TutorialGame_Script.thisScript.get_GameInfo[(int)GameTagPlacement.ToothPlacement].props_tag_name);

        // Set the object to move
        moveObject = new DW_MoveTools();

        // Set additional tool value for uses
        accessory = GetComponent<DW_ToolAccessory>();

        // Cancel out all unwanted script
        element = new DW_ElementCancelation();

        // Enable used of tool
        GrantAccessToUseCottonGauze(true);
    }

    private void DeActivate()
    {
        // Cancel all active component
        element.DeActivate();

        // Disable used of tool
        GrantAccessToUseCottonGauze(false);
    }
    #endregion

    #region COMPONENT
    private void GrantAccessToUseCottonGauze(bool enable)
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
                    if (accessor.GetComponent<DW_CottonGauzePlacement>() == null) accessor.AddComponent<DW_CottonGauzePlacement>();
                }
            }
        }
    }
    #endregion
}
