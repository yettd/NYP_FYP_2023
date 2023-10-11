using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DW_AdvanceMove
{
    private bool isIdle;
    private float minDepth;
    private float maxDepth;

    private float idleTimeOut;
    private float NextTimingOfAdvanceMove = 0;
    private Vector3 currentPosition;

    public DW_AdvanceMove()
    {
        isIdle = false;
        minDepth = 0;
        maxDepth = 0;
        idleTimeOut = 0;
    }

    public DW_AdvanceMove(float min, float max, float timeOut)
    {
        isIdle = false;
        minDepth = min;
        maxDepth = max;
        idleTimeOut = timeOut;
    }

    #region SETUP
    private void MoveTargetToDepth()
    {
        GameObject tool = GameObject.FindGameObjectWithTag(TutorialGame_Script.thisScript.get_dwTool);
        if (tool.transform.position.z < maxDepth) tool.transform.Translate(Vector3.left * 1 * Time.deltaTime);
    }
    #endregion

    #region MAIN
    public void Drag(Vector3 position)
    {
        if ((int)position.x != (int)currentPosition.x && (int)position.y != (int)currentPosition.y) ResetDepth();
        if (isIdle) MoveTargetToDepth();
    }

    public void AdvanceMoveFeatures()
    {
        isIdle = (Time.time >= NextTimingOfAdvanceMove);
    }
    #endregion

    #region COMPONENT
    private void ResetDepth()
    {
        GameObject tool = GameObject.FindGameObjectWithTag(TutorialGame_Script.thisScript.get_dwTool);
        tool.transform.position = new Vector3(tool.transform.position.x, tool.transform.position.y, minDepth);
        currentPosition = tool.transform.position;

        NextTimingOfAdvanceMove = Time.time + idleTimeOut;
    }
    #endregion
}
