using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DW_ToolsNagivator
{
    private enum CapabilityMethod { SCALING, EXTRACTION, FILLING, NONE };

    private const string toolSelectedTag = "DW_Tool";
    private string currentTool;
    private DW_ExtractionCapability extractionAccess;

    public DW_ToolsNagivator()
    {
        currentTool = string.Empty;
        extractionAccess = new DW_ExtractionCapability();
    }

    #region SETUP
    private ItemTag GetItemToUse(string title)
    {
        foreach (ItemTag tool in StorageManager.GetInventory().get_items.ToArray())
            if (tool.itemName == title) return tool;

        return null;
    }
    #endregion

    #region MAIN
    public void GetToolsOnUse(string title)
    {
        // Search and get tool on use
        currentTool = title;

        // Create instance and process using it
        GetInstanceOfToolUsage();
    }
    #endregion

    #region COMPONENT
    private void GetInstanceOfToolUsage()
    {
        // Clear any used tool to prevent tool pile
        GameObject usedTool = GameObject.FindGameObjectWithTag(toolSelectedTag);
        if (usedTool) { GameObject.Destroy(usedTool); }

        // Get new tool for use
        if (GetItemToUse(currentTool).model != null)
        {
            GameObject cloneTool = GameObject.Instantiate(GetItemToUse(currentTool).model, new Vector3(6,0,0), Quaternion.identity);
            cloneTool.transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);
            cloneTool.tag = toolSelectedTag;

            // Give access to capability level
            GrantCapabilityOnSelectedTool(cloneTool, TutorialNagivatorScript.Instance().get_manual.toolAccessId);
        }       
    }

    private void GrantCapabilityOnSelectedTool(GameObject clone, int index)
    {
        switch (index)
        {
            case (int)CapabilityMethod.SCALING:
                break;

            case (int)CapabilityMethod.EXTRACTION:
                extractionAccess.GrantToolCapability(clone, GetItemToUse(currentTool).itemName);
                break;

            case (int)CapabilityMethod.FILLING:
                break;
        }
    }
    #endregion
}
