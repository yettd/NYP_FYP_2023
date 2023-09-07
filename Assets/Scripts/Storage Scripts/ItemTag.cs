using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemTag : ScriptableObject
{
    public string itemName;
    public int quaility;

    public ItemTag(string _itemName, int _qualitiy)
    {
        itemName = _itemName;
        quaility = _qualitiy;
    }
}
