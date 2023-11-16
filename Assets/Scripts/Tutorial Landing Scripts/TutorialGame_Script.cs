using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum GameTagPlacement
{ 
    DW_Camera,
    DW_Tool,
    TeethSection,
    GumSection,
    DamagedTooth,
    ToothPlacement,
    ToothExtracted,
    NotTagged,
}

public enum InterfaceFeedBack
{
    PerformingUsedTool,
    CancelledInQueueTool,
    CurrentlyInUsed,
    Idle
}

[System.Serializable]
public class GameSetup_Data
{
    public GameObject[] props;
    public string props_tag_name;
}

public class TutorialGame_Script : MonoBehaviour
{
    public static TutorialGame_Script thisScript;

    [SerializeField] private GameSetup_Data[] gameInfo;
    public GameSetup_Data[] get_GameInfo { get { return gameInfo; } }

    private DW_ToothExtraction extraction;
    private DW_ExtraTooth_Addons extraction_toothAddons;
    private DW_TaskAddons tasking;

    private DataManageScript data;
    private const string fileDirectory = "savefile_tutorial_";
    private const string fileDirectoryPath = "Assets/Resources/TutorialLevel/meta/";

    [SerializeField] private GameObject winScreen;
    [SerializeField] private GameObject loseScreen;
    [SerializeField] private GameObject statusRemark;
    [SerializeField] private GameObject actionStatus;

    private InterfaceFeedBack onGoingFeedback;
    public InterfaceFeedBack get_onGoingFeedback { get { return onGoingFeedback; } }

    public DW_TaskAddons getTasking { get { return tasking; } }

    private DW_AudioLibrary audiolibrary;
    public DW_AudioLibrary get_audiolibrary { get { return audiolibrary; } }

    void Start()
    {
        thisScript = this;

        // Build in task manage row
        tasking = GetComponent<DW_TaskAddons>();

        // Begin to find all gameobject which tagged correctly
        SetGamePropReady();

        // Begin on level and setup starting property
        SetInstructionObject();

        // Set the default feedback to idle
        onGoingFeedback = InterfaceFeedBack.Idle;
    }

    void Update()
    {
        // Progress the level
        LevelInProgress();
    }

    #region SETUP
    private void UpdateInstructionStatus(bool cleared)
    {
        // Display the win screen of it
        if (cleared) minigameTaskListController.Instance.OnGameComplete();

        // Display the lose screen of it
        loseScreen.SetActive(!cleared);

        // Play audio
        audiolibrary.PlayAudioWhenLose(!cleared);
    }

    private void UpdateInstructionActionStatus(string title, bool center)
    {
        // Reset the visible status
        CancelInvoke("SetVisibleActionStatus");

        // Update the content to the given title
        teethMan.tm.CT(title, center);

        // Display until its not acitve
        Invoke("SetVisibleActionStatus", 3);
    }

    private void SetVisibleActionStatus()
    {
        // Get the interface not visible
        actionStatus.SetActive(false);
        onGoingFeedback = InterfaceFeedBack.Idle;
    }

    private void SaveProgressForThisSession(bool condition)
    {
        // Load instruction database
        InstructionManual levelData;
        data = new DataManageScript(data.GetPath(fileDirectoryPath), fileDirectory + TutorialNagivatorScript.Instance().get_manual.name + ".txt");

        // Finding existing file and overwritten to a new one
        if (data.FindFilePath()) levelData = data.LoadInfoThroughJson2<InstructionManual>();

        // Get a new one from the selected tutorial
        else levelData = TutorialNagivatorScript.Instance().get_manual;

        // Save to text file as json of the instruction database
        levelData.cleared.completed = condition;
        data.SaveInfoAsNewJson(levelData);
    }

    private void SetInstructionObject()
    {
        // Get instruction access id to process level properties
        switch (TutorialNagivatorScript.Instance().get_manual.toolAccessId)
        {
            case 1: // Extraction
                // Game Logical Script
                extraction = new DW_ToothExtraction();

                // Tooth Spawning Script (Baby Molar)
                extraction_toothAddons = GetComponent<DW_ExtraTooth_Addons>();
                extraction.Begin();

                extraction_toothAddons.enabled = true;
                extraction_toothAddons.Invoke("GetToothExtraction", 0.5f);

                // Task Logical Script (Addons)
                tasking.Setup(Resources.Load<showTask>("minigameTasklist/Extraction"));

                // Audio Script
                audiolibrary = new DW_AudioLibrary();
                break;

            default: // Any instruction level
                break;
        }
    }

    private void SetGamePropReady()
    {
        // Find info of tag with gameobject
        foreach (GameSetup_Data info in gameInfo)
            foreach (GameObject prop in info.props)
                prop.tag = info.props_tag_name;
    }

    private string GetActionListDescription(InterfaceFeedBack feedBack)
    {
        // Identify the feedback needed for use
        switch (feedBack)
        {
            // When tool is selected
            case InterfaceFeedBack.CurrentlyInUsed:
                return GameObject.FindGameObjectWithTag(gameInfo[(int)GameTagPlacement.DW_Tool].props_tag_name).name + "\n" + GetCamViewInUsedTool();

            // When tool is performing the given task
            case InterfaceFeedBack.PerformingUsedTool:
                return "Performing Task: ";

            // When tool is currently set to be performing and something interupt the process
            case InterfaceFeedBack.CancelledInQueueTool:
                return "The following task have been cancelled: ";

            // When there is no action made from user
            case InterfaceFeedBack.Idle:
                return "Done!";
        }

        // Can't define any of the possible feedback
        return "???";
    }
    #endregion

    #region MAIN
    public void AdvancementContent(string title, float progressValue)
    {
        // Find out where the performing tool is not cancelled
        if (onGoingFeedback != InterfaceFeedBack.CancelledInQueueTool)
        {
            // Display the progress status
            onGoingFeedback = InterfaceFeedBack.PerformingUsedTool;
            if (!actionStatus.activeInHierarchy) actionStatus.SetActive(true);

            // Update the content of the current progress
            bool centerAlignment = FindCamViewIndex() == 0;

            if (progressValue >= 0) UpdateInstructionActionStatus(GetActionListDescription(onGoingFeedback) + title + "\n" + progressValue.ToString("0.0") + " %", centerAlignment);
            else UpdateInstructionActionStatus(GetActionListDescription(InterfaceFeedBack.Idle), centerAlignment);
        }
    }

    public void UseFeedbackDisplay(InterfaceFeedBack feedback, bool visible, string title)
    {
        // Find the possible task to play
        if (onGoingFeedback != InterfaceFeedBack.PerformingUsedTool) onGoingFeedback = feedback;

        // Continue to display perform task progress instead of marker content
        if (onGoingFeedback != InterfaceFeedBack.PerformingUsedTool)
        {
            // Display the progress status
            actionStatus.SetActive(visible);

            // Update the content to manage the user for direction
            bool centerAlignment = onGoingFeedback == InterfaceFeedBack.CurrentlyInUsed && 
                FindCamViewIndex() != GameObject.FindGameObjectWithTag(gameInfo[(int)GameTagPlacement.DW_Camera].props_tag_name).GetComponent<DW_AutoToolScaler>().get_scaleData.Length - 1;

            if (visible) UpdateInstructionActionStatus(GetActionListDescription(onGoingFeedback) + (title != string.Empty ? title : string.Empty), centerAlignment);
        }
    }

    public void RefreshFeedbackContent(InterfaceFeedBack feedback)
    {
        // Identify the feedback given
        switch (feedback)
        {
            // Currently using tool
            case InterfaceFeedBack.CurrentlyInUsed:
                UseFeedbackDisplay(InterfaceFeedBack.CurrentlyInUsed, true, string.Empty);
                break;
        }
    }
    #endregion

    #region COMPONENT
    private void SetClearedCondition(bool condition)
    {
        // Finding of tutorial begin to setup
        if (TutorialNagivatorScript.getScript != null)
        {
            // Update Instruction Status using data manage
            UpdateInstructionStatus(condition);

            // Save progress to instruction status
            //SaveProgressForThisSession(condition);
        }     
    }

    private void LevelInProgress()
    {
        // Get instruction access id to process level properties
        switch (TutorialNagivatorScript.Instance().get_manual.toolAccessId)
        {
            case 1: // Extraction
                if (extraction != null)
                {
                    // Finding the extraction is completed
                    if (extraction.IsCompleted()) SetClearedCondition(true);

                    // Finding the extraction is not going well
                    else if (extraction.IsFailed()) SetClearedCondition(false);

                    // Display the remark status
                    statusRemark.transform.GetComponent<TMP_Text>().text = extraction.GetExtractionProgressStatus();

                    // Get guide intruction (MISC)
                    GetGuideInstruction();
                }
                break;

            default: // Any instruction level
                break;
        }
    }
    #endregion

    #region MISC
    private string GetCamViewInUsedTool()
    {
        Debug.Log("GOT HERE PLS");
        // Find the view cam to display text accordingly
        switch (FindCamViewIndex())
        {
            case 1:
                return "Drag around the screen to move the tool around.\nClick on the tooth to view closely.";
            case 2:
                return "Click on to the teeth for better view.";
            default:
                return "Tap the back button to use the instrument.";
        }
    }

    private string GetCamViewInIdleMode()
    {
        // Find the view cam to display text accordingly
        switch (FindCamViewIndex())
        {
            case 1:
                return "Locate #55 .\n Tap on #55 to start extraction process.";
            case 2:
                return "Locate #55.";
            default:
                return "Use the " + tasking.GetToolOftheCurrentStep() + " to begin the next step.";
                //return "Use the syringe to inject the anesthetic.";
        }
    }

    private int FindCamViewIndex()
    {
     
        
        // Get data from other script
        ToolScale_Data[] data = GameObject.FindGameObjectWithTag(gameInfo[(int)GameTagPlacement.DW_Camera].props_tag_name).GetComponent<DW_AutoToolScaler>().get_scaleData;

        // Search for available cam in used
        for (int index = 0; index < data.Length; index++)
            if (data[index].cameraRef.gameObject.activeInHierarchy) return index;

        // Find nothing and give a negative value
        return -1;
    }

    private void GetGuideInstruction()
    {
        if (onGoingFeedback == InterfaceFeedBack.Idle && !winScreen.activeInHierarchy && !loseScreen.activeInHierarchy)
        {
            if (!actionStatus.activeInHierarchy) actionStatus.SetActive(true);
            teethMan.tm.CT(GetCamViewInIdleMode(), FindCamViewIndex() != GameObject.FindGameObjectWithTag(gameInfo[(int)GameTagPlacement.DW_Camera].props_tag_name).GetComponent<DW_AutoToolScaler>().get_scaleData.Length - 1); 
        }
    }
    #endregion
}
