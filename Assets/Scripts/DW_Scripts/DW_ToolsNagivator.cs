using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DW_ToolsNagivator
{
    private DW_ExtractionCapability extractionAccess;
    private string currentTool;

    public DW_ToolsNagivator()
    {
        extractionAccess = new DW_ExtractionCapability();
        currentTool = string.Empty;
    }

    public DW_ToolsNagivator(Vector3 toolScale)
    {
        extractionAccess = new DW_ExtractionCapability();
        currentTool = string.Empty;
    }

    #region SETUP
    private ItemTag GetItemToUse(string title)
    {
        // Finding the tool info for reference
        foreach (ItemTag tool in StorageManager.GetInventory().get_items.ToArray())
            if (tool.itemName == title) return tool;

        // Can't find the tool and not be able to reference
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
        CleanUpToolUsed();

        // Get new tool for use
        if (GetItemToUse(currentTool).model != null)
        {
            // Take out on the tool been selected
            GameObject cloneTool = GameObject.Instantiate(GetItemToUse(currentTool).model);
            cloneTool.tag = TutorialGame_Script.thisScript.get_GameInfo[(int)GameTagPlacement.DW_Tool].props_tag_name;

            // Give access to capability level
            if (TutorialNagivatorScript.Instance().get_manual.toolAccessId == 1)
                extractionAccess.GrantToolCapability(cloneTool, GetItemToUse(currentTool).itemName);
        }       
    }

    private void CleanUpToolUsed()
    {
        // Finding of used tool
        GameObject usedTool = GameObject.FindGameObjectWithTag(TutorialGame_Script.thisScript.get_GameInfo[(int)GameTagPlacement.DW_Tool].props_tag_name);

        // Despawn of used tool
        if (usedTool) { GameObject.Destroy(usedTool); }
    }
    #endregion
}
