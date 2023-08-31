using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class DisplayItem : MonoBehaviour
{

    [SerializeField] public TMP_Text toolNameText;
    [SerializeField] public TMP_Text toolDescText;
    [SerializeField]  public Image toolImage;
    // Start is called before the first frame update
    void Start()
    {
        
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

        DisplayNew();

    }

    public void LoadContent(DentistTool dentistTool)
    {
        //Load the data from scriptableObject and assign them
        toolNameText.text = dentistTool.Name;
        toolDescText.text = dentistTool.Usage + "\n" + dentistTool.InstrumentGrasp + "\n" + dentistTool.Instrumentation;
        toolImage.sprite = dentistTool.Icon;
    }

    public void DisplayNew()
    {

        if (collectionManager.CM.toolOrProcedure)
        {
            if (ButtonReferenceManager.Instance.storeCollectionID == CollectionEnum.S)
            {

                LoadContent(ButtonReferenceManager.Instance.S[collectionManager.CM.GetStore()]);
            }
            else if (ButtonReferenceManager.Instance.storeCollectionID == CollectionEnum.E)
            {

                LoadContent(ButtonReferenceManager.Instance.E[collectionManager.CM.GetStore()]);
            }
            else if (ButtonReferenceManager.Instance.storeCollectionID == CollectionEnum.F)
            {

                LoadContent(ButtonReferenceManager.Instance.F[collectionManager.CM.GetStore()]);
            }
        }
        else
        {
            if (ButtonReferenceManager.Instance.storedDTHButtonID == DTHEnum.DT)
            {

                LoadContent(ButtonReferenceManager.Instance.dtTools[collectionManager.CM.GetStore()]);
            }
            else if (ButtonReferenceManager.Instance.storedDTHButtonID == DTHEnum.DH)
            {

                LoadContent(ButtonReferenceManager.Instance.dhTools[collectionManager.CM.GetStore()]);
            }
        }

    }

}
