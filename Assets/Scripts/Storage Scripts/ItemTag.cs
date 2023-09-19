using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemTag
{
    public string itemName;
    public Texture icon;
    public GameObject itemModelPrefab;  
    public ItemTag(string _itemName, Texture _icon, GameObject _itemModelPrefab)
    {
        itemName = _itemName;
        icon = _icon;
        itemModelPrefab = _itemModelPrefab;
    }
}
