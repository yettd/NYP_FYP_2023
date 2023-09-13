using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class minigameTaskListController : MonoBehaviour
{

  
    public static minigameTaskListController Instance;

    private Steps currentStep;
    private Steps NextSteps;
    private bool TBgums=false;
    public GameObject canvase;
    public Procedure procedure;
    [SerializeField] UnityEvent openGame;
    [SerializeField] UnityEvent closeGame;
    [SerializeField] UnityEvent close;
    [SerializeField] UnityEvent StopRotation;
    [SerializeField] UnityEvent ResumeRotation;
    public bool minigameOpen;

    bool toolSelected;
    private void Awake()
    {
        if (Instance==null)
        {
            Instance = this;
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
        openGame.Invoke();

        //open minimini gameWindow

        //load correct mininigame
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.V))
        {
            stopRotation();
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            RR();
        }
    }

    public void setGame(bool a)
    {
        TBgums = a;
        startminigame();
        minigameOpen = true;
        Debug.Log(TBgums);
    }
    public bool GetGumd()
    {
        return TBgums;
    }

    public void CloseGameOrBack()
    {
        if(toolSelected)
        {
            RR();
        }
        else if(minigameOpen)
        {

            closeGame.Invoke();
            minigameOpen = false;
        }
        else
        {
            close.Invoke();
        }
    }

    public void stopRotation()
    {
        StopRotation.Invoke();
        toolSelected = true;
    }    
    public void RR()
    {
        toolSelected = false;   
        ResumeRotation.Invoke();
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