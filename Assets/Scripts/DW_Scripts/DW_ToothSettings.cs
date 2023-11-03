using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DW_ToothSettings : MonoBehaviour
{
    public int toothIndex;

    private void OnMouseDown()
    {
        SelectTooth();
    }

    public void SelectTooth()
    {
        teethMan.tm.CallClickOn(TutorialGame_Script.thisScript.get_GameInfo[(int)GameTagPlacement.DamagedTooth].props[toothIndex].name);
    }
}
