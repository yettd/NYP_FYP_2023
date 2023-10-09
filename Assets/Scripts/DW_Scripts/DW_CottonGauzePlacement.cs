using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DW_CottonGauzePlacement : MonoBehaviour
{
    private bool isApply = false;

    #region SETUP
    private void PlacementEffect()
    {
        
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
