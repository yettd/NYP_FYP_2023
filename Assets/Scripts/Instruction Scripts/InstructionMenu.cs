using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;


public class InstructionMenu : MonoBehaviour
{
    private DataManageScript data;
    private const string fileDirectory = "savefile_tutorial_";

    private enum ProcessButtonStatus { START, COMPLETE };
    private ProcessButtonStatus status = ProcessButtonStatus.START;

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
        LoadStageData();
        UpdateContent();
        RefreshProgressManual();
    }

    private void LoadStageData()
    {
        data = new DataManageScript("Assets/Resources/TutorialLevel/meta/", fileDirectory + GetInstructionSelect() + ".txt");
        if (TutorialNagivatorScript.thisScript)
        {
            manual = TutorialNagivatorScript.thisScript.get_manual;
            Debug.Log(manual.description);
        }
        else manual = Resources.Load<InstructionManual>("TutorialLevel/None");

        if (data.FindFilePath()) LoadProgressThroughLocal();
    }

    private string GetInstructionSelect()
    {
        if (TutorialNagivatorScript.thisScript) return TutorialNagivatorScript.thisScript.get_manual.name;
        else return "None";
    }
    #endregion

    #region MAIN
    public void ProcessToCompletion()
    {
        SaveProgressThroughLocal();
        string destinationScene = (status == ProcessButtonStatus.COMPLETE ? SceneReturn : TutorialNagivatorScript.thisScript.GetGameScene());
        SceneManager.LoadScene(destinationScene);
    }

    public void ReturnToMain()
    {
        SaveProgressThroughLocal();
        SceneManager.LoadScene(SceneReturn);
    }

    public void RefreshProgressManual()
    {
        CheckingForCompleteTutorial();
        CheckingForCompleteStep();
    }
    #endregion

    #region COMPONENT
    private void UpdateContent()
    {
        Title.text = manual.Title;
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

    private void CheckingForCompleteTutorial()
    {
        status = (manual.cleared.completed ? ProcessButtonStatus.COMPLETE : ProcessButtonStatus.START);
        ProcessBtn.gameObject.transform.GetChild(0).GetComponent<TMP_Text>().text = status.ToString();
    }
    #endregion

    #region EXTRA
    private void SaveProgressThroughLocal()
    {
        // Saving progress
        data.SaveInfoAsNewJson(manual);
    }

    private void LoadProgressThroughLocal()
    {
        // Load progress
        InstructionManual loadedData = data.LoadInfoThroughJson<InstructionManual>();
        
        for (int step = 0; step < manual.step.Length; step++)
            manual.step[step].completed = loadedData.step[step].completed;

        manual.cleared.completed = loadedData.cleared.completed;
    }
    #endregion
}
