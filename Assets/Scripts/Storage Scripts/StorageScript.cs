using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorageScript : MonoBehaviour
{
    private StorageManager storage;
    [SerializeField] private InstructionManual manual;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("LoadAssetsForUse", 0.5f);
    }

    #region MAIN
    private void LoadAssetsForUse()
    {
        storage = GetComponent<StorageManager>();
        manual = TutorialNagivatorScript.thisScript.get_manual;
        LoadToolForSelection();
    }

    private void LoadToolForSelection()
    {
        foreach (ItemTag tool in manual.tools)
            storage.AddItem(tool);

        // Default item
        storage.AddItem(new ItemTag("N", Resources.Load<Texture>("StorageAssets/Icon/Select")));
    }
    #endregion
}
