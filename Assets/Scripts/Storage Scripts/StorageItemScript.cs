using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorageItemScript : MonoBehaviour
{
    private StorageComponent component;
    private ItemTag item;

    #region SETUP
    public void SetItemComponent(StorageComponent component, ItemTag item)
    {
        this.component = component;
        this.item = item;
    }
    #endregion

    #region MAIN
    public void SelectItem()
    {
        component.GetItemSelection(item);
    }
    public void SelectTool()
    {
        minigameTaskListController.Instance.ToolsSelected(item.itemName);
    }
    #endregion
}
