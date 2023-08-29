using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ToolInfoController : MonoBehaviour
{
    [SerializeField] private TMP_Text toolNameText;
    [SerializeField] private TMP_Text toolDescText;
    [SerializeField] private Image toolImage;

    private void OnEnable()
    {
        if (!ButtonReferenceManager.Instance)
            return;
        if (ButtonReferenceManager.Instance.storedDTHButtonID == DTHEnum.DH)
        {
            //If DTH is DH, show DH stuff..
            LoadContent(ButtonReferenceManager.Instance.dhTools[ButtonReferenceManager.Instance.storedIndex]);
        }
        else if (ButtonReferenceManager.Instance.storedDTHButtonID == DTHEnum.DT)
        {
            //If DTH is DH, show DH stuff..
            LoadContent(ButtonReferenceManager.Instance.dtTools[ButtonReferenceManager.Instance.storedIndex]);
        }

    }
    public void LoadContent(DentistTool dentistTool)
    {
        //Load the data from scriptableObject and assign them
        toolNameText.text = dentistTool.Name;
        toolDescText.text = dentistTool.Usage + "\n" + dentistTool.InstrumentGrasp + "\n" + dentistTool.Instrumentation;
        toolImage.sprite = dentistTool.Icon;
    }

  
}
