using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DW_MoveTools
{
    private GameObject target;
    private Vector3 offset;

    private Camera cam;

    #region COMPONENT
    private void BeginDrag()
    {
        
    }

    private void EndDrag()
    {
        target = null;
    }
    #endregion

    #region MAIN
    public void SelectTarget(GameObject tool)
    {
        target = tool;
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        BeginDrag();
    }
    #endregion
}
