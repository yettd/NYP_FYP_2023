using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorageScript : MonoBehaviour
{
    private StorageManager storage;
    private StorageComponent component;

    [SerializeField] private InstructionManual manual;

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
        storage = StorageManager.GetInventory();

        if (TutorialNagivatorScript.getScript != null) { manual = TutorialNagivatorScript.Instance().get_manual; }
        else { manual = new InstructionManual(); }

        LoadToolForSelection();
    }

    private void LoadToolForSelection()
    {
        if (TutorialNagivatorScript.getScript != null) { foreach (ItemTag tool in manual.tools) storage.AddItem(tool); }

        // Default item
        storage.AddItem(new ItemTag("N", Resources.Load<Texture>("StorageAssets/Icon/Select")));

        // Display item
        component.GetUnitDisplay(storage.get_items.ToArray());
    }
    #endregion
}
