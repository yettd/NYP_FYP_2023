using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DW_CottonGauzeAdvancement : MonoBehaviour
{
    private bool isPerforming = false;

    void Update()
    {
        if (!isPerforming && TutorialGame_Script.thisScript.get_onGoingFeedback != InterfaceFeedBack.PerformingUsedTool)
            isPerforming = true;
    }

    #region SETUP
    #endregion

    #region MAIN
    public void PerformTool(float current, float percentageOfCompletion)
    {
        // Build in feedback when performing task
        if (isPerforming)
        {
            GetComponent<DW_CottonGauzePlacement>().GetClearingToWork();
            TutorialGame_Script.thisScript.AdvancementContent("Finishing product", Mathf.Clamp(100 / percentageOfCompletion * current, 0, 100));
        }
    }
    #endregion
}
