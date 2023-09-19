using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StorageComponent : MonoBehaviour
{
    [SerializeField] private GameObject unit;
    [SerializeField] private GameObject displayTab;

    private StorageScript storage = null;

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
            storage = spawnStorage.GetComponent<StorageScript>();
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
            slot.GetComponent<StorageItemScript>().SetItemComponent(this, items[index]);
        }
    }
    #endregion

    #region SUB_MAIN
    public void GetItemSelection(ItemTag item)
    {
        displayTab.transform.GetChild(0).GetComponent<TMP_Text>().text = item.itemName;
        displayTab.transform.GetChild(1).GetComponent<RawImage>().texture = item.icon;
    }
    #endregion

    #region COMPONENT
    private IEnumerator DisplayText()
    {
        displayTab.transform.GetChild(0).gameObject.SetActive(true);
        yield return new WaitForSeconds(1);
        displayTab.transform.GetChild(0).gameObject.SetActive(false);
    }
    #endregion
}
