using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class InstructionMenu : MonoBehaviour
{
    private DataManageScript data;
    private const string filePath = "TutorialLevel/";
    private const string fileDirectory = "savefile_tutorial.txt";

    private Saving data2;

    [SerializeField] private Button ProcessBtn;
    [SerializeField] private string SceneReturn;
    [SerializeField] private TMP_Text Title;
    [SerializeField] private Text Description;

    private InstructionManual manual;
    public InstructionManual get_manual { get { return manual; } }

    void Start()
    {
        LoadContentInfo();
    }

    #region SETUP
    private void LoadContentInfo()
    {
        LoadLocalFileData();
        LoadStageData();
        UpdateContentTitle();
        UpdateContentDescription();
        CheckingForCompleteStep();
    }

    private void LoadStageData()
    {
        manual = Resources.Load<InstructionManual>("TutorialLevel/Stage " + PlayerPrefs.GetInt("TutorialStageLevel", 1));
    }

    private void LoadLocalFileData()
    {
        data = new DataManageScript("Assets/Resources/" + filePath, fileDirectory);
        //LoadProgressThroughLocal();
    }
    #endregion

    #region MAIN
    public void ProcessToCompletion()
    {
        SceneManager.LoadScene("Tutorial_GameScene");
    }

    public void ReturnToMain()
    {
        // SaveProgressThroughLocal();
        SceneManager.LoadScene(SceneReturn);
    }

    public void RefreshProgressManual()
    {
        CheckingForCompleteStep();
    }
    #endregion

    #region COMPONENT
    private void UpdateContentTitle()
    {
        Title.text = manual.Title;
    }

    private void UpdateContentDescription()
    {
        Description.text = manual.description;
    }

    private void CheckingForCompleteStep()
    {
        int currentStep = 0;
        int totalStep = manual.step.Length;

        for (int check = 0; check < totalStep; check++)
        {
            if (manual.step[check].completed)
                currentStep++;
        }

        // Process Button
        ProcessBtn.interactable = (currentStep >= totalStep);
    }
    #endregion

    #region EXTRA
    //private void SaveProgressThroughLocal()
    //{
    //    InstructionManual[] manuals = Resources.LoadAll<InstructionManual>(filePath);
    //    if (data.FindFilePath(fileDirectory)) data.SaveInfoAsNewJson(string.Empty);

    //    foreach (InstructionManual manual in manuals)
    //        data.SaveInfoAsJson(manual);
    //}

    //private void LoadProgressThroughLocal()
    //{
    //    InstructionManual[] manuals = Resources.LoadAll<InstructionManual>(filePath);

    //    if (data.FindFilePath(fileDirectory))
    //    {
    //        foreach (InstructionManual manual in manuals)
    //        {
    //            manual.cleared = data.LoadInfoThroughJson<InstructionManual>().cleared;

    //            for (int step = 0; step < manual.step.Length; step++)
    //                manual.step[step].completed = data.LoadInfoThroughJson<InstructionManual>().step[step].completed;
    //        }
    //    }
    //}

    private void SaveProgressThroughLocal()
    {
        InstructionManual[] manuals = Resources.LoadAll<InstructionManual>(filePath);

        foreach (InstructionManual manual in manuals)
            data2.saveToJson(manual, "Tutorial_" + PlayerPrefs.GetInt("TutorialStageLevel", 1) + "_SaveFile");
    }

    private void LoadProgressThroughLocal()
    {
        InstructionManual[] manuals = Resources.LoadAll<InstructionManual>(filePath);
    }
    #endregion
}
