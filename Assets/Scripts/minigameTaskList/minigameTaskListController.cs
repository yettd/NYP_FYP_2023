using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class minigameTaskListController : MonoBehaviour
{
    public static minigameTaskListController Instance;

    private scaling currentScalingTask;
    private filling currentFillingTask;
    private extraction currentExtraionTask;

    public Procedure procedure;
    private void Awake()
    {
        currentScalingTask = scaling.LOCATING;
        currentFillingTask = filling.LOCATING;
        currentExtraionTask = extraction.LOCATING;
        if(Instance)
        {
            Instance = this;
        }    
    }

    public scaling GetCurrentScalingStep()
    {
        return currentScalingTask;
    }
    public filling GetCurrentFillingTaskStep()
    {
        return currentFillingTask;
    }
    public extraction GetCurrentScakingStep()
    {
        return currentExtraionTask;
    }

    public bool gonext()
    {
        
        switch(procedure)
        {
            case Procedure.Scaling:
                //see if next is complete
                currentScalingTask++;
                Debug.Log(currentScalingTask);
                //
                break;

            case Procedure.Extration:

                currentExtraionTask++;
                Debug.Log(currentScalingTask);
                break;
            case Procedure.Filling:
                break;
                
        }

        return false;
    }




}
public enum scaling
{
    LOCATING=1,
    CHOOSING=2,
    SCRAPING=3,
    END_TASK=4
}
public enum filling
{
    LOCATING,
    CHOOSING,
    SCRAPING,
    END_TASK
}

public enum extraction
{
    LOCATING=1,
    CHOOSING=2,
    SCRAPING=3,
    END_TASK
}

public enum Procedure
{
    Scaling,
    Filling,
    Extration
}
