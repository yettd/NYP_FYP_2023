using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DW_CottonGauzePlacement : MonoBehaviour
{
    private bool isApply = false;
    private bool cleaningDone = false;

    private const float clearingRateSpeed = 20;
    private float percentageOfClearing = 0;
    private int effectiveRatePoint = 0;

    #region SETUP
    private void CheckForClearingArea()
    {
        if (percentageOfClearing < 100) isApply = false;
        else cleaningDone = true;
    }

    private IEnumerator ClearingParticle()
    {
        yield return new WaitForSeconds(0.5f);
        percentageOfClearing += clearingRateSpeed;
        CheckForClearingArea();
    }
    #endregion

    #region MAIN
    public void ApplyProduct()
    {
        if (!isApply)
        {
            StartCoroutine(ClearingParticle());
            isApply = true;
        }
    }
    #endregion

    #region COMPONENT
    public void AddRatePoint(int amount)
    {
        effectiveRatePoint += amount;
    }

    public void ResetRatePoint()
    {
        effectiveRatePoint = 0;
    }

    public bool IsCleaningDone()
    {
        return cleaningDone;
    }
    #endregion
}
