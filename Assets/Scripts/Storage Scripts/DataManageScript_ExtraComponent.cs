using UnityEngine;
using System.IO;
using Newtonsoft.Json;

public static class DataManageScript_ExtraComponent
{
    #region EXTENSION
    public static T LoadInfoThroughJson2<T>(this DataManageScript script)
    {
        StreamReader reader = new StreamReader(script.GetPath() + script.GetDirectoryName());
        string readString = reader.ReadToEnd();
        return JsonConvert.DeserializeObject<T>(readString);
    }
    #endregion

    #region MISC
    public static void SaveInfoAsNewJson2(this DataManageScript script, object text)
    {
        if (PlayerPrefs.HasKey(script.GetPath() + script.GetDirectoryName())) PlayerPrefs.DeleteKey(script.GetPath() + script.GetDirectoryName());
        PlayerPrefs.SetString(script.GetPath() + script.GetDirectoryName(), JsonUtility.ToJson(text));
    }

    public static T LoadInfoThroughJson2_1<T>(this DataManageScript script)
    {
        string readString = PlayerPrefs.GetString(script.GetPath() + script.GetDirectoryName(), string.Empty);
        return JsonConvert.DeserializeObject<T>(readString);
    }
    #endregion
}
