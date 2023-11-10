using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class minigameTaskListController : MonoBehaviour
{
    private AudioManager audioManager;

    [SerializeField] private GameObject SettingsPanel;
    bool SettingsPanelactive;
    public void openSettingsPanel()
    {
        //AudioManager.Instance.PlayPingSound();
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
        //AudioManager.Instance.PlayPingSound();
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
    public bool TBgums=false;
    public GameObject canvase;
    private GameObject model=null;
    public Procedure procedure;
    [SerializeField] UnityEvent openGame;
    [SerializeField] UnityEvent closeGame;
    [SerializeField] UnityEvent close;
    [SerializeField] UnityEvent StopRotation;
    [SerializeField] UnityEvent ResumeRotation;
    [SerializeField] UnityEvent WIN;
    float timerForGame;
    [SerializeField] UnityEvent GIC;
    [SerializeField] UnityEvent GIClose;
    public bool minigameOpen;
    TextMeshProUGUI NameForttOOL;
    [SerializeField] GameObject pause;
    [SerializeField]List<GameObject> jaw = new List<GameObject>();
    [SerializeField] changePasueToBack pauseButton;
    bool toolSelected;

    public GameObject instrution;

    TextMeshProUGUI timerText;

    [SerializeField]float problemTeeth;
    [SerializeField] float solvedTeetg;
    GameObject insturitionParent;
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
    public bool gic=false;
    GameObject Almagotrrr;
   //text
   [SerializeField]TextMeshProUGUI AmtProblem;
    private void Awake()
    {
        audioManager = AudioManager.Instance;

        if (audioManager == null)
        {
            Debug.LogError("AudioManager not found.");
        }
        if (Instance==null)
        {
            Instance = this;
        }
        switch (TutorialNagivatorScript.Instance().get_manual.toolAccessId)
        {
            case 0:
                case 1:
                procedure = Procedure.Scaling;
                break;
            case 2:
                procedure = Procedure.Filling;
                //jaw[0].SetActive(false);
                //jaw[1].SetActive(true);
                break;

            default: // Any instruction level
                break;
        }
    }
    private void Start()
    {
        timerForGame = 0;
        timerText= GameObject.Find("timer").GetComponent<TextMeshProUGUI>();
        timerText.text = "0";
        StartCoroutine("increaseTimer");
        Almagotrrr = GameObject.Find("Almagotrrr");
        Almagotrrr.SetActive(false);
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
    IEnumerator increaseTimer()
    {
        yield return new WaitForSeconds(1);
        timerForGame++;
        timerText.text =timerForGame.ToString();
        StartCoroutine("increaseTimer");
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
        audioManager.PlaySFX(5);
        CS = NS;
        currentStep = st.TBD[CS].s;
        showCorrectStep();
        NS++;
        if (NS >= st.TBD.Length)
        {
            CheckGameComplete();
            NS--;
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
        currentStep = st.TBD[CS].s;
        NS = CS + 1;
        if(NS<st.TBD.Length)
        {

            NextSteps = st.TBD[NS].s;
        }

        SetUpTaskList();
        showCorrectStep();
   
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
        StopAllCoroutines();

        if(timerForGame < 60)
        {
            switch(procedure)
            {
                case Procedure.Scaling:
                    achivmen.instance.UnlockAchivement(0,"scalingInMin");
                    break;

                case Procedure.Filling:
                    achivmen.instance.UnlockAchivement(1, "scalingInMin");
                    break;
            }
        }
        if(achivmen.instance.GetIfUnlock(0) && achivmen.instance.GetIfUnlock(1) )
        {
            Debug.Log("asdasd");
            achivmen.instance.UnlockAchivement(3);
        }


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

            teethMan.tm.backText( true);
        }
        else if(cameraChanger.Instance.GetZoom()==true)
        {
            TL.gameObject.SetActive(false);
            toolSelection.SetActive(false);
            teethMan.tm.Back();
            cameraChanger.Instance.ZoomOutCam();

            teethMan.tm.CT("Swipe to rotate \n Click on teeth to zoom in further", false);
        }
        else
        {
            if(minigameOpen)
            {
                closeGame.Invoke();
                minigameOpen = false;

                teethMan.tm.CT("Click on which gum to zoom in", false);
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
        GIClose.Invoke();
        ResumeRotation.Invoke();
    }
    public void ResumeGame()
    {
        pause.SetActive(false);
        IsPause = false;
    }

    public void ToolsSelected(string toolsname, GameObject model)
    {
    
        if (this.model != null)
        {
            toolSelected = false;
            Destroy(this.model);
            this.model=null;
        }
        stopRotation(); 
        if (cameraChanger.Instance.GetZoom() )
        {
            if(!gic && toolsname== "Applicator")
            {
                teethMan.tm.ct("tap the GIC capsule 3 time", true);
                GIC.Invoke();
                return;
            }
            if(Almagotrrr.activeSelf)
            {
                GIClose.Invoke();
            }

            // testTool.gameObject.SetActive(true);
            toolSelectedName = toolsname;
            this.model = Instantiate(model) as GameObject;
            this.model.transform.position = cameraChanger.Instance.GetCurrentCam().gameObject.transform.position + cameraChanger.Instance.GetCurrentCam().gameObject.transform.forward;
            this.model.transform.rotation = cameraChanger.Instance.GetCurrentCam().gameObject.transform.rotation;
            this.model.transform.parent = canvase.gameObject.transform.GetChild(0).transform;
            this.model.transform.localScale = new Vector3(5, 5, 5);
            teethMan.tm.ct($"{toolsname}\n Click and drag the handle to use it \n Click back deselet and to rotate camera again", true);
          
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
        foreach (Transform child in TL.GetChild(0).transform.GetChild(0))
        {
            // Destroy the child object
            Destroy(child.gameObject);
        }
        ImageOfTask.Clear();
        bool first = true;
        foreach(TaskBreakDown TBD in st.TBD)
        {
            Image slot = Instantiate(Resources.Load<Image>("minigameTasklist/Image"),TL.GetChild(0).transform.GetChild(0));
            ImageOfTask.Add(slot);
            if(first)
            {
                slot.sprite = TBD.i;
                first = false;
            }
            slot.AddComponent<Button>().onClick.AddListener(() =>
            {
                if (instrution.transform.parent==slot.gameObject.transform)
                {
                    instrution.transform.parent = null;
                    instrution.SetActive(false);
                    return;
                }
                instrution.SetActive(true);
                instrution.transform.parent=slot.transform;
                instrution.transform.localPosition = new Vector3(300, 0, 0);
                instrution.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = TBD.Des;
            });
            
        }
        int i = 0;
        foreach (TaskBreakDown TBD in st.TBD)
        {
            ImageOfTask[i].sprite = TBD.i;
            i++;
        }
    }
    private void showCorrectStep()
    {

        int i = 0;
        foreach (TaskBreakDown TBD in st.TBD)
        {
            if (TBD.s == currentStep)
            {
                ImageOfTask[i].color = new Vector4(1, 1, 1, 1f);
            }
            else
            {
                ImageOfTask[i].color = new Vector4(1,1,1,.5f);
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

