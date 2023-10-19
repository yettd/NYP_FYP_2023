using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorageManager
{
    private static StorageManager script;

    private const string storageIconPath = "StorageAssets/Icon/";
    public string getStorageIconPath { get { return storageIconPath; } }

    private List<ItemTag> items;
    public List<ItemTag> get_items { get { return items; } }

    public static StorageManager GetInventory()
    {
        if (script == null)
        {
            script = new StorageManager();
            script.items = new List<ItemTag>();
        }
        return script;
    }

<<<<<<< Updated upstream
    #region SETUP
    private void CreateNewItemEntry(string title, Texture icon,GameObject model)
    {
        items.Add(new ItemTag(title, icon, model));
=======
    private void CreateNewItemEntry(string title, Texture icon, GameObject ModelsOfTools)
    {
        items.Add(new ItemTag(title, icon, ModelsOfTools)) ;
>>>>>>> Stashed changes
    }

    private void RemoveItemEntry(string title)
    {
        items.Remove(GetItemStorage(title));
    }
    #endregion

    #region MAIN
    public void AddItem(ItemTag item)
    {
        if (items.Contains(GetItemStorage(item.itemName))) { ModifyItemContent(item); }
<<<<<<< Updated upstream
        else { CreateNewItemEntry(item.itemName, item.icon, item.model); }
=======
        else { CreateNewItemEntry(item.itemName, item.icon, item.ModelsOfTools); }

        // Update
        storage.GetUnitDisplay(items.ToArray());
>>>>>>> Stashed changes
    }

    public void RemoveItem(string title)
    {
        if (items.Contains(GetItemStorage(title))) { ModifyItemContent(GetItemStorage(title)); }
        else { RemoveItemEntry(title); }
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
}
