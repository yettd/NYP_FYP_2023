using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DW_TaskAddons : MonoBehaviour
{
    private InstructionManual manual;
    public GameObject taskPanel;
    private showTask task;

    #region SETUP
    public void Setup(showTask task)
    {
        // Send manual for checking
        manual = TutorialNagivatorScript.Instance().get_manual;
        for (int i = 0; i < manual.step.Length; i++) manual.step[i].taskDone = false;

        // Able to read out all step
        this.task = task;
    }
    #endregion

    #region COMPONENT
    private void SetInstructionCompletion(int step, bool condition)
    {
        // Check whichever step are available
        manual.step[step].taskDone = condition;
    }

    private void RefreshTaskPanel()
    {
        // Clear them out
        ClearTaskList();

        // Create and update completed and on-going step
        CreateTaskList();
    }
    #endregion

    #region MAIN
    public void GetStepClearance(string toolUsed)
    {
        // Store the number of step
        int stepNo = 0;

        // Search the step array
        foreach (InstructionTemplate step in manual.step)
        {
            // Use of required tool and the latest task check to be done
            if (step.requiredTool == toolUsed && !step.taskDone)
            {
                // Make a check on the step
                SetInstructionCompletion(stepNo, true);
                break;
            }

            // Count the number of step
            stepNo++;
        }

        // Refresh browser of the step content
        RefreshTaskPanel();
    }
    #endregion

    #region MISC
    private void CreateTaskList()
    {
        int length = task.TBD.Length;

        for (int i = 0; i < length; i++)
        {
            Image entry = Instantiate(Resources.Load<Image>("minigameTasklist/Image"), taskPanel.transform);
            entry.GetComponent<Image>().sprite = task.TBD[i].i;

            entry.GetComponent<Image>().color = manual.step[i].taskDone ? new Color(0.3f, 0.3f, 0.3f) : Color.white;
        }
    }

    private void ClearTaskList()
    {
        for (int i = 0; i < taskPanel.transform.childCount; i++)
            GameObject.Destroy(taskPanel.transform.GetChild(i).gameObject);
    }
    #endregion
}
