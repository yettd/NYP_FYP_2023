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
        Forceps,
        CottonGauze,
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
        foreach (InstructionTemplate instruction in TutorialNagivatorScript.Instance().get_manual.step)
        {
            capabilityCoder.Add(instruction.requiredTool);
        }
    }
    #endregion

    #region MAIN
    public void GrantToolCapability(GameObject target, string grantId)
    {
        accessor = target;
        AcceptToolAndGrant(EncodeCapabilityOfGrantId(grantId));
    }
    #endregion

    #region COMPONENT
    private int EncodeCapabilityOfGrantId(string id)
    {
        for (int index = 0; index < capabilityCoder.ToArray().Length; index++)
            if (capabilityCoder[index] == id) return index;

        return (int)CAPABILITY.None;
    }

    private void AcceptToolAndGrant(int index)
    {
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
