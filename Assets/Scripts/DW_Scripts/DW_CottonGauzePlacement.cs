using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DW_CottonGauzePlacement : MonoBehaviour
{
    private bool isApply = false;
    private bool cleaningDone = false;

    private const float clearingRateSpeed = 20;
    private float percentageOfClearing = 0;

    #region SETUP
    private void CheckForClearingArea()
    {
        // Find out if there are still need to be cleaned and perform the task again
        if (percentageOfClearing < 100) isApply = false;

        // Cleaning Done
        else SetCleaningDone();
    }

    private void SetCleaningDone()
    {
        // Mark the tooth apply to the cotton gauze
        tag = TutorialGame_Script.thisScript.get_GameInfo[(int)GameTagPlacement.ToothExtracted].props_tag_name;

        // Mark the component that cleaning is done
        cleaningDone = true;
    }

    private IEnumerator ClearingParticle()
    {
        // Wait for a few seconds to take clearing particle
        yield return new WaitForSeconds(0.5f);

        // Update the clearing progress
        percentageOfClearing += clearingRateSpeed;

        // Check of the clearing condition
        CheckForClearingArea();
    }
    #endregion

    #region MAIN
    public void ApplyProduct()
    {
        // Only called once when the placement is successful
        if (!isApply)
        {
            // Perform the cleaning of extracted tooth
            StartCoroutine(ClearingParticle());

            // Done
            isApply = true;
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
