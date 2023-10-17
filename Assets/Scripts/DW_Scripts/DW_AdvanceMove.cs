using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DW_AdvanceMove
{
    #region SETUP
    private GameObject GetTargetTool()
    {
        return GameObject.FindGameObjectWithTag(TutorialGame_Script.thisScript.get_GameInfo[(int)GameTagPlacement.DW_Tool].props_tag_name);
    }
    #endregion

    #region MAIN
    public void SnapTargetToPoint(Transform point, bool isSnap)
    {
        if (GameObject.FindGameObjectWithTag(TutorialGame_Script.thisScript.get_GameInfo[(int)GameTagPlacement.DW_Camera].props_tag_name).GetComponent<DW_ViewSwitcher>().GetMainCamViewport())
        {
            if (isSnap) GetTargetTool().transform.position = new Vector3(GetTargetTool().transform.position.x, GetTargetTool().transform.position.y, point.position.z);
            else ResetPoint();
        }
    }
    #endregion

    #region COMPONENT
    public void ResetPoint()
    {
        GameObject cam = GameObject.FindGameObjectWithTag(TutorialGame_Script.thisScript.get_GameInfo[(int)GameTagPlacement.DW_Camera].props_tag_name);
        GetTargetTool().transform.position = new Vector3(GetTargetTool().transform.position.x, GetTargetTool().transform.position.y, cam.GetComponent<DW_ViewSwitcher>().GetToolDepthMapToCam());
    }
    #endregion
}
