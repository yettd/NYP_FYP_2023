using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;


public class InstructionMenu : MonoBehaviour
{
    [SerializeField] private Saving data;

    private enum ProcessButtonStatus { START, COMPLETE };
    private ProcessButtonStatus status = ProcessButtonStatus.START;

    [SerializeField] private Button ProcessBtn;
    [SerializeField] private string SceneReturn;
    [SerializeField] private TMP_Text Title;
    [SerializeField] private Text Description;

    private InstructionManual manual;

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
        manual = TutorialNagivatorScript.thisScript.get_manual;
        LoadProgressThroughLocal();
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
        data.saveToJson(manual, "Complete");
    }

    private void LoadProgressThroughLocal()
    {
        //// Load progress
        //string text = data.LoadFromJson("Complete");
        //TutorialNagivatorScript.thisScript.LoadManalData(text);
        //data.clearSave();
    }
    #endregion
}
