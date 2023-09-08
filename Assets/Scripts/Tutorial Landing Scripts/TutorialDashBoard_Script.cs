using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TutorialDashBoard_Script : MonoBehaviour
{
    [SerializeField] private Button PreviousBtn;
    [SerializeField] private Button NextBtn;

    private TutorialDashBoard_Selection_Script selectionScript;

    private InstructionManual[] tutorial;
    public InstructionManual[] get_tutorial { get { return tutorial; } }

    // Start is called before the first frame update
    void Start()
    {
        Invoke("CheckForContentParameter", 0.5f);
        LoadTutorialData();
    }

    #region SETUP
    private void LoadTutorialData()
    {
        selectionScript = GetComponent<TutorialDashBoard_Selection_Script>();
        tutorial = Resources.LoadAll<InstructionManual>("TutorialLevel");
    }
    #endregion

    #region MAIN
    public void PreviousPage()
    {
        selectionScript.GetPreviousPage();
        CheckForContentParameter();
    }

    public void NextPage()
    {
        selectionScript.GetNextPage();
        CheckForContentParameter();
    }

    public void ReturnToMain()
    {
        SceneManager.LoadScene(0);
    }
    #endregion

    #region COMPONENT
    private void CheckForContentParameter()
    {
        // Check for availability button interaction on click
        // Disable interaction when availability is not true

        PreviousBtn.interactable = selectionScript.CheckPreviousPageAvailability();
        NextBtn.interactable = selectionScript.CheckNextPageAvailability();
    }
    #endregion
}
