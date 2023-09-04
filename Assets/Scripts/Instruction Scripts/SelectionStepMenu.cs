using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectionStepMenu : MonoBehaviour
{
    [SerializeField] private GameObject selectionTab;
    private InstructionManual manual;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("ReadManual", 0.5f);
    }

    #region SETUP
    private void ReadManual()
    {
        manual = GetComponent<InstructionMenu>().get_manual;
        GenerateInstructionStep();
    }

    private RawImage GetStepImage(string pathName)
    {
        RawImage obj = Resources.Load<RawImage>("InstructionManual/" + pathName);
        RawImage img = Instantiate(obj, transform.position, Quaternion.identity);
        return img;
    }
    #endregion

    #region COMPONENT
    private void OnGoingStep(string stepTitle, Texture icon)
    {
        RawImage img = GetStepImage("entry");
        img.texture = icon;
        img.GetComponent<StepInstructionComponent>().SetScenePath(stepTitle);
        img.transform.SetParent(selectionTab.transform);
    }

    private void NextStep()
    {
        RawImage img = GetStepImage("next");
        img.transform.SetParent(selectionTab.transform);
    }

    private void FinishStep()
    {
        RawImage img = GetStepImage("finish");
        img.transform.SetParent(selectionTab.transform);
    }
    #endregion

    #region MAIN
    public void GenerateInstructionStep()
    {
        if (manual.step.Length > 1) // Get multiple instruction step
        {
            foreach (InstructionTemplate i in manual.step)
            {
                OnGoingStep(i.data, i.Icon);
                NextStep();
            }

            FinishStep();
        }
        else if (manual.step.Length != 0) // Get single instruction step
        {
            OnGoingStep(manual.step[0].data, manual.step[0].Icon);
            FinishStep();
        }
    }
    #endregion
}
