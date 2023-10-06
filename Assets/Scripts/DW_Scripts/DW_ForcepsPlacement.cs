using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DW_ForcepsPlacement : MonoBehaviour
{
    private bool isApply = false;

    #region SETUP
    private void PlacementEffect()
    {
        Transform tool = GameObject.FindGameObjectWithTag("DW_Tool").transform;
        if (tool.GetComponentInChildren<DW_AnestheicPlacement>() == null) transform.SetParent(tool);
    }
    #endregion

    #region MAIN
    public void ApplyProduct()
    {
        if (!isApply)
        {
            PlacementEffect();
            isApply = true;
        }
    }
    #endregion
}
