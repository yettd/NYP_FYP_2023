using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorageScript : MonoBehaviour
{
    private StorageManager storage;
    private StorageComponent component;
    private InstructionManual manual;

    #region SETUP
    public void LoadAssetStorage(StorageComponent component)
    {
        this.component = component;
        LoadAssetsForUse();
    }
    #endregion

    #region MAIN
    private void LoadAssetsForUse()
    {
        // Load uniserval storage to store items
        storage = StorageManager.GetInventory();

        // Get loaded instruction log from start-menu otherwise load empty instruction log
        if (TutorialNagivatorScript.getScript != null) { manual = TutorialNagivatorScript.Instance().get_manual; }
        else { manual = new InstructionManual(); }

        // Start to load items
        LoadToolForSelection();
    }

    private void LoadToolForSelection()
    {
        // Add all selected item into slot
        storage.RemoveAllItems();
        foreach (ItemTag tool in manual.tools) storage.AddItem(tool);

        // Default item
        storage.AddItem(new ItemTag("N", Resources.Load<Texture>("StorageAssets/Icon/Select"), Resources.Load<GameObject>("StorageAssets/Models")));

        // Display item
        component.GetUnitDisplay(storage.get_items.ToArray());
    }
    #endregion
}