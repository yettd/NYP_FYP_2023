using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DW_AnestheicPlacement : MonoBehaviour
{
    private bool isApply = false;
    private float anestheicDose = 0;
    private float anestheic_maxDose = 0;

    #region SETUP
    private void GumSectionEffect()
    {
        transform.GetComponent<Renderer>().material = Resources.Load<Material>("TutorialAssets/ApplyTarget");
        transform.GetComponent<Renderer>().material.color = new Color(anestheicDose, anestheicDose, anestheicDose, 0.5f);
        StartCoroutine(ProductDoseLogical());
    }

    private IEnumerator ProductDoseLogical()
    {
        anestheicDose += 0.01f;
        yield return new WaitForSeconds(0.1f);

        if (anestheicDose < anestheic_maxDose) GumSectionEffect();
        else gameObject.tag = "Untagged";
    }

    private void SetAnestheicDose(float amount)
    {
        anestheic_maxDose += amount;
    }
    #endregion

    #region MAIN
    public void ApplyProduct()
    {
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
        isApply = false;
    }

    public bool IsAnestheicDosed()
    {
        return anestheicDose != 0;
    }
    #endregion
}
