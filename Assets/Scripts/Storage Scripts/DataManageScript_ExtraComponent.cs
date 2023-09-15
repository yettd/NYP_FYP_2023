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
        reader.Close();

        return JsonConvert.DeserializeObject<T>(readString);
    }
    #endregion

    #region MISC
    public static string GetPath(this DataManageScript script, string path)
    {
        return (Application.isEditor ? path : Application.persistentDataPath);
    }
    #endregion
}
