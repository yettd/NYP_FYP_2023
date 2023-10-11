using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DW_AnestheticTool : MonoBehaviour
{
    private DW_ElementCancelation element;
    private DW_ToolMarker marker;
    private DW_MoveTools moveObject;

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
                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.left), out detect, Mathf.Infinity))
                {
                    if (detect.collider.gameObject.GetComponent<DW_AnestheicPlacement>() != null)
                    {
                        // Perform any use of product available in the placement itself
                        detect.collider.gameObject.GetComponent<DW_AnestheicPlacement>().ApplyProduct();
                    }
                }

                // Done
                Destroy(gameObject);
            }

            else
            {
                // Detect any possible area for interaction
                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.left), out detect, Mathf.Infinity))
                    marker.ToolMarkerPossible(detect.collider.gameObject.GetComponent<DW_AnestheicPlacement>() != null);

                else
                    marker.ToolMarkerPossible(false);
            }
        }
    }

    #region SETUP
    public void Activate()
    {
        // Set marker
        marker = new DW_ToolMarker(TutorialGame_Script.thisScript.get_gumTag);
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
                    if (accessor.GetComponent<DW_AnestheicPlacement>() != null) accessor.GetComponent<DW_AnestheicPlacement>().ResetProdutUse();
                    else accessor.AddComponent<DW_AnestheicPlacement>();
                }
            }
        }
    }
    #endregion
}
