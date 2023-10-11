using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DW_MoveTools
{
    private DW_AdvanceMove advanceScript;

    private const string referenceName = "Camera2"; 
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
        return !Application.isEditor && Input.touchCount != 0 && Input.GetTouch(0).phase == TouchPhase.Ended;
    }

    private GameObject GetTarget()
    {
        // Find targeted object with the selected tag
        return GameObject.FindGameObjectWithTag(TutorialGame_Script.thisScript.get_dwTool);
    }

    private GameObject GetCameraTarget()
    {
        // Find targeted object with the selected tag
        return GameObject.FindGameObjectWithTag(referenceName);
    }
    #endregion

    #region COMPONENT
    public bool Drag()
    {
        if (isMove)
        {
            // Constant update of gameobject to mouse pointer
            MoveTargetToPointer();

            // Advance Move: Script
            if (GameObject.FindGameObjectWithTag(referenceName).GetComponent<DW_ViewSwitcher>().GetMainCamViewport())
            {
                advanceScript.AdvanceMoveFeatures();
                advanceScript.Drag(GetTarget().transform.position);
            }

            // Release it when condition are met to the user desire
            if (IsMouseReleasedActive() || IsTouchReleasedActive()) Release();
        }

        return isMove;
    }

    private void Release()
    {
        isMove = false;
    }
    #endregion

    #region MAIN
    public void StartMove()
    {
        float mainCam = GetCameraTarget().GetComponent<DW_ViewSwitcher>().GetToolDepthMapToCam();
        const int delay = 2;

        advanceScript = new DW_AdvanceMove(mainCam, mainCam + 3, delay);
        isMove = true;
    }
    #endregion
}
