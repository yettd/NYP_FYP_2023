using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DW_CottonGauzePlacement : MonoBehaviour
{
    private bool isApply = false;
    private bool isSetToClean = false;
    private bool cleaningDone = false;

    private float clearingRateSpeed = 0;
    private float percentageOfClearing = 0;
    private float currentClearing = 0;

    #region SETUP
    private void CheckForClearingArea()
    {
        // Find out if there are still need to be cleaned and perform the task again
        if (currentClearing < percentageOfClearing) isApply = false;

        // Cleaning Done
        else SetCleaningDone();
    }

    private void SetCleaningDone()
    {
        // Mark the tooth apply to the cotton gauze
        tag = TutorialGame_Script.thisScript.get_GameInfo[(int)GameTagPlacement.ToothExtracted].props_tag_name;

        // Mark the component that cleaning is done
        cleaningDone = true;

        // Remove used tool
        GameObject tool = GameObject.FindGameObjectWithTag(TutorialGame_Script.thisScript.get_GameInfo[(int)GameTagPlacement.DW_Tool].props_tag_name);
        if (tool) Destroy(tool);
    }

    private IEnumerator ClearingParticle()
    {
        // Wait for a few seconds to take clearing particle
        yield return new WaitForSeconds(0.5f);

        // Update the clearing progress
        currentClearing += clearingRateSpeed;

        // Check of the clearing condition
        CheckForClearingArea();
    }

    public void GetClearingToWork()
    {
        // Set properties to clear unwanter particle
        if (!isSetToClean)
        {
            percentageOfClearing = Random.Range(50, 100);
            clearingRateSpeed = Random.Range(1, 20);
            isSetToClean = true;
        }

        // Perform the cleaning of extracted tooth
        StartCoroutine(ClearingParticle());

        // Done
        isApply = true;
    }
    #endregion

    #region MAIN
    public void ApplyProduct()
    {
        // Only called once when the placement is successful
        if (!isApply)
        {
            // Perform tool 
            if (GetComponent<DW_CottonGauzeAdvancement>() != null) gameObject.GetComponent<DW_CottonGauzeAdvancement>().PerformTool(currentClearing + clearingRateSpeed, percentageOfClearing);
            else gameObject.AddComponent<DW_CottonGauzeAdvancement>().PerformTool(currentClearing + clearingRateSpeed, percentageOfClearing);
        }
    }
    #endregion

    #region COMPONENT
    public bool IsCleaningDone()
    {
        // Find out if the cleaning is completely clean up
        return cleaningDone;
    }
    #endregion
}
