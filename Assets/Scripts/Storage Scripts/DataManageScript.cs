using UnityEngine;
using System.IO;

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
        string data = JsonUtility.ToJson(text, true);
        WriteFile(data);
    }

    public void SaveInfoAsNewJson(object text)
    {
        ClearFile();
        if (text.ToString() != string.Empty) SaveInfoAsJson(text);
    }

    public string LoadRawScript()
    {
        ReadFile();
        return readString;
    }

    public T LoadInfoThroughJson<T>()
    {
        ReadFile();
        return JsonUtility.FromJson<T>(readString);
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
        reader.Close();
    }

    private void ClearFile()
    {
        if (FindFilePath()) File.WriteAllText(path + directoryName, string.Empty);
    }
    #endregion

    #region FILE
    public bool FindFilePath()
    {
        if (File.Exists(path + directoryName))
            return true;
        else
            return false;
    }
    #endregion

    #region MISC
    public string GetPath()
    {
        return path;
    }

    public string GetDirectoryName()
    {
        return directoryName;
    }

    public string GetReadString()
    {
        return readString;
    }
    #endregion
}
