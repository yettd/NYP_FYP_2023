using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DW_CottonGauzeAdvancement : MonoBehaviour
{
    private bool isPerforming = false;
    private GameObject tool;

    void Update()
    {
        if (!isPerforming && TutorialGame_Script.thisScript.get_onGoingFeedback != InterfaceFeedBack.PerformingUsedTool)
            isPerforming = true;
    }

    #region SETUP
    private void GetToolForUse()
    {
        GameObject toolUsed = GameObject.FindGameObjectWithTag(TutorialGame_Script.thisScript.get_GameInfo[(int)GameTagPlacement.DW_Tool].props_tag_name);
        tool = Instantiate(Resources.Load<GameObject>("TutorialAssets/Tools/Original/" + toolUsed.name));

        tool.transform.position = toolUsed.transform.position;
        Destroy(tool, 1);
    }
    #endregion

    #region MAIN
    public void PerformTool(float current, float percentageOfCompletion)
    {
        // Build in feedback when performing task
        if (isPerforming)
        {
            GetComponent<DW_CottonGauzePlacement>().GetClearingToWork();
            if (!tool) GetToolForUse();
            TutorialGame_Script.thisScript.AdvancementContent("Finishing product", Mathf.Clamp(100 / percentageOfCompletion * current, 0, 100));
        }
    }
    #endregion
}
