using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DW_AnestheticTool : MonoBehaviour
{
    private DW_ElementCancelation element;
    private DW_ToolMarker marker;
    private DW_MoveTools moveObject;
    private DW_AdvanceMove advance_moveObject;

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
            if (!moveObject.Drag())
            {
                // Detect any possible area which are targeted as placement
                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out detect, Mathf.Infinity))
                {
                    if (detect.collider.gameObject.GetComponent<DW_AnestheicPlacement>() != null)
                    {
                        // Perform any use of product available in the placement itself
                        detect.collider.gameObject.GetComponent<DW_AnestheicPlacement>().ApplyProduct();

                        // Done
                        Destroy(gameObject);
                    }
                }

                // Reset the position
                Destroy(gameObject);
            }

            else
            {
                // Detect any possible area for interaction
                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out detect, Mathf.Infinity))
                {
                    // Anestheic interact found!
                    bool checkForPlacement = detect.collider.gameObject.GetComponent<DW_AnestheicPlacement>() != null;

                    // Display selection marker reader
                    marker.ToolMarkerPossible(checkForPlacement);

                    // Advance Script: Move tool and clamp tool depth to the selected marker
                    //advance_moveObject.SnapTargetToPoint(detect.collider.gameObject.transform, checkForPlacement);
                }
                else
                {
                    // Display selection marker reader
                    marker.ToolMarkerPossible(false);

                    // Advance Script: Move tool and clamp tool depth to the selected marker
                    //advance_moveObject.ResetPoint();
                }
            }
        }
    }

    #region SETUP
    public void Activate()
    {
        // Use of advance scripts
        if (GetComponent<DW_AdvanceAnestheicTool>() == null)
            gameObject.AddComponent<DW_AdvanceAnestheicTool>().UseAdvanceScript();

        // Use of advance move
        advance_moveObject = new DW_AdvanceMove();

        // Set marker
        marker = new DW_ToolMarker(TutorialGame_Script.thisScript.get_GameInfo[(int)GameTagPlacement.GumSection].props_tag_name);
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
