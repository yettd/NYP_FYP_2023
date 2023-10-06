using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DW_AnestheicPlacement : MonoBehaviour
{
    private bool isApply = false;

    #region SETUP
    private void PlacementEffect()
    {
        transform.GetComponent<Renderer>().material = Resources.Load<Material>("TutorialAssets/ApplyTarget");
        transform.GetComponent<Renderer>().material.color = new Color(0, 0, 0, 0.5f);
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
