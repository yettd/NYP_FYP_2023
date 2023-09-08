using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StorageComponent : MonoBehaviour
{
    [SerializeField] private GameObject unit;
    private StorageManager storage = null;

    private const string storagePath = "StorageAssets/";
    private const string storageAssetName = "Storage";
    private const string storageItemAssetName = "ToolBox";

    void Start()
    {
        CreateStorageFunction();
    }

    #region SETUP
    private void CreateStorageFunction()
    {
        if (storage == null)
        {
            GameObject spawnStorage = Instantiate(Resources.Load<GameObject>(storagePath + storageAssetName));
            storage = spawnStorage.GetComponent<StorageManager>();
            storage.LoadAssetStorage(this);
        }
    }

    private RawImage GetItemSlot(string slot)
    {
        RawImage img = Resources.Load<RawImage>(storagePath + storageItemAssetName);
        RawImage clone = Instantiate(img);
        clone.transform.SetParent(unit.transform);

        return clone;
    }

    private void ResetDisplayUnit()
    {
        for (int index = 0; index < unit.transform.childCount; index++)
            Destroy(unit.transform.GetChild(index).gameObject);
    }
    #endregion

    #region MAIN
    public void GetUnitDisplay(ItemTag[] items)
    {
        ResetDisplayUnit();

        for (int index = 0; index < items.Length; index++)
        {
            RawImage slot = GetItemSlot(items[index].itemName);
            slot.texture = items[index].icon;
        }
    }
    #endregion
}
