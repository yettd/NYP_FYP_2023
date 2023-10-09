using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DW_CottonGauze : MonoBehaviour
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
        if (moveObject != null)
        {
            // Enable for moving
            moveObject.Drag();

            // Find the area for the use of tool
            RaycastHit detect;
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out detect, Mathf.Infinity))
            {
                if (detect.collider.gameObject.GetComponent<DW_CottonGauzePlacement>() != null && !moveObject.get_isMove)
                {
                    detect.collider.gameObject.GetComponent<DW_CottonGauzePlacement>().ApplyProduct();
                    Destroy(gameObject);
                }
                else if (!moveObject.get_isMove)
                {
                    Destroy(gameObject);
                }
            }
        }
    }

    #region SETUP
    public void Activate()
    {
        // Set marker
        marker = new DW_ToolMarker("");
        moveObject = new DW_MoveTools();
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
                if (enable) accessor.AddComponent<DW_CottonGauzePlacement>();
                else Destroy(accessor.GetComponent<DW_CottonGauzePlacement>());
            }
        }
    }
    #endregion
}
