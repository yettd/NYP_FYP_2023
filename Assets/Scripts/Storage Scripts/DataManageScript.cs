using UnityEngine;
using System.IO;
using System.Collections;
using System.Collections.Generic;

public class DataManageScript
{
    string path;
    string directoryName;
    string readString;

    #region SETUP
    public DataManageScript(string path, string directoryName)
    {
        this.path = path;
        this.directoryName = directoryName;
    }
    #endregion

    #region MAIN
    public void SaveInfoAsJson(object text)
    {
        string data = JsonUtility.ToJson(text);
        WriteFile(data);
    }

    public void SaveInfoAsNewJson(object text)
    {
        ClearFile();
        if (text.ToString() != string.Empty) SaveInfoAsJson(text);
    }

    public void SaveInfo(object text)
    {
        WriteFile(text.ToString());
    }

    public string LoadInfoString()
    {
        ReadFile();
        return readString;
    }

    public T LoadInfoThroughJson<T>(string data)
    {
        Debug.Log(data);
        return JsonUtility.FromJson<T>(data);
    }
    #endregion

    #region COMPONENT
    private void WriteFile(string text)
    {
        StreamWriter writer = new StreamWriter(path + directoryName, true);
        writer.WriteLine(text);
        writer.Close();
    }

    private void ReadFile()
    {
        StreamReader reader = new StreamReader(path + directoryName, true);
        readString = reader.ReadToEnd();
        Debug.Log(readString);
        reader.Close();
    }

    private void ClearFile()
    {
        if (FindFilePath(directoryName)) File.Delete(path + directoryName);
    }
    #endregion

    #region FILE
    public bool FindFilePath(string directoryName)
    {
        if (File.Exists(path + directoryName))
            return true;
        else
            return false;
    }
    #endregion
}
