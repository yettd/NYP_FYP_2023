using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemTag
{
    public string itemName;
    public Texture icon;

    public ItemTag(string _itemName, Texture _icon)
    {
        itemName = _itemName;
        icon = _icon;
    }
}
