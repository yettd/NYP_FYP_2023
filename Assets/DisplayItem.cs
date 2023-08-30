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
        LoadContent(ButtonReferenceManager.Instance.dtTools[collectionManager.CM.GetStore()]);
        

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

        LoadContent(ButtonReferenceManager.Instance.dtTools[collectionManager.CM.GetStore()]);
    }

}