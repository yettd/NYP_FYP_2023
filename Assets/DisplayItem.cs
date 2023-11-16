using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
public class DisplayItem : MonoBehaviour
{

    [SerializeField] public TMP_Text toolNameText;
    [SerializeField] public TMP_Text toolDescText;
    [SerializeField]  public Image toolImage;
    Vector3 currentScale;
    string[] Text;
    int currentValue = 0;

    // Start is called before the first frame update
    void Start()
    {
        currentScale = transform.localScale;
        transform.localScale = Vector3.zero;
    }

    private void OnMouseDown()
    {
        Debug.Log("asdasfsdg"); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        if (!ButtonReferenceManager.Instance)
            return;

        //If DTH is DH, show DH stuff..

        //DisplayNew();
        //Expand();
    }

    public void LoadContent(DentistTool dentistTool)
    {
        //Load the data from scriptableObject and assign them

        string i = (dentistTool.Usage != "") ? $"\n {dentistTool.Usage}" : "";

        string b = (dentistTool.InstrumentGrasp != "") ? $"\n {dentistTool.InstrumentGrasp}" : "";
        string c = (dentistTool.Instrumentation != "") ? $"\n {dentistTool.Instrumentation}" : "";
        string everything = dentistTool.Name + i + (b==""? null:b) + (c == "" ? null : c);
        Debug.Log(everything);
        Spread(everything);
        //toolNameText.text = dentistTool.Name;
        toolDescText.text = Text[0];
        currentValue = 0;
        toolImage.sprite = dentistTool.Icon;
        dentistTool.view = true;
        ButtonReferenceManager.Instance.unlockAchivement();

        //CollectionBase CB;
        //string a = Saving.save.LoadFromJson("collection");
        //if (a != null)
        //{
        //    CB = JsonUtility.FromJson<CollectionBase>(Saving.save.LoadFromJson("collection"));

        //    for (int j = 0; j < CB.toolsName.Count; j++)
        //    {
        //        if(CB.toolsName[j]==dentistTool.Name)
        //        {
        //            CB.view[j] = true;
        //        }
        //    }
        //    Saving.save.saveToJson(CB, "collection");

        //}


    }

    public void Spread(string a)
    {
        Text = a.Split("\n");
        Debug.Log(Text);
      
    }

    public void NextText(bool e)
    {
        int oldValue = currentValue;
        if(e)
        {
            currentValue++;

        }
        else
        {
            currentValue--;

        }

        if(currentValue <0 || currentValue >=Text.Length)
        {
            currentValue = oldValue;
        }
        toolDescText.text = Text[currentValue];
    }

    public void DisplayNew()
    {

        if (collectionManager.CM.toolOrProcedure)
        {
            if (ButtonReferenceManager.Instance.storeCollectionID == CollectionEnum.S)
            {

                LoadContent(ButtonReferenceManager.Instance.S[ButtonReferenceManager.Instance.storedIndex]);
            }
            else if (ButtonReferenceManager.Instance.storeCollectionID == CollectionEnum.E)
            {

                LoadContent(ButtonReferenceManager.Instance.E[ButtonReferenceManager.Instance.storedIndex]);
            }
            else if (ButtonReferenceManager.Instance.storeCollectionID == CollectionEnum.F)
            {

                LoadContent(ButtonReferenceManager.Instance.F[ButtonReferenceManager.Instance.storedIndex]);
            }
        }
        else
        {
            if (ButtonReferenceManager.Instance.storedDTHButtonID == DTHEnum.DT)
            {

                LoadContent(ButtonReferenceManager.Instance.dtTools[ButtonReferenceManager.Instance.storedIndex]);
            }
            else if (ButtonReferenceManager.Instance.storedDTHButtonID == DTHEnum.DH)
            {

                LoadContent(ButtonReferenceManager.Instance.dhTools[ButtonReferenceManager.Instance.storedIndex]);
            }
        }

    }
    public void Expand()
    {
        transform.DOScale(currentScale, 1);
    }
    public void CloseWindow()
    {
        transform.DOScale(Vector3.zero, 1);
    }

}
