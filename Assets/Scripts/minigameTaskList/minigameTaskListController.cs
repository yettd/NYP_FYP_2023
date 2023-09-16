using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class minigameTaskListController : MonoBehaviour
{

  
    public static minigameTaskListController Instance;

    public  Steps currentStep;
    private Steps NextSteps;
    private Steps prevStep;
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

    [SerializeField]GameObject testTool;
    private string toolSelectedName="";
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
        prevStep = currentStep;
        currentStep=NextSteps;
        NextSteps++;
       // Debug.Log($"{currentStep} : {NextSteps}");
        return false;
    }

    public bool goprev()
    {
        NextSteps = currentStep;
        currentStep = prevStep;
        prevStep--;

        return false;
    }

    public Steps getCurrentStep()
    {
        return currentStep;
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
        gonext();
        openGame.Invoke();
    }
    public bool GetGumd()
    {
        return TBgums;
    }

    public void CloseGameOrBack()
    {
        if(currentStep==Steps.SCRAPINGS )
        {
            RR();
        }
        else if(currentStep == Steps.CHOOSINGS)
        {

            closeGame.Invoke();
            minigameOpen = false;
        }
        else
        {
            close.Invoke();
        }
        goprev();
    }

    public void stopRotation()
    {
        StopRotation.Invoke();
        toolSelected = true;
    }    
    public void RR()
    {
        testTool.gameObject.SetActive(false);
        toolSelected = false;   
        ResumeRotation.Invoke();
    }

    public void ToolsSelected(string toolsname)
    {
        if(currentStep==Steps.CHOOSINGS)
        {

            testTool.gameObject.SetActive(true);
            toolSelectedName = toolsname;
            Debug.LogError(toolsname);
            gonext();
            stopRotation();
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