using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
public class minigameTaskListController : MonoBehaviour
{

  
    public static minigameTaskListController Instance;

    public  Steps currentStep;
    private Steps NextSteps;
    private Steps prevStep;
    private bool TBgums=false;
    public GameObject canvase;
    private GameObject model;
    public Procedure procedure;
    [SerializeField] UnityEvent openGame;
    [SerializeField] UnityEvent closeGame;
    [SerializeField] UnityEvent close;
    [SerializeField] UnityEvent StopRotation;
    [SerializeField] UnityEvent ResumeRotation;
    [SerializeField] UnityEvent WIN;
    public bool minigameOpen;
    [SerializeField] GameObject pause;
    [SerializeField] changePasueToBack pauseButton;
    bool toolSelected;


    [SerializeField]float problemTeeth;
    [SerializeField] float solvedTeetg;
    public bool IsPause;
    showTask st;

    //sideStuff
    public GameObject goodJob;

    GameCompletion GC;
    Saving s;
    private string toolSelectedName="";
    private void Awake()
    {
        if (Instance==null)
        {
            Instance = this;
        }

    }
    private void Start()
    {
        //load procedure

        //load steps
        SetUpTaskList();
        string a = Saving.save.LoadFromJson("game");
        if (a!=null)
        {
            GC = JsonUtility.FromJson<GameCompletion>(Saving.save.LoadFromJson("game"));
        }
        else
        {
            GC = new GameCompletion();
           
        }
        
    }

    private void saveGameComplation(Procedure pro)
    {
        switch(pro)
        {
            case Procedure.Scaling:
                GC.setGC(0);
                break;
            case Procedure.Filling:
                GC.setGC(1);
                break;
            case Procedure.Extration:
                GC.setGC(2);
                break;
        }
        Saving.save.saveToJson(GC, "game");
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
        showCorrectStep();
        pauseButton.ChangeButtonSprite();
        // Debug.Log($"{currentStep} : {NextSteps}");
        return false;
    }

    public bool goprev()
    {
        NextSteps = currentStep;
        currentStep = prevStep;
        if(NoMorePrevStep()==false)
        {
        prevStep--;

        }
        showCorrectStep();
        pauseButton.ChangeButtonSprite();
        return false;
    }

    bool NoMorePrevStep()
    {
        if(prevStep==Steps.LOCATINGE|| prevStep == Steps.LOCATINGF|| prevStep == Steps.LOCATINGS)
        {
            return true;
        }
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
    }

    public void IncreaseTeethWithProblem()
    {
        problemTeeth++;
    }


    public void setGame(bool a)
    {
        TBgums = a;
        startminigame();
        minigameOpen = true;
        cameraChanger.Instance.startCamera();
        openGame.Invoke();
    }
    public bool GetGumd()
    {
        return TBgums;
    }

    //show teethOnly
    private GameObject teeth;
    public void SetTeetch(GameObject t)
    {
        teeth = t.transform.parent.gameObject;
        startminigame();
        minigameOpen = true;
        gonext();
        cameraChanger.Instance.startCamera();
        openGame.Invoke();
    }

    public GameObject getTeetch()
    {
        return teeth;
    }

    //end

    public void OnGameComplete()
    {
        //playAnimation for completion;
        saveGameComplation(procedure);
        WIN.Invoke();
    }

    public void CheckGameComplete()
    {
        solvedTeetg++;
        if(solvedTeetg>=problemTeeth)
        {
            Debug.LogError("NO WIN");
            OnGameComplete();
        }
    }

    public void CloseGameOrBack()
    {
        if(currentStep==Steps.SCRAPINGS )
        {
            RR();
        }
        else if(currentStep == Steps.CHOOSINGS)
        {

                teethMan.tm.Back();
                cameraChanger.Instance.ZoomOutCam();
        }
        else
        {
            if(minigameOpen)
            {
                closeGame.Invoke();
                minigameOpen = false;
            }
            else
            {

                cameraChanger.Instance.closeCamera();
                close.Invoke();
                IsPause = true;
                return;
            }
       
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
        Destroy(model);
        toolSelected = false; 
        ResumeRotation.Invoke();
    }
    public void ResumeGame()
    {
        pause.SetActive(false);
        IsPause = false;
    }

    public void ToolsSelected(string toolsname, GameObject model)
    {

        if (currentStep == Steps.CHOOSINGS)
        {
            // testTool.gameObject.SetActive(true);
            toolSelectedName = toolsname;
            this.model = Instantiate(model) as GameObject;
            this.model.transform.position = cameraChanger.Instance.GetCurrentCam().gameObject.transform.position + cameraChanger.Instance.GetCurrentCam().gameObject.transform.forward;
            this.model.transform.rotation = cameraChanger.Instance.GetCurrentCam().gameObject.transform.rotation;
            this.model.transform.parent = canvase.gameObject.transform.GetChild(0).transform;
            this.model.transform.localScale = new Vector3(3, 3, 3);
            Debug.LogError(toolsname);
            gonext();
            stopRotation();
        }

    }

    public string GetSelectedtool()
    {
        return toolSelectedName;
    }

    public Transform TL;
    List<Image> ImageOfTask = new List<Image>();
    private void SetUpTaskList()
    {
     
        switch (procedure)
        {
            case Procedure.Scaling:
                st = Resources.Load<showTask>("minigameTasklist/scaling");
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
        bool first = true;
        foreach(TaskBreakDown TBD in st.TBD)
        {
            Image slot = Instantiate(Resources.Load<Image>("minigameTasklist/Image"),TL);
            ImageOfTask.Add(slot);
            if(first)
            {
                slot.sprite = TBD.i;
                first = false;
            }
            
        }

    }
    private void showCorrectStep()
    {
        int i= 0;
        foreach (TaskBreakDown TBD in st.TBD)
        {
            if(TBD.s==currentStep)
            {
                ImageOfTask[i].sprite = TBD.i;
            }
            else
            {
                ImageOfTask[i].sprite = null;
            }
            i++;
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