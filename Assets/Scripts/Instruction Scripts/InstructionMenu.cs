using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;


public class InstructionMenu : MonoBehaviour
{
    private DataManageScript data;

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
        data = new DataManageScript("Assets/Resources/TutorialLevel/meta/", "savefile_tutorial_" + TutorialNagivatorScript.thisScript.get_manual.name + ".txt");
        manual = TutorialNagivatorScript.thisScript.get_manual;
        if (data.FindFilePath()) LoadProgressThroughLocal();
    }
    #endregion

    #region MAIN
    public void ProcessToCompletion()
    {
        SceneManager.LoadScene(TutorialNagivatorScript.thisScript.GetGameScene());
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
