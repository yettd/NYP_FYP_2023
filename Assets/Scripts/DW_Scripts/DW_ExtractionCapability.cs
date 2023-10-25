using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DW_ExtractionCapability
{
    private List<string> capabilityCoder;
    private GameObject accessor;

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
        foreach (InstructionTemplate instruction in TutorialNagivatorScript.Instance().get_manual.step)
            capabilityCoder.Add(instruction.requiredTool);
    }
    #endregion

    #region MAIN
    public void GrantToolCapability(GameObject target, string grantId)
    {
        // Currently selected tool need to be granted tool access
        accessor = target;

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
        // Extraction Tool: Currently used
        switch (index)
        {
            case (int)CAPABILITY.AnestheicTool:
                accessor.AddComponent<DW_AnestheticTool>().Activate();
                break;

            case (int)CAPABILITY.Forceps:
                accessor.AddComponent<DW_ForcepsTool>().Activate();
                break;

            case (int)CAPABILITY.CottonGauze:
                accessor.AddComponent<DW_CottonGauze>().Activate();
                break;

            case (int)CAPABILITY.None:
                break;
        }
    }
    #endregion
}
