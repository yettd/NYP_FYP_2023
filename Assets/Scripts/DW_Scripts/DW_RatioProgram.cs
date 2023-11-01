using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DW_RatioProgram : MonoBehaviour
{
    public static DW_RatioProgram program;

    public GameObject aimPanel;
    private float distance;
    private float maxDistance;

    void Start()
    {
        program = this;    
    }

    #region SETUP
    private float GetRadius(float distance)
    {
        return distance * 0.5f;
    }

    private float GetPercentage()
    {
        float distanceTowardCenter = 100 / GetRadius(maxDistance) * distance;
        return Mathf.Clamp(distanceTowardCenter, 0, 100);
    }
    #endregion

    #region MAIN
    public void FindRatioData(float distance, float maxDistance)
    {
        this.distance = distance;
        this.maxDistance = maxDistance;

        Debug.Log("Distance: " + (maxDistance - distance));
    }
    #endregion

    #region COMPONENT
    private void UpdateCotentAimPanel()
    {
        if (aimPanel) aimPanel.GetComponent<TMP_Text>().text = "Ratio: " + GetPercentage().ToString("0.0") + " %";
        Debug.Log("Ratio: " + GetPercentage().ToString("0.0") + " %");
    }
    #endregion
}
