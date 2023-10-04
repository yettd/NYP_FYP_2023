using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DW_AnestheticTool : MonoBehaviour
{
    private DW_MoveTools moveableTool;
    private const string targetForUse = "GumSection";

    private void OnDestroy()
    {
        DeActivate();
    }

    private void Update()
    {
        if (moveableTool != null) moveableTool.SelectTarget(gameObject);
    }

    #region SETUP
    public void Activate()
    {
        moveableTool = new DW_MoveTools();
        TargetSelectionArea();
    }

    private void DeActivate()
    {
        GrantAccessToCreateUsePoints(false);
    }
    #endregion

    #region COMPONENT
    private void TargetSelectionArea()
    {
        // Create marker to targeted point
        GrantAccessToCreateUsePoints(true);
    }

    private void GrantAccessToCreateUsePoints(bool enable)
    {
        GameObject[] accessors = GameObject.FindGameObjectsWithTag(targetForUse);

        foreach (GameObject accessor in accessors)
        {
            if (enable)
            {
                if (accessor.GetComponent<DW_SelectionComponent>() == null) accessor.AddComponent<DW_SelectionComponent>().Select();
                else accessor.GetComponent<DW_SelectionComponent>().Select();
            }
            else if (!enable && accessor.GetComponent<DW_SelectionComponent>() != null) accessor.GetComponent<DW_SelectionComponent>().DeSelect();
        }
    }
    #endregion
}
