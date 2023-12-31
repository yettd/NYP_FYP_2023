using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;
public class showCurrentStep : MonoBehaviour
{
    [SerializeField]TextMeshProUGUI showInstrution;
    [SerializeField] public Image img;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void displayCurrentStep()
    {
        img.color = new Vector4(1, 1, 1, 1);
        showInstrution.color = new Vector4(0, 0, 0, 1);
        showInstrution.text = TutorialNagivatorScript.Instance().get_manual.toolAccessId != 1 ? minigameTaskListController.Instance.returnCurrentstep() : TutorialGame_Script.thisScript.getTasking.GetStepDetail();
        DOVirtual.Color(new Vector4(1, 1, 1, 1), new Vector4(1, 1, 1, 0), 10, (value) =>
        {
            showInstrution.color = new Vector4(0, 0, 0, value.a);
            img.color = value;
        }).OnComplete(() =>
        {

        });
    }
}
