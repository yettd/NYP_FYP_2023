using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DW_ToolAccessory : MonoBehaviour
{
    [SerializeField] private Vector3 offset;
    [SerializeField] private Vector3 direction;

    #region MAIN
    public Vector3 GetToolPosition()
    {
        // Get tool new position
        return transform.position + offset;
    }

    public Vector3 GetToolPointedDirection()
    {
        // Get tool point to the assign direction
        return transform.TransformDirection(direction);
    }
    #endregion

    #region DEBUG
    public void DisplayToolPositionWithOffset()
    {
        // Define the tool position with the offset
        Debug.DrawRay(transform.position + offset, transform.TransformDirection(direction), Color.blue);
    }

    public void DisplayToolPositionOrgin()
    {
        // Define the tool original position
        Debug.DrawRay(transform.position, transform.TransformDirection(direction), Color.red);
    }
    #endregion
}
