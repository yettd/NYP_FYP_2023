using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemTag
{
    public string itemName;
    public Texture icon;
    public GameObject model;
    public ItemTag(string _itemName, Texture _icon, GameObject _model)
    {
        itemName = _itemName;
        icon = _icon;
        model = _model;
    }
}
