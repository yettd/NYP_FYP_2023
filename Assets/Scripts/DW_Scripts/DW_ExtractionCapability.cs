using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class DW_ExtractionCapability
{
    private List<string> capabilityCoder;
    private GameObject accessor;

    private bool isSelectedAnestheicTool = false;
    private bool isSelectedForceps = false;

    private enum CAPABILITY
    {
        AnestheicTool,
        Forceps = 2,
        CottonGauze = 5,
        None
    }

    public DW_ExtractionCapability()
    {
        accessor = null;
        capabilityCoder = new List<string>();
        SetCapabilityProperties();
    }

    #region SETUP
    private void SetCapabilityProperties()
    {
        // Load information about the tool are needed for this level
        if (TutorialNagivatorScript.Instance().get_manual.toolAccessId == 1)
        {
            foreach (InstructionTemplate instruction in TutorialNagivatorScript.Instance().get_manual.step)
                capabilityCoder.Add(instruction.requiredTool);


            Debug.Log($"{capabilityCoder.Count} :: asdasdasd");
        }
    }
    #endregion

    #region MAIN
    public void GrantToolCapability(GameObject target, string grantId)
    {
        Debug.Log($"grant ID :{grantId}");
        // Currently selected tool need to be granted tool access
        accessor = target;
        if(capabilityCoder.Count==0)
        {
            SetCapabilityProperties();
        }
        // Given the item name and identify the tool access
        AcceptToolAndGrant(EncodeCapabilityOfGrantId(grantId));
    }
    #endregion

    #region COMPONENT
    private int EncodeCapabilityOfGrantId(string id)
    {
        // Find the tool which are selected and identify it with the avaialble tool given
        for (int index = 0; index < capabilityCoder.ToArray().Length; index++)
            if (capabilityCoder[index] == id) return index;

        // Can't find the tool to define it then don't grant any access
        return (int)CAPABILITY.None;
    }

    private void AcceptToolAndGrant(int index)
    {
        Debug.Log(index);
        // Extraction Tool: Currently used
        switch (index)
        {
            case (int)CAPABILITY.AnestheicTool:
                // Get tool enable for use
                accessor.AddComponent<DW_AnestheticTool>().Activate();

                // Check tasking is completed
                if (!GetTaskCheckComplete(isSelectedAnestheicTool)) isSelectedAnestheicTool = true;
                break;

            case (int)CAPABILITY.Forceps:
                // Get tool enable for use
                accessor.AddComponent<DW_ForcepsTool>().Activate();

                // Check tasking is completed
                if (!GetTaskCheckComplete(isSelectedForceps)) isSelectedForceps = true;
                break;

            case (int)CAPABILITY.CottonGauze:
                // Get tool enable for use
                accessor.AddComponent<DW_CottonGauze>().Activate();

                // Check tasking is completed
                GetTaskCheckComplete(true);
                break;

            case (int)CAPABILITY.None:
                break;
        }
    }
    #endregion

    #region MISC
    private bool GetTaskCheckComplete(bool condition)
    {
        // Find for the step have been checked
        if (!condition)
        {
            // Get clearance from the latest with required tool for checking
            TutorialGame_Script.thisScript.getTasking.GetStepClearance(GameObject.FindGameObjectWithTag(TutorialGame_Script.thisScript.get_GameInfo[(int)GameTagPlacement.DW_Tool].props_tag_name).name);

            // Mark this check as completed
            return false;
        }

        // Prompt back the user that the current step have been completed
        TutorialGame_Script.thisScript.getTasking.GetStepClearance(string.Empty);
        return true;
    }
    #endregion
}
