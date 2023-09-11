using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class minigameTaskListController : MonoBehaviour
{

  
    public static minigameTaskListController Instance;

    private Steps currentStep;
    private Steps NextSteps;
    private bool TBgums=false;
    public GameObject canvase;
    public Procedure procedure;
    private void Awake()
    {
        if (Instance==null)
        {
            Instance = this;
        Debug.Log(Instance);
        }

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
            case Procedure.Extration:
                currentStep = Steps.LOCATINGE;
                NextSteps = currentStep + 1;
                //  Debug.Log($"{currentStep} : {NextSteps}");
                break;
            case Procedure.Filling:
                currentStep = Steps.LOCATINGF;
                NextSteps = currentStep + 1;
                //  Debug.Log($"{currentStep} : {NextSteps}");
                break;
        }
        canvase.SetActive(true);

        //open minimini gameWindow

        //load correct mininigame
    }

    private void Update()
    {
   
    }

    public void setGame(bool a)
    {
        TBgums = a;
        Debug.Log("asdasdasd");
        startminigame();
    }
    public bool GetGumd()
    {
        return true;
    }



}
//put task here



public enum Procedure
{
    Scaling,
    Filling,
    Extration
}

public enum Steps
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