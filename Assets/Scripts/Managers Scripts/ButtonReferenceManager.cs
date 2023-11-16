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

    CollectionBase CB;
    

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
        string a = Saving.save.LoadFromJson("collection");
     
    }

    public void LoadToolsDatabases()
    {
        dhTools.Clear();
        dtTools.Clear();
        ARTorP = false;
        storedDTHButtonID = DTHEnum.DH;
        storeCollectionID = CollectionEnum.S;


        S = Resources.LoadAll<DentistTool>("AllTheTools/Scaling");

        F = Resources.LoadAll<DentistTool>("AllTheTools/Filling");

        E = Resources.LoadAll<DentistTool>("AllTheTools/Extraction");
        GetDh();
        GetDt();
        string a = Saving.save.LoadFromJson("collection");
        if (a != null)
        {
            //CB = JsonUtility.FromJson<CollectionBase>(Saving.save.LoadFromJson("collection"));

           


            //for (int i = 0; i < CB.toolsName.Count; i++)
            //{
            //    foreach(DentistTool dt in S)
            //    {
            //        if(dt.Name == CB.toolsName[i])
            //        {
            //            Debug.Log($"{CB.toolsName[i]} && {CB.rusty[i]}" );
            //            dt.rusty = CB.rusty[i];
            //            dt.view = CB.view[i];
            //        }
            //    }
            //}

            //for (int i = 0; i < CB.toolsName.Count; i++)
            //{
            //    foreach (DentistTool dt in F)
            //    {
            //        if (dt.Name == CB.toolsName[i])
            //        {
            //            dt.rusty = CB.rusty[i];
            //            dt.view = CB.view[i];
            //        }
            //    }
            //}

            //for (int i = 0; i < CB.toolsName.Count; i++)
            //{
            //    foreach (DentistTool dt in E)
            //    {
            //        if (dt.Name == CB.toolsName[i])
            //        {
            //            dt.rusty = CB.rusty[i];
            //            dt.view = CB.view[i];
            //        }
            //    }
            //}
        }
        else
        {
            CB = new CollectionBase();
            foreach (DentistTool dt in S)
            {
                dt.view = false; dt.rusty = true;
            }
            foreach (DentistTool dt in E)
            {
                dt.view = false; dt.rusty = true;
            }
            foreach (DentistTool dt in F)
            {
                dt.view = false; dt.rusty = true;
            }
            //foreach (DentistTool s in S)
            //{
            //    CB.toolsName.Add(s.Name);
            //    CB.view.Add(false);
            //    CB.rusty.Add(true);
            //}
            //foreach (DentistTool s in E)
            //{
            //    CB.toolsName.Add(s.Name);
            //    CB.view.Add(false);
            //    CB.rusty.Add(true);
            //}
            //foreach (DentistTool s in F)
            //{
            //    CB.toolsName.Add(s.Name);
            //    CB.view.Add(false);
            //    CB.rusty.Add(true);
            //}
            Saving.save.saveToJson(CB,"collection");
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
    public void ResetTools()
    {
        foreach (DentistTool a in S)
        {
            a.view = false;
            a.rusty = true;
        }
        foreach (DentistTool a in E)
        {
            a.view = false;
            a.rusty = true;
        }
        foreach (DentistTool a in F)
        {
            a.view = false;
            a.rusty = true;
        }
    }
}

public class CollectionBase
{
   public List<string> toolsName = new List<string>();
   public List<bool> rusty = new List<bool>();
   public List<bool> view = new List<bool>();
}
