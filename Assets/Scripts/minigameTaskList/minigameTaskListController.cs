using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;
public class minigameTaskListController : MonoBehaviour
{

    [SerializeField] private GameObject SettingsPanel;
    bool SettingsPanelactive;
    public void openSettingsPanel()
    {
        AudioManager.Instance.PlayPingSound();
        //AudioPlayer.Instance.PlayAudioOneShot(0, .5f);
        if (SettingsPanelactive == false)
        {
            SettingsPanel.transform.gameObject.SetActive(true);
            SettingsPanelactive = true;
        }
        else
        {
            SettingsPanel.transform.gameObject.SetActive(false);
            SettingsPanelactive = false;
        }
    }
    public void CloseSettingsPanel()
    {
        AudioManager.Instance.PlayPingSound();
        //AudioPlayer.Instance.PlayAudioOneShot(0, .5f);
        if (SettingsPanelactive == true)
        {
            SettingsPanel.transform.gameObject.SetActive(false);
            SettingsPanelactive = false;
        }
    }

    public static minigameTaskListController Instance;

    public  Steps currentStep;
    private int CS;
    private Steps NextSteps;
    private int NS;
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

    //tool step and Selection
    [SerializeField]GameObject toolSelection;
    GameObject toolStep;


    //text
    [SerializeField]TextMeshProUGUI AmtProblem;
    private void Awake()
    {
        if (Instance==null)
        {
            Instance = this;
        }
        switch (TutorialNagivatorScript.Instance().get_manual.toolAccessId)
        {
            case 0:
                procedure = Procedure.Scaling;
                break;
            case 2:
                procedure = Procedure.Filling;
                break;

            default: // Any instruction level
                break;
        }
    }
    private void Start()
    {
        //load procedure

  


        //load steps
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

        CS = NS;
        currentStep = st.TBD[CS].s;
        showCorrectStep();
        NS++;
        if (NS >= st.TBD.Length)
        {
            CheckGameComplete();
            return true;
        }

  
        //pauseButton.ChangeButtonSprite();
        // Debug.Log($"{currentStep} : {NextSteps}");
        return false;
    }

  
    bool NoMorePrevStep()
    {
        //if(prevStep==Steps.LOCATINGE|| prevStep == Steps.LOCATINGF|| prevStep == Steps.LOCATINGS)
        //{
        //    return true;
        //}
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

    public void startminigame(showTask st, int DidAStep)
    {

        this.st = st;
        CS = DidAStep;
        Debug.Log(CS);
        currentStep = st.TBD[CS].s;
        NS = CS + 1;
        if(NS<st.TBD.Length)
        {

        NextSteps = st.TBD[NS].s;
        }



        SetUpTaskList();
        showCorrectStep();
        //switch(procedure)
        //{
        //    case Procedure.Scaling:
        //        currentStep = Steps.LOCATINGS;
        //        NextSteps = currentStep + 1;
        //      //  Debug.Log($"{currentStep} : {NextSteps}");
        //            break;
        //    case Procedure.Extration:
        //        currentStep = Steps.LOCATINGE;
        //        NextSteps = currentStep + 1;
        //        //  Debug.Log($"{currentStep} : {NextSteps}");
        //        break;
        //    case Procedure.Filling:
        //        currentStep = Steps.LOCATINGF;
        //        NextSteps = currentStep + 1;
        //        //  Debug.Log($"{currentStep} : {NextSteps}");
        //        break;
        //}

        //open minimini gameWindow

        //load correct mininigame
    }

    private void Update()
    {

    }

    public void IncreaseTeethWithProblem()
    {
        problemTeeth++;
        AmtProblem.text = $"0/{problemTeeth}";
    }


    public void setGame(bool a)
    {
        TBgums = a;
        minigameOpen = true;
        pauseButton.ChangeButtonSprite();
        //cameraChanger.Instance.startCamera();
        openGame.Invoke();
    }
    public bool GetGumd()
    {
        return TBgums;
    }

    //show teethOnly
    //private GameObject teeth;
    //public void SetTeetch(GameObject t)
    //{
    //    teeth = t.transform.parent.gameObject;
       
    //    minigameOpen = true;
    //    cameraChanger.Instance.startCamera();
    //    openGame.Invoke();
    //}

    //public GameObject getTeetch()
    //{
    //    return teeth;
    //}

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
        AmtProblem.text = $"{solvedTeetg }/{problemTeeth}";
        if (solvedTeetg>=problemTeeth)
        {
            OnGameComplete();
        }
    }

    public void CloseGameOrBack()
    {
        if(toolSelected ==true)
        {
            RR();
        }
        else if(cameraChanger.Instance.GetZoom()==true)
        {
            TL.gameObject.SetActive(false);
            toolSelection.SetActive(false);
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

        pauseButton.ChangeButtonSprite();
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

        if (cameraChanger.Instance.GetZoom())
        {
            // testTool.gameObject.SetActive(true);
            toolSelectedName = toolsname;
            this.model = Instantiate(model) as GameObject;
            this.model.transform.position = cameraChanger.Instance.GetCurrentCam().gameObject.transform.position + cameraChanger.Instance.GetCurrentCam().gameObject.transform.forward;
            this.model.transform.rotation = cameraChanger.Instance.GetCurrentCam().gameObject.transform.rotation;
            this.model.transform.parent = canvase.gameObject.transform.GetChild(0).transform;
            this.model.transform.localScale = new Vector3(5, 5, 5);
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

        TL.gameObject.SetActive(true);
        toolSelection.SetActive(true);
        //switch (procedure)
        //{
        //    case Procedure.Scaling:
        //        st = Resources.Load<showTask>("minigameTasklist/scaling");
        //        //  Debug.Log($"{currentStep} : {NextSteps}");
        //        break;
        //    case Procedure.Extration:
        //        currentStep = Steps.LOCATINGE;
        //        NextSteps = currentStep + 1;
        //        //  Debug.Log($"{currentStep} : {NextSteps}");
        //        break;
        //    case Procedure.Filling:
        //        currentStep = Steps.LOCATINGF;
        //        NextSteps = currentStep + 1;
        //        //  Debug.Log($"{currentStep} : {NextSteps}");
        //        break;
        //}
        foreach (Transform child in TL)
        {
            // Destroy the child object
            Destroy(child.gameObject);
        }
        ImageOfTask.Clear();
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

    public int  getCSValue()
    {
        return CS;
    }
}
//put task here



public enum Procedure
{
    Scaling,
    Filling,
    Extration
}

