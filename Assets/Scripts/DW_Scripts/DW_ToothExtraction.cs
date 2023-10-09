using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DW_ToothExtraction : MonoBehaviour
{
    #region SETUP
    private void SetDamagedTooth()
    {
        GameObject[] teeths = GameObject.FindGameObjectsWithTag(TutorialGame_Script.thisScript.get_teethTag);
        foreach (GameObject teeth in teeths) teeth.transform.GetChild(Random.Range(0, teeth.transform.childCount)).tag = TutorialGame_Script.thisScript.get_damagedTooth;
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
