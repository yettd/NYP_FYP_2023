using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DW_ElementCancelation
{
    #region SETUP
    public void Activate()
    {
        ApplyElementCancelThroughTooth();
        ApplyElementCancelThroughGum();
    }

    public void DeActivate()
    {
        ResetElementThroughElement();
    }
    #endregion

    #region COMPONENT
    private void ApplyElementCancelThroughTooth()
    {
        GameObject[] teeths = GameObject.FindGameObjectsWithTag(TutorialGame_Script.thisScript.get_GameInfo[(int)GameTagPlacement.TeethSection].props_tag_name);
        TeethDirtClean[] teethDirtClean_comp;
        openTooth[] openTooth_comp;

        foreach (GameObject teeth in teeths)
        {
            teethDirtClean_comp = teeth.transform.GetComponentsInChildren<TeethDirtClean>();
            openTooth_comp = teeth.transform.GetComponentsInChildren<openTooth>();

            foreach (TeethDirtClean component in teethDirtClean_comp) if (component.enabled) component.enabled = false;
            foreach (openTooth component in openTooth_comp) if (component.enabled) component.enabled = false;
        }
    }

    private void ApplyElementCancelThroughGum()
    {
        
    }

    private void ResetElementThroughElement()
    {
        GameObject[] teeths = GameObject.FindGameObjectsWithTag(TutorialGame_Script.thisScript.get_GameInfo[(int)GameTagPlacement.TeethSection].props_tag_name);
        TeethDirtClean[] teethDirtClean_comp;
        openTooth[] openTooth_comp;

        foreach (GameObject teeth in teeths)
        {
            teethDirtClean_comp = teeth.transform.GetComponentsInChildren<TeethDirtClean>();
            openTooth_comp = teeth.transform.GetComponentsInChildren<openTooth>();

            foreach (TeethDirtClean component in teethDirtClean_comp) if (component.enabled) component.enabled = true;
            foreach (openTooth component in openTooth_comp) if (component.enabled) component.enabled = true;
        }
    }
    #endregion
}
