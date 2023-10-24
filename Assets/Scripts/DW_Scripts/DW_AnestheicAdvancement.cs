using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DW_AnestheicAdvancement : MonoBehaviour
{
    private GameObject tool;
    private bool isPerforming = false;

    #region MAIN
    public void PerformTool()
    {
        if (!isPerforming)
        {
            isPerforming = true;
        }
    }
    #endregion
}
