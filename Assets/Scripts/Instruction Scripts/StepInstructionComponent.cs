using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class StepInstructionComponent : MonoBehaviour
{
    private InstructionTemplate checkList;
    private InstructionMenu menu;

    [SerializeField] private RawImage icon;
    [SerializeField] private TMP_Text text;

    #region SETUP
    public void SetManualPath(InstructionTemplate path)
    {
        menu = GameObject.Find("Interface").GetComponent<InstructionMenu>();
        checkList = path;
    }
    #endregion

    #region MAIN
    public void BeginGameSceneInstruction()
    {
        checkList.completed = true;
        menu.RefreshProgressManual();
        DisplayStepInstruction();
    }
    #endregion

    #region MODIFY COMPONENT
    public void SetIcon(Texture texture)
    {
        icon.texture = texture;
    }

    public void SetIndex(int index)
    {
        text.text = index.ToString();
    }
    #endregion

    #region COMPONENT
    private void DisplayStepInstruction()
    {
        GameObject.FindGameObjectWithTag("InstructionBoard").transform.GetChild(0).GetComponent<TMP_Text>().text = checkList.title;
        GameObject.FindGameObjectWithTag("InstructionBoard").transform.GetChild(1).GetComponent<Text>().text = checkList.instruction;
    }
    #endregion
}
