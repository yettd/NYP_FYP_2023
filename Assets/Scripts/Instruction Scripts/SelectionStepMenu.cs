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
        ReadManual();
    }

    #region SETUP
    private void ReadManual()
    {
        manual = TutorialNagivatorScript.thisScript.get_manual;
        GenerateInstructionStep();
    }

    private RawImage GetStepImage(string pathName)
    {
        RawImage obj = Resources.Load<RawImage>("InstructionManual/" + pathName);
        RawImage img = Instantiate(obj);
        return img;
    }
    #endregion

    #region COMPONENT
    private void OnGoingStep(int stepIndex, Texture icon)
    {
        RawImage img = GetStepImage("entry");
        img.GetComponent<StepInstructionComponent>().SetIcon(icon);
        img.GetComponent<StepInstructionComponent>().SetIndex(stepIndex + 1);
        img.GetComponent<StepInstructionComponent>().SetManualPath(manual.step[stepIndex]);
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
            for (int step = 0; step < manual.step.Length; step++)
            {
                OnGoingStep(step, manual.step[step].Icon);
                NextStep();
            }

            FinishStep();
        }
        else if (manual.step.Length != 0) // Get single instruction step
        {
            OnGoingStep(0, manual.step[0].Icon);
            NextStep();
            FinishStep();
        }
    }
    #endregion
}
