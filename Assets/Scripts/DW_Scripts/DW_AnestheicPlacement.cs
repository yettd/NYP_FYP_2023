using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DW_AnestheicPlacement : MonoBehaviour
{
    private bool isApply = false;

    private const float DoseRatePoint = 0.01f;
    private float anestheicDose = 0;
    private float anestheic_maxDose = 0;

    #region SETUP
    private void GumSectionEffect()
    {
        // Start cycle in the process in dosing the anestheic side effect
        StartCoroutine(ProductDoseLogical());
    }

    private IEnumerator ProductDoseLogical()
    {
        // Modify of the dose effect that can changes the appearance
        anestheicDose += DoseRatePoint;

        // Pause the effect a while then continue
        yield return new WaitForSeconds(0.1f);

        // Find out that the dose have fully taken effect through the component
        if (anestheicDose < anestheic_maxDose) GumSectionEffect();
    }

    private void SetAnestheicDose(float amount)
    {
        // Set the amount of anestheic to be used for processing
        anestheic_maxDose += amount;

        // Perform tool
        if (GetComponent<DW_AnestheicAdvancement>() == null) gameObject.AddComponent<DW_AnestheicAdvancement>().PerformTool();
        else GetComponent<DW_AnestheicAdvancement>().PerformTool();
    }
    #endregion

    #region MAIN
    public void ApplyProduct()
    {
        // Only called once when this is apply
        if (!isApply)
        {
            // Set amount of dose to it
            SetAnestheicDose(0.8f);
            StartCoroutine(ProductDoseLogical());

            // Done
            isApply = true;
        }
    }
    #endregion

    #region COMPONENT
    public void ResetProdutUse()
    {
        // Reset the use of placement and start over
        isApply = false;
    }

    public bool IsAnestheicDosed()
    {
        // Finding the anestheic have been dosed and able to carry on to the other task
        return anestheicDose != 0;
    }
    #endregion
}
