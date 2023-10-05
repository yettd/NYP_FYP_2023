using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DW_ToolsNagivator
{
    private DW_ExtractionCapability extractionAccess;
    private const string toolSelectedTag = "DW_Tool";

    private string currentTool;
    private Vector3 startingPoint;
    private Vector3 toolScale;

    public DW_ToolsNagivator()
    {
        extractionAccess = new DW_ExtractionCapability();
        currentTool = string.Empty;
        startingPoint = new Vector3(6, 0, 0);
        toolScale = new Vector3(0.25f, 0.25f, 0.25f);
    }

    public DW_ToolsNagivator(Vector3 startingPoint, Vector3 toolScale)
    {
        extractionAccess = new DW_ExtractionCapability();
        currentTool = string.Empty;
        this.startingPoint = startingPoint;
        this.toolScale = toolScale;
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
            GameObject cloneTool = GameObject.Instantiate(GetItemToUse(currentTool).model, startingPoint, Quaternion.identity);
            cloneTool.transform.localScale = toolScale;
            cloneTool.tag = toolSelectedTag;

            // Give access to capability level
            if (TutorialNagivatorScript.Instance().get_manual.toolAccessId == 1)
                extractionAccess.GrantToolCapability(cloneTool, GetItemToUse(currentTool).itemName);
        }       
    }

    private void CleanUpToolUsed()
    {
        GameObject usedTool = GameObject.FindGameObjectWithTag(toolSelectedTag);
        if (usedTool) { GameObject.Destroy(usedTool); }
    }
    #endregion
}
