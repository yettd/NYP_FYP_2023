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
        SetCapabilityProperties("A", "F", "C");
    }

    #region SETUP
    private void SetCapabilityProperties(string tool1, string tool2, string tool3)
    {
        capabilityCoder.Add(tool1);
        capabilityCoder.Add(tool2);
        capabilityCoder.Add(tool3);
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
                // accessor.AddComponent<DW_ForcepsTool>();
                break;

            case (int)CAPABILITY.CottonGauze:
                // accessor.AddComponent<DW_CottonGauze>();
                break;

            case (int)CAPABILITY.None:
                break;
        }
    }
    #endregion
}
