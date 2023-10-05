using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DW_MoveTools
{
    private GameObject target;
    private Camera cam;

    #region COMPONENT
    public void Drag()
    {
        Vector3 pos = cam.ScreenToWorldPoint(Input.mousePosition);
        target.transform.position = new Vector3(pos.x, pos.y, target.transform.position.z);
        Ray ray = cam.ScreenPointToRay(cam.WorldToScreenPoint(pos));
        if (Physics.Raycast(target.transform.position, target.transform.forward, out RaycastHit hit))
        {
            Debug.Log(hit.collider.gameObject.name);
            //switch (minigameTaskListController.Instance.procedure)
            //{
            //    case Procedure.Scaling:
            //        hit.collider.GetComponent<TeethDirtClean>().Clean(hit);
            //        break;
            //}
        }

    }
    #endregion

    #region MAIN
    public void SelectTarget(GameObject tool)
    {
        target = tool;
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }
    #endregion
}
