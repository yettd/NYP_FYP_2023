using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DW_ActionListProgram
{
    private string actionTitle;
    private List<float> taskLoad;

    private float progress = 0;
    private float maxProgress = 0;

    private float NextProgressTime = 0;

    public DW_ActionListProgram()
    {
        // List all task of time given by this array
        taskLoad = new List<float>();
    }

    #region SETUP
    private float DoProgress()
    {
        // Calcuate by the percentage of the total time
        float percentage = 100 / maxProgress;
        return Mathf.Clamp(percentage * progress + percentage, 0, 100);
    }
    #endregion

    #region MAIN
    public void IdentifyActionType(string title)
    {
        // Fill in detail of action
        actionTitle = title;

        // Get the total time needs to complete the task
        foreach (float time in taskLoad) maxProgress += time;
    }

    public void AddTaskActionLoad(float time)
    {
        // Add a new timeline to get a task done
        taskLoad.Add(time);
    }

    public void StartProgram()
    {
        // Do the task need and countdown to its completed
        if (progress < maxProgress)
        {
            // Calculate new progress time upon every count interval
            if (Time.time >= NextProgressTime)
            {
                // Set a new count interval
                NextProgressTime = Time.time + 0.5f;

                // Make progress by the assign time
                progress+=0.5f;
            }

            // Update the content for user visual
            TutorialGame_Script.thisScript.AdvancementContent(actionTitle, DoProgress());
        }
        
        // It's completed then don't do any
        else { TutorialGame_Script.thisScript.AdvancementContent(string.Empty, -1); }
    }
    #endregion
}
