using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*This is how u can identify which page is it
 The way i use it is setting wat page it came from
from there, can check whether came from Main Scene or Video Scene 
can oso check within Main Scene since have Tool Info and Tool Selection Game Object working as panels*/
public enum ButtonENUM 
{ 
    MAINSCENE,
    TOOLSELECTION,
    ARBIT,
    TOOLINFO,
    ASSESSMENT,
    DEMOVID,
    SETTINGS
}

/*This is to check whether user click Dental Hygiene or Dental Therapy option
 None to set it to normal aka main menu*/
public enum DTHEnum
{
    DT,
    DH,
    NONE
}

public enum CollectionEnum
{
    S,
    E,
    F,
    NONE
}


public class ButtonReferenceManager : MonoBehaviour
{
    public static ButtonReferenceManager Instance { get; private set; }
    public ButtonENUM storedButtonID;
    public DTHEnum storedDTHButtonID=DTHEnum.DH;
    public CollectionEnum storeCollectionID=CollectionEnum.S;
    public bool ARTorP = false;
    public int storedIndex;
    public List<DentistTool> dtTools = new List<DentistTool>();
    public List<DentistTool> dhTools = new List<DentistTool>();
    public DentistTool[] S;
    public DentistTool[] E;
    public DentistTool[] F;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
        ARTorP = false;
        DontDestroyOnLoad(this.gameObject);
        LoadToolsDatabases();
        storedDTHButtonID = DTHEnum.DH;
    }

    public void LoadToolsDatabases()
    {
        dhTools.Clear();
        dtTools.Clear();
        ARTorP = false;
        storedDTHButtonID = DTHEnum.DH;
        storeCollectionID = CollectionEnum.S;
        string a = Saving.save.LoadFromJson("game");


        S = Resources.LoadAll<DentistTool>("AllTheTools/Scaling");

        F = Resources.LoadAll<DentistTool>("AllTheTools/Filling");

        E = Resources.LoadAll<DentistTool>("AllTheTools/Extraction");
        GetDh();
        GetDt();
        if (a!=null)
        {
            GameCompletion GC = JsonUtility.FromJson<GameCompletion>(a);


            
        
        }
    }

    public void unlockAchivement()
    {
        Debug.Log("asdasd");
        bool ok = true;
        foreach (DentistTool tool in E)
        {
            if(tool.view==false)
            {
                ok = false;
                break;
            }
        }
        if(ok)
        {
            achivmen.instance.UnlockAchivement(6, "seenAllextration");
        }
        ok = true;
        foreach (DentistTool tool in S)
        {
            if (tool.view == false)
            {
                ok = false;
                break;
            }
        }
        if (ok)
        {
            achivmen.instance.UnlockAchivement(4, "seenAllextration");
        }
        ok = true;
        foreach (DentistTool tool in F)
        {
            if (tool.view == false)
            {
                ok = false;
                break;
            }
        }
        if (ok)
        {
            achivmen.instance.UnlockAchivement(5, "seenAllextration");
        }
        if(achivmen.instance.GetIfUnlock(6) && achivmen.instance.GetIfUnlock(4) && achivmen.instance.GetIfUnlock(5))
        {
            achivmen.instance.UnlockAchivement(7, "seenAllextration");
        }
        ok = true;
    }

    void GetDh()
    {
        foreach(DentistTool a in S)
        {
            if(!a.DTDH)
            {
                dhTools.Add(a);
            }
        }
        foreach (DentistTool a in E)
        {
            if (!a.DTDH)
            {
                dhTools.Add(a);
            }
        }
        foreach (DentistTool a in F)
        {
            if (!a.DTDH)
            {
                dhTools.Add(a);
            }
        }

    }
    void GetDt()
    {
        foreach (DentistTool a in S)
        {
            if (a.DTDH)
            {
                dtTools.Add(a);
            }
        }
        foreach (DentistTool a in E)
        {
            if (a.DTDH)
            {
                dtTools.Add(a);
            }
        }
        foreach (DentistTool a in F)
        {
            if (a.DTDH)
            {
                dtTools.Add(a);
            }
        }
    }

}
