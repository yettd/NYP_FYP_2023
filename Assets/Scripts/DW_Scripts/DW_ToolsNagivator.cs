using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DW_ToolsNagivator
{
    private DW_ExtractionCapability extractionAccess;

    private string currentTool;
    private Vector3 startingPoint;

    public DW_ToolsNagivator()
    {
        extractionAccess = new DW_ExtractionCapability();
        currentTool = string.Empty;
        startingPoint = new Vector3(6, 0, 0);
    }

    public DW_ToolsNagivator(Vector3 startingPoint, Vector3 toolScale)
    {
        extractionAccess = new DW_ExtractionCapability();
        currentTool = string.Empty;
        this.startingPoint = startingPoint;
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
        CleanUpToolUsed();

        // Get new tool for use
        if (GetItemToUse(currentTool).model != null)
        {
            GameObject cloneTool = GameObject.Instantiate(GetItemToUse(currentTool).model);
            cloneTool.tag = TutorialGame_Script.thisScript.get_dwTool;

            // Give access to capability level
            if (TutorialNagivatorScript.Instance().get_manual.toolAccessId == 1)
                extractionAccess.GrantToolCapability(cloneTool, GetItemToUse(currentTool).itemName);
        }       
    }

    private void CleanUpToolUsed()
    {
        GameObject usedTool = GameObject.FindGameObjectWithTag(TutorialGame_Script.thisScript.get_dwTool);
        if (usedTool) { GameObject.Destroy(usedTool); }
    }
    #endregion
}
