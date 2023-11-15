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
    [SerializeField] private GameObject displayItemName;

    private InterfaceFeedBack onGoingFeedback;
    public InterfaceFeedBack get_onGoingFeedback { get { return onGoingFeedback; } }

    public GameObject get_itemDisplayName { get { return displayItemName; } }
    public DW_TaskAddons getTasking { get { return tasking; } }

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
        winScreen.SetActive(cleared);
        minigameTaskListController.Instance.OnGameComplete();
        // Display the lose screen of it
        loseScreen.SetActive(!cleared);
    }

    private void UpdateInstructionActionStatus(string title)
    {
        // Reset the visible status
        CancelInvoke("SetVisibleActionStatus");

        // Update the content to the given title
        actionStatus.transform.GetChild(0).GetComponent<TMP_Text>().text = title;

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
                return GetCamViewInUsedTool();

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
            if (progressValue >= 0) UpdateInstructionActionStatus(GetActionListDescription(onGoingFeedback) + title + "\n" + progressValue.ToString("0.0") + " %");
            else UpdateInstructionActionStatus(GetActionListDescription(InterfaceFeedBack.Idle));
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
            if (visible) UpdateInstructionActionStatus(GetActionListDescription(onGoingFeedback) + (title != string.Empty ? title : string.Empty));
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
        switch (FindCamViewIndex())
        {
            case 1:
                return "Drag the tool around to move it.\nClick on the tooth to view closely.";

            case 2:
                return "Zoom in for better view.\nClick on the teeth to view closely.";

            default:
                return "Zoom out for better view.\nDeselect tool when its done.";
        }
    }

    private int FindCamViewIndex()
    {
        int index = 0;
        ToolScale_Data[] data = GameObject.FindGameObjectWithTag(gameInfo[(int)GameTagPlacement.DW_Camera].props_tag_name).GetComponent<DW_AutoToolScaler>().get_scaleData;

        foreach (ToolScale_Data current in data)
        {
            if (current.cameraRef.gameObject.activeInHierarchy) break;
            index++;
        }

        return index;
    }
    #endregion
}
