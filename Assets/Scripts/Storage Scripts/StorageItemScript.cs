using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorageItemScript : MonoBehaviour
{
    private StorageComponent component;
    private ItemTag item;
    private GameObject currentItemModel;
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
        DisplayItemModel();
    }
    public void SelectTool()
    {
        minigameTaskListController.Instance.ToolsSelected(item.itemName);
    }

     public void DisplayItemModel()
    {
        if (currentItemModel != null)
        {
            Destroy(currentItemModel);
        }

        if (item != null && item.itemModelPrefab != null)
        {
            currentItemModel = Instantiate(item.itemModelPrefab, Vector3.zero, Quaternion.identity);
        }
    }
    #endregion
}
