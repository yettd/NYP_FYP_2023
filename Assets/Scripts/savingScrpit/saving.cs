using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class Saving : MonoBehaviour
{
    // Start is called before the first frame update

    //how to use saveFeauture

    //create class then use the savemethod save(to which file);

    //loadsave(which json file) 

    public static Saving save;
    public void saveToJson(object p, string a)
    {
        string playerData = JsonUtility.ToJson(p);

        string filePath = Application.persistentDataPath + $"/save/{a}.json";
        System.IO.File.WriteAllText(filePath, playerData);
    }

    private void Awake()
    {

        if(save==null)
        {
            save = this;
        }
        else
        {
            Destroy(gameObject);
        }

        string filePath = Application.persistentDataPath + "/save";
        if (!System.IO.Directory.Exists(filePath)) // if /save folder dont exist creast one
        {
            System.IO.Directory.CreateDirectory(Application.persistentDataPath + "/save");
            System.IO.File.Create(Application.persistentDataPath + "/current.txt");
        }

        DontDestroyOnLoad(gameObject);
    }

    
  
    public string LoadFromJson(string a) //load json folder to get stuff like if minigame was completed
    {

        string filePath = Application.persistentDataPath + $"/save/{a}.json";
        if (!System.IO.File.Exists(filePath))
        {
            return null;
        }
        string playerData = System.IO.File.ReadAllText(filePath);

        return playerData;
    }
    public void clearSave()
    {
        string filePath = Application.persistentDataPath + $"/save/";

        if (!System.IO.File.Exists(filePath))
        {
            return;
        }
        System.IO.File.Delete(filePath);
    }

    public string LoadSoundFromJson()
    {

        string filePath = Application.persistentDataPath + $"/SoundSetting/sound.json";
        if (!System.IO.File.Exists(filePath))
        {
            return null;
        }
        string playerData = System.IO.File.ReadAllText(filePath);

        return playerData;
    }
    public void saveSoundToJson(object p)
    {
        string playerData = JsonUtility.ToJson(p);
        string filePath = Application.persistentDataPath + $"/SoundSetting/sound.json";
        System.IO.File.WriteAllText(filePath, playerData);
    }
}
