using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DW_ToolsNagivator
{
    private static DW_ToolsNagivator script;

    private DW_ExtractionCapability extractionAccess;
    private string currentTool;
    private string previousTool;

    public DW_ToolsNagivator()
    {
        extractionAccess = new DW_ExtractionCapability();
        currentTool = string.Empty;
        previousTool = " ";
    }

    public static DW_ToolsNagivator GetToolNagivator()
    {
        if (script == null)
        {
            script = new DW_ToolsNagivator();
        }
        return script;
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


        if (currentTool != previousTool || GameObject.FindGameObjectWithTag(TutorialGame_Script.thisScript.get_GameInfo[(int)GameTagPlacement.DW_Tool].props_tag_name)== null)
        {
            Debug.Log("creatingTool");
            GetInstanceOfToolUsage();
            previousTool = currentTool;
        }
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
            GameObject cloneTool = GameObject.Instantiate(Resources.Load<GameObject>("TutorialAssets/Tools/Marker/" + GetItemToUse(currentTool).model.name));
            cloneTool.tag = TutorialGame_Script.thisScript.get_GameInfo[(int)GameTagPlacement.DW_Tool].props_tag_name;
            Debug.Log(cloneTool.name);
            // Clear marker as there are any dulipate tool convention
            CleanUpUsedMarker(GetItemToUse(currentTool).model.name);
            cloneTool.name = GetItemToUse(currentTool).model.name;

            // Give access to capability level


            if (TutorialNagivatorScript.Instance().get_manual.toolAccessId == 1)
            {

                extractionAccess.GrantToolCapability(cloneTool, GetItemToUse(currentTool).itemName);
            }
        }
    }

    private void CleanUpToolUsed()
    {
        // Finding of used tool
        GameObject usedTool = GameObject.FindGameObjectWithTag(TutorialGame_Script.thisScript.get_GameInfo[(int)GameTagPlacement.DW_Tool].props_tag_name);

        // Despawn of used tool
        if (usedTool) { GameObject.Destroy(usedTool); }
    }

    private void CleanUpUsedMarker(string newTool)
    {
        // Finding for marker
        GameObject toolMarker = GameObject.Find(newTool);

        // Despawn of used marker
        if (toolMarker && toolMarker.tag != TutorialGame_Script.thisScript.get_GameInfo[(int)GameTagPlacement.DW_Tool].props_tag_name)
            GameObject.Destroy(toolMarker);
    }
    #endregion
}
