using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class ExtraStorageManage
{
    #region EXTENSION
    private static Texture GetItemIcon(this StorageManager script, string title)
    {
        Texture texture = Resources.Load<Texture>(script.getStorageIconPath + title);
        return texture;
    }
    #endregion

    #region MAIN
    // EXTRA: Container storage for storing items 
    public static StorageManager GetSafeStorage(this StorageManager script, int id)
    {
        StorageManager tempScript = null;
        DataManageScript data = new DataManageScript("Assets/Resources/StorageAssets/InventoryData/", "InventoryTag_" + id + ".txt");

        if (!data.FindFilePath()) tempScript = new StorageManager();
        return tempScript;
    }

    // EXTRA: Remove all items from uniserval/Container storage
    public static void RemoveAllItems(this StorageManager script)
    {
        if (script.get_items.ToArray().Length != 0) script.get_items.Clear();
        Debug.Log("Items have been clear...");
    }
    #endregion
}
