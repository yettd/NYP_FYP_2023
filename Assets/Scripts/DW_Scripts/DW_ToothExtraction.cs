using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DW_ToothExtraction : MonoBehaviour
{
    private const string subTargetTag = "DamagedTooth";
    private const string targetTag = "TeethSection";

    #region SETUP
    private void SetDamagedTooth()
    {
        GameObject[] teeths = GameObject.FindGameObjectsWithTag(targetTag);
        foreach (GameObject teeth in teeths) teeth.transform.GetChild(Random.Range(0, teeth.transform.childCount)).tag = subTargetTag;
    }
    #endregion

    #region MAIN
    public void Begin()
    {
        // Set random damaged tooth
        SetDamagedTooth();
    }
    #endregion
}
