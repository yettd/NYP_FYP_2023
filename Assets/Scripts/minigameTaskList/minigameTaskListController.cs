using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class minigameTaskListController : MonoBehaviour
{
    private enum Steps
    {
        //scaling
        LOCATINGS,
        CHOOSINGS,
        SCRAPINGS,
        END_TASKS,
        //extraion

        LOCATINGE,
        CHOOSINGE,
        SCRAPINGE,
        END_TASKE,
        //filling

        LOCATINGF,
        CHOOSINGF,
        SCRAPINGF,
        END_TASKF


    }
    public static minigameTaskListController Instance;

    private Steps currentStep;
    private Steps NextSteps;

    public Procedure procedure;
    private void Awake()
    {

        if(Instance)
        {
            Instance = this;
        }
        startminigame();
    }





    public bool gonext()
    {
        if (DoneWithMiniGame())
        {
            Debug.Log("Done ");
            return true;
        }
        currentStep=NextSteps;
        NextSteps++;
       // Debug.Log($"{currentStep} : {NextSteps}");
        return false;
    }

    public bool DoneWithMiniGame()
    {
        if (NextSteps.ToString().Contains("END"))
        {
            return true;
        }

        return false;
    }

    public void startminigame()
    {
        switch(procedure)
        {
            case Procedure.Scaling:
                currentStep = Steps.LOCATINGS;
                NextSteps = currentStep + 1;
              //  Debug.Log($"{currentStep} : {NextSteps}");
                    break;
        }
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            gonext();
        }
        if (Input.GetMouseButtonDown(1))
        {
            gonext();
        }
    }



}
//put task here



public enum Procedure
{
    Scaling,
    Filling,
    Extration
}
