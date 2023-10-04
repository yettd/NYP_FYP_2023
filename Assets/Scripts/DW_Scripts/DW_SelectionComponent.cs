using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DW_SelectionComponent : MonoBehaviour
{
    private const float fadeDelayTimer = 0.05f;
    private const float increaseOfFadeTimer = 0.1f;
    private const float maxfadeValue = 1;

    private bool isSelected = false;
    private float fadeValue = 0;

    #region SETUP
    private void ChangeMaterialState(Material material)
    {
        Material changeM = transform.GetComponent<Renderer>().material;
        if (changeM != material) transform.GetComponent<Renderer>().material = material;
    }

    private void StartSectionSelect()
    {
        ChangeMaterialState(Resources.Load<Material>("TutorialAssets/SelectedTarget"));
        transform.GetComponent<Renderer>().material.color = new Color(fadeValue, 1, fadeValue);
        StartCoroutine(BeginSectionFade());
    }
    #endregion

    #region MAIN
    public void Select()
    {
        isSelected = true;
        StartSectionSelect();
    }

    public void DeSelect()
    {
        transform.GetComponent<Renderer>().material.color = Color.white;
        isSelected = false;
    }
    #endregion

    #region COMPONENT
    private IEnumerator BeginSectionFade()
    {
        if (fadeValue >= maxfadeValue) fadeValue = 0;
        else fadeValue += increaseOfFadeTimer;

        yield return new WaitForSeconds(fadeDelayTimer);
        if (isSelected) StartSectionSelect();
    }
    #endregion
}
