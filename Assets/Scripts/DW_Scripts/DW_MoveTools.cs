using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DW_MoveTools
{
    private bool isMove;

    #region SETUP
    private void MoveTargetToPointer()
    {
        // Get plotPoint mapped with camera to use it on mouse pointer
        Vector3 plotPoint = GetCameraTarget().GetComponent<Camera>().ScreenToWorldPoint(Input.mousePosition);
        float tooldepth = GetCameraTarget().GetComponent<DW_ViewSwitcher>().GetToolDepthMapToCam();

        // Find targeted tool and move along with the mouse pointer
        if (GetTarget()) GetTarget().transform.position = new Vector3(plotPoint.x, plotPoint.y, tooldepth);
    }

    private bool IsMouseReleasedActive()
    {
        // Only worked in editor mode: Check for mouse click
        return Application.isEditor && Input.GetMouseButton(1);
    }

    private bool IsTouchReleasedActive()
    {
        // Only worked when its not worked in editor mode: Check touch if the user have lift off the touch screen
        return !Application.isEditor && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended;
    }

    private bool IsTouchMoveActive()
    {
        // Only worked when its not worked in editor mode: Check touch if the user is still moving
        return !Application.isEditor && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved;
    }

    private GameObject GetTarget()
    {
        // Find targeted object with the selected tag
        return GameObject.FindGameObjectWithTag(TutorialGame_Script.thisScript.get_GameInfo[(int)GameTagPlacement.DW_Tool].props_tag_name);
    }

    private GameObject GetCameraTarget()
    {
        // Find targeted object with the selected tag
        return GameObject.FindGameObjectWithTag(TutorialGame_Script.thisScript.get_GameInfo[(int)GameTagPlacement.DW_Camera].props_tag_name);
    }
    #endregion

    #region COMPONENT
    private bool DragUntilNotMoving()
    {
        // Find out move is possible
        if (isMove)
        {
            // Constant update of gameobject to mouse pointer
            MoveTargetToPointer();

            // Release it when condition are met to the user desire
            if (IsMouseReleasedActive() || IsTouchReleasedActive()) Release();
        }

        // Check back on move status
        return isMove;
    }

    private bool DragUntilObjectTrigger()
    {
        // Constant check to the user desire
        isMove = (IsMouseReleasedActive() || IsTouchMoveActive());

        // Find out move is possible
        if (isMove)
        {
            // Constant update of gameobject to mouse pointer
            MoveTargetToPointer();
        }

        // Check back on move status
        return isMove;
    }

    private void Release()
    {
        // Make move not possible
        isMove = false;
    }
    #endregion

    #region MAIN
    public void StartMove()
    {
        // Begin to move the object
        isMove = true;
    }

    public bool GetCurrentDragStyle()
    {
        // Identify drag style and determine the action move
        return DragUntilObjectTrigger();
    }
    #endregion
}
