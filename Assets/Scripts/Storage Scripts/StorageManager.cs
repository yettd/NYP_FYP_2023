using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorageManager : MonoBehaviour
{
    [SerializeField] private StorageComponent storage;
    private const string storageIconPath = "StorageAssets/Icon/";

    private List<ItemTag> items;
    private GameObject currentItemModel;

    #region SETUP
    public void LoadAssetStorage(StorageComponent component)
    {
        storage = component;
        items = new List<ItemTag>();
    }

    private void CreateNewItemEntry(string title, Texture icon, GameObject itemModelPrefab)
    {
        items.Add(new ItemTag(title, icon, itemModelPrefab));
    }


    private void RemoveItemEntry(string title)
    {
        items.Remove(GetItemStorage(title));
    }
    #endregion
    public void DisplayItemModel(ItemTag item)
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
    #region MAIN
    public void AddItem(ItemTag item)
    {
        if (items.Contains(GetItemStorage(item.itemName)))
        {
            ModifyItemContent(item);
        }
        else
        {
            CreateNewItemEntry(item.itemName, item.icon, item.itemModelPrefab);
        }

        // Update
        storage.GetUnitDisplay(items.ToArray());
    }


    public void RemoveItem(string title)
    {
        if (items.Contains(GetItemStorage(title))) { ModifyItemContent(GetItemStorage(title)); }
        else { RemoveItemEntry(title); }

        // Update
        storage.GetUnitDisplay(items.ToArray());
    }
    #endregion

    #region COMPONENT
    private void ModifyItemContent(ItemTag item)
    {
        ItemTag temp = item;
        ReplaceItemWithNewContent(temp);
    }

    private void ReplaceItemWithNewContent(ItemTag item)
    {
        for (int search = 0; search < items.ToArray().Length; search++)
        {
            if (items[search].itemName == item.itemName)
            {
                items.Remove(items[search]);
                items.Insert(search, item);
            }
        }
    }

    #endregion

    #region CONDITION
    private ItemTag GetItemStorage(string title)
    {
        foreach (ItemTag item in items.ToArray())
        {
            if (item.itemName == title)
                return item;
        }

        return null;
    }
    #endregion

    #region MISC
    private Texture GetItemIcon(string title)
    {
        Texture texture = Resources.Load<Texture>(storageIconPath + title);
        return texture;
    }
    #endregion
}
