using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DW_CottonGauzeAdvancement : MonoBehaviour
{
    #region SETUP
    #endregion

    #region MAIN
    public void PerformTool(float current, float percentageOfCompletion)
    {
        // Build in feedback when performing task
        TutorialGame_Script.thisScript.AdvancementContent("Finishing product", 100 / percentageOfCompletion * current);
    }
    #endregion

    #region COMPONENT
    #endregion
}
