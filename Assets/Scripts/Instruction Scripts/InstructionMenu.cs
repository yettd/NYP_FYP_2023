using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class InstructionMenu : MonoBehaviour
{
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
        UpdateContentTitle();
        UpdateContentDescription();
        CheckingForCompleteStep();
    }

    private void LoadStageData()
    {
        manual = Resources.Load<InstructionManual>("TutorialLevel/Stage " + PlayerPrefs.GetInt("TutorialStageLevel", 1));
    }
    #endregion

    #region MAIN
    public void ProcessToCompletion()
    {
        PlayerPrefs.SetInt("Tutorial_" + PlayerPrefs.GetInt("TutorialStageLevel", 1) + "_Completed", 1);
        ReturnToMain();
    }

    public void ReturnToMain()
    {
        SceneManager.LoadScene(SceneReturn);
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
            if (PlayerPrefs.HasKey(manual.Title + "_Step_" + check))
                currentStep++;
        }

        // Process Button
        ProcessBtn.interactable = (currentStep >= totalStep);
    }
    #endregion
}
