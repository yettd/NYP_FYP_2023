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

    private DataManageScript data;
    private const string fileDirectory = "savefile_tutorial_";
    private const string fileDirectoryPath = "Assets/Resources/TutorialLevel/meta/";

    [SerializeField] private GameObject winScreen;
    [SerializeField] private GameObject loseScreen;
    [SerializeField] private GameObject statusRemark;

    void Start()
    {
        thisScript = this;

        // Begin to find all gameobject which tagged correctly
        SetGamePropReady();

        // Begin on level and setup starting property
        SetInstructionObject();
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

        // Display the lose screen of it
        loseScreen.SetActive(!cleared);
    }

    private void SaveProgressForThisSession()
    {
        // Load instruction database
        InstructionManual levelData;
        data = new DataManageScript(data.GetPath(fileDirectoryPath), fileDirectory + TutorialNagivatorScript.Instance().get_manual.name + ".txt");

        // Finding existing file and overwritten to a new one
        if (data.FindFilePath()) levelData = data.LoadInfoThroughJson2<InstructionManual>();

        // Get a new one from the selected tutorial
        else levelData = TutorialNagivatorScript.Instance().get_manual;

        // Save to text file as json of the instruction database
        data.SaveInfoAsNewJson(levelData);
    }

    private void SetInstructionObject()
    {
        // Get instruction access id to process level properties
        switch (TutorialNagivatorScript.Instance().get_manual.toolAccessId)
        {
            case 1: // Extraction
                extraction = new DW_ToothExtraction();
                extraction.Begin();
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
    #endregion

    #region MAIN
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
            //SaveProgressForThisSession();
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
}
