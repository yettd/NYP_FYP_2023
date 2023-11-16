using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class minigameTaskListController : MonoBehaviour
{
    private AudioManager audioManager;
    public GameObject setting;
    [SerializeField] private GameObject SettingsPanel;
    bool SettingsPanelactive;
    [SerializeField]GameObject wrong;
    public void wrongTool()
    {
        wrong.SetActive(true);
    }
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
    float timerForMin;
    float timerForTenth;
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

    Vector4 ogTimerAnchor;

    Vector4 ogTeethAmtAnchor;


    //clock and teethUI


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
                procedure = Procedure.Scaling;
                break;
            case 1:
                procedure = Procedure.Extration;
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
        if(SceneManager.GetActiveScene().name== "Tutorial_Extraction")
        {
            teethMan.tm.dis();
        }
        timerText= GameObject.Find("timer").GetComponent<TextMeshProUGUI>();
        timerText.text = "0 : 00";
        StartCoroutine("increaseTimer");
        Almagotrrr = GameObject.Find("Almagotrrr");
        Almagotrrr.SetActive(false);
        //load steps
        string a = Saving.save.LoadFromJson("timeandRating");
        if (a!=null)
        {
            GC = JsonUtility.FromJson<GameCompletion>(Saving.save.LoadFromJson("timeandRating"));
         
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
        if(timerForGame > 9)
        {
            timerForTenth++;
            if(timerForTenth>=6)
            {
                timerForMin++;
                timerForTenth = 0;
            }
            timerForGame = 0;
        }
        timerText.text =$"{timerForMin} : {timerForTenth}{timerForGame.ToString()}";
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
        Saving.save.saveToJson(GC, "timeandRating");
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
        setting.SetActive(false);


        RectTransform rectTransform = AmtProblem.transform.parent.GetComponent<RectTransform>();
        ogTeethAmtAnchor = new Vector4(rectTransform.anchorMin.x, rectTransform.anchorMin.y, rectTransform.anchorMax.x, rectTransform.anchorMax.y);
        rectTransform.DOAnchorMin(new Vector2(0.7361111f, 0.78f), 0.001f);
        rectTransform.DOAnchorMax(new Vector2(0.837889f, 0.9841976f), 0.001f);
        rectTransform.DOAnchorPos(new Vector2(0, 0f), 0.001f);

        rectTransform = timerText.transform.parent.GetComponent<RectTransform>();
        ogTimerAnchor = new Vector4(rectTransform.anchorMin.x, rectTransform.anchorMin.y, rectTransform.anchorMax.x, rectTransform.anchorMax.y);
        rectTransform.DOAnchorMin(new Vector2(0.4088889f, 0.809f), 0.001f);
        rectTransform.DOAnchorMax(new Vector2(0.5888889f, 1), 0.001f);

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

    public string saveTimeAndRating()
    {
        return $"{timerForMin} : {timerForTenth}{timerForGame}";
    }

    public bool returnTimeInSec(float highscore)
    {
        float second = timerForMin *60 + (timerForTenth*10 + timerForGame );

        if (second < highscore || highscore == -1 ) {
            switch (procedure)
            {
                case Procedure.Scaling:
                    GC.scalingInSecond = second;
                    break;

                case Procedure.Filling:
                    GC.FillingInSecond = second;
                    break;
                case Procedure.Extration:
                    GC.ExtrationInSecond = second;
                    break;
            }
            return true;
        }


        return false;
    }

    public void ScoreSystem()
    {
        switch (procedure)
        {
            case Procedure.Scaling:
                if (returnTimeInSec(GC.scalingInSecond))
                {
                    if(timerForMin<5)
                    {
                        GC.scalingRating = 3;
                    }
                    else if (timerForMin < 10)
                    {
                        GC.scalingRating = 2;
                    }
                    else if (timerForMin < 15)
                    {
                        GC.scalingRating = 1;
                    }
                    else
                    {
                        GC.scalingRating = 0;
                    }
                    GetComponent<ScoreDisplay>().DisplayScore(GC.scalingRating);
                    GC.scalingTime = saveTimeAndRating();
                }
                else
                {
                    if (timerForMin < 5)
                    {
                        GetComponent<ScoreDisplay>().DisplayScore(3);
                    }
                    else if (timerForMin < 10)
                    {
                        GetComponent<ScoreDisplay>().DisplayScore(2);
                    }
                    else if (timerForMin < 15)
                    {
                        GetComponent<ScoreDisplay>().DisplayScore(1);
                    }
                    else
                    {
                        GetComponent<ScoreDisplay>().DisplayScore(0);
                    }
                }
                break;

            case Procedure.Filling:
                if (returnTimeInSec(GC.FillingInSecond))
                {
                    if (timerForMin < 5)
                    {
                        GC.FillingRating = 3;
                    }
                    else if (timerForMin < 10)
                    {
                        GC.FillingRating = 2;
                    }
                    else if (timerForMin < 15)
                    {
                        GC.FillingRating = 1;
                    }
                    else
                    {
                        GC.FillingRating = 0;
                    }
                    GetComponent<ScoreDisplay>().DisplayScore(GC.FillingRating);
                    GC.FillingTime = saveTimeAndRating();
                }
                else
                {
                    if (timerForMin < 5)
                    {
                        GetComponent<ScoreDisplay>().DisplayScore(3);
                    }
                    else if (timerForMin < 10)
                    {
                        GetComponent<ScoreDisplay>().DisplayScore(2);
                    }
                    else if (timerForMin < 15)
                    {
                        GetComponent<ScoreDisplay>().DisplayScore(1);
                    }
                    else
                    {
                        GetComponent<ScoreDisplay>().DisplayScore(0);
                    }
                }
                break;
            case Procedure.Extration:
                if (returnTimeInSec(GC.ExtrationInSecond))
                {
                    if (timerForMin < 5)
                    {
                        GC.ExtrationRating = 3;
                    }
                    else if (timerForMin < 10)
                    {
                        GC.ExtrationRating = 2;
                    }
                    else if (timerForMin < 15)
                    {
                        GC.ExtrationRating = 1;
                    }
                    else
                    {
                        GC.ExtrationRating = 0;
                    }
                    GetComponent<ScoreDisplay>().DisplayScore(GC.ExtrationRating);
                    GC.ExtrationTime = saveTimeAndRating();
                }
                else
                {
                    if (timerForMin < 5)
                    {
                        GetComponent<ScoreDisplay>().DisplayScore(3);
                    }
                    else if (timerForMin < 10)
                    {
                        GetComponent<ScoreDisplay>().DisplayScore(2);
                    }
                    else if (timerForMin < 15)
                    {
                        GetComponent<ScoreDisplay>().DisplayScore(1);
                    }
                    else
                    {
                        GetComponent<ScoreDisplay>().DisplayScore(0);
                    }
                }
                break;
        }
    }

    public void OnGameComplete()
    {
        //playAnimation for completion;
        StopAllCoroutines();

        if (timerForMin < 10)
        {
            switch(procedure)
            {
                case Procedure.Scaling:
                    achivmen.instance.UnlockAchivement(0,"scalingInMin");
                    break;

                case Procedure.Filling:
                    achivmen.instance.UnlockAchivement(1, "scalingInMin");
                    break;
                case Procedure.Extration:
                    achivmen.instance.UnlockAchivement(2, "scalingInMin");
                    break;
            }
        }
        ScoreSystem();



        if(achivmen.instance.GetIfUnlock(0) && achivmen.instance.GetIfUnlock(1) )
        {
            achivmen.instance.UnlockAchivement(3);
        }


        saveGameComplation(procedure);
        WIN.Invoke();
        audioManager.PlaySFX(13);
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
            teethMan.tm.changeToolColor("");
            teethMan.tm.backText( true);
        }
        else if(cameraChanger.Instance.GetZoom()==true)
        {
            TL.GetComponent<RectTransform>().DOAnchorPosX(-174f, 1f);
            toolSelection.GetComponent<RectTransform>().DOAnchorPosX(192, 1f);
            teethMan.tm.Back();
            cameraChanger.Instance.ZoomOutCam();
            timerText.transform.parent.transform.GetComponent<RectTransform>().DOAnchorMin(new Vector2(ogTimerAnchor.x, ogTimerAnchor.y), 0.01f);
            timerText.transform.parent.transform.GetComponent<RectTransform>().DOAnchorMax(new Vector2(ogTimerAnchor.z, ogTimerAnchor.w), 0.01f);
            timerText.transform.parent.transform.GetComponent<RectTransform>().DOAnchorPos(new Vector2(0, 0), 0.01f);

            AmtProblem.transform.parent.transform.GetComponent<RectTransform>().DOAnchorMin(new Vector2(ogTeethAmtAnchor.x, ogTeethAmtAnchor.y), 0.01f);
            AmtProblem.transform.parent.transform.GetComponent<RectTransform>().DOAnchorMax(new Vector2(ogTeethAmtAnchor.z, ogTeethAmtAnchor.w), 0.01f);
            AmtProblem.transform.parent.transform.GetComponent<RectTransform>().DOAnchorPos(new Vector2(0, 0), 0.01f);

            setting.SetActive(true);
            teethMan.tm.CT("Swipe to rotate \n Click on a teeth to zoom in further", false);
        }
        else
        {
            if(minigameOpen)
            {
                closeGame.Invoke();
                minigameOpen = false;

                teethMan.tm.CT("Click on either gum to zoom in", false);
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
                teethMan.tm.ct("tap the GIC capsule 3 times", true);
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
            teethMan.tm.ct($"{toolsname}\n Click the handle and drag to use it \n Click on the back button to deselect and rotate camera again", true);
          
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

        TL.GetComponent<RectTransform>().DOAnchorPosX(129.56f,1f);
        toolSelection.GetComponent<RectTransform>().DOAnchorPosX(-150, 1f);
       
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
            //slot.AddComponent<Button>().onClick.AddListener(() =>
            //{
            //    //if (instrution.transform.parent==slot.gameObject.transform)
            //    //{
            //    //    instrution.transform.parent = null;
            //    //    instrution.SetActive(false);
            //    //    return;
            //    //}
            //    //instrution.SetActive(true);
            //    //instrution.transform.parent=slot.transform;
            //    //instrution.transform.localPosition = new Vector3(300, 0, 0);
            //    //instrution.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = TBD.Des;
            //});
            
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

    public string returnCurrentstep()
    {
        foreach (TaskBreakDown TBD in st.TBD)
        {
            if (TBD.s == currentStep)
            {
                return TBD.Des;
            }
        }
        return null;
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

