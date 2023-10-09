using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DW_MoveTools
{
    private bool isMove;
    public bool get_isMove { get { return isMove; } }

    #region SETUP
    private void MoveTargetToPointer()
    {
        Vector3 camera = GameObject.FindGameObjectWithTag("Camera2").GetComponent<Camera>().transform.position;
        Vector3 plotPoint = GameObject.FindGameObjectWithTag("Camera2").GetComponent<Camera>().ScreenToWorldPoint(Input.mousePosition);
        if (GetTarget()) GetTarget().transform.position = new Vector3(plotPoint.x, plotPoint.y, -0.5f);
    }

    private bool IsMouseReleasedActive()
    {
        return Input.GetKeyDown(KeyCode.A);
    }

    private bool IsTouchReleasedActive()
    {
        return Input.touchCount != 0 && Input.GetTouch(0).phase == TouchPhase.Ended;
    }

    private GameObject GetTarget()
    {
        return GameObject.FindGameObjectWithTag(TutorialGame_Script.thisScript.get_dwTool);
    }
    #endregion

    #region COMPONENT
    public void Drag()
    {
        if (isMove)
        {
            MoveTargetToPointer();

            if (Application.isEditor && IsMouseReleasedActive()) Release();
            else if (!Application.isEditor && IsTouchReleasedActive()) Release();
        }
    }

    private void Release()
    {
        isMove = false;
    }
    #endregion

    #region MAIN
    public void StartMove()
    {
        isMove = true;
    }
    #endregion
}
