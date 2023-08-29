using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ARScanManager : MonoBehaviour
{
    [SerializeField] GameObject infoGameObject;
    [SerializeField] GameObject videoGameObject;

    [Header("This is for tool info")]
    [SerializeField] GameObject toolInfoGameObject;
    [SerializeField] Image toolInfoImage;
    [SerializeField] TMP_Text toolInfoName;
    [SerializeField] TMP_Text toolInfoContent;

    private Button infoButton;
    private Button videoButton;
    private DentistTool dentistTool;


    private void Start()
    {
        infoButton = infoGameObject.GetComponent<Button>();
        videoButton = videoGameObject.GetComponent<Button>();
        toolInfoGameObject.SetActive(false);        
    }

    public void HideButtons()
    {
        infoGameObject.SetActive(false);
        videoGameObject.SetActive(false);
    }


    //after scan
    //show button
    public void ShowButtons()
    {
        infoGameObject.SetActive(true);
        videoGameObject.SetActive(true);
    }
    //get tool info
    public void GetToolInfo(ModelTargetToolInfo modelTargetToolInfo)
    {
        this.dentistTool = modelTargetToolInfo.dentistTool;
    }

    //if info btn
    //show info base on dentistTool
    public void ShowInfo()
    {
        toolInfoGameObject.SetActive(true);

        toolInfoImage.sprite = dentistTool.Icon;
        toolInfoName.text = dentistTool.Name;
        toolInfoContent.text = dentistTool.Usage + "\n" + dentistTool.InstrumentGrasp + "\n" + dentistTool.Instrumentation;

    }
}
