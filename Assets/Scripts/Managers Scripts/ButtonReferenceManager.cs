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
    public DTHEnum storedDTHButtonID;
    public CollectionEnum storeCollectionID;
    public int storedIndex;
    public DentistTool[] dtTools;
    public DentistTool[] dhTools;
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
        DontDestroyOnLoad(this.gameObject);
        LoadToolsDatabases();

    }

    void LoadToolsDatabases()
    {
        dhTools = Resources.LoadAll<DentistTool>("AllTheTools/DH");
        dtTools = Resources.LoadAll<DentistTool>("AllTheTools/DT");
        S = Resources.LoadAll<DentistTool>("AllTheTools/Scaling");
        F = Resources.LoadAll<DentistTool>("AllTheTools/Filling");
        E = Resources.LoadAll<DentistTool>("AllTheTools/Extraction");

    }

}
