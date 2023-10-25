using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorageItemScript : MonoBehaviour
{
    private StorageComponent component;
    private ItemTag item;
    private DW_ToolsNagivator toolsNagivator;

    private GameObject itemModel;
    public moveTools MTS;

    #region SETUP
    public void SetItemComponent(StorageComponent component, ItemTag item)
    {
        toolsNagivator = DW_ToolsNagivator.GetToolNagivator();

        this.component = component;
        this.item = item;
    }
    #endregion

    #region MAIN
    public void SelectItem()
    {
        // Select item for uses
        component.GetItemSelection(item);

        // Target selected area on item currently used
        if (TutorialNagivatorScript.Instance().get_manual.toolAccessId == 1) toolsNagivator.GetToolsOnUse(item.itemName);
    }

    public void SelectTool()
    {
       if (TutorialNagivatorScript.Instance().get_manual.toolAccessId != 1) minigameTaskListController.Instance.ToolsSelected(item.itemName, item.model);
    }
    #endregion
}
