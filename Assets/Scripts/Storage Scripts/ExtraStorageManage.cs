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
}
