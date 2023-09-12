using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InstructionToolGrant : MonoBehaviour
{
    [SerializeField] private GameObject ToolDisplayTab;
    [SerializeField] private GameObject DisplayEmptyText;
    private InstructionManual manual;

    // Start is called before the first frame update
    void Start()
    {
        LoadAssetManual();
        Invoke("DisplayToolBySlot", 1);
    }

    #region SETUP
    private void LoadAssetManual()
    {
        manual = TutorialNagivatorScript.thisScript.get_manual;
        DisplayEmptyText.SetActive(manual.tools.Length == 0);
    }

    private void SpawnSlot(Texture texture)
    {
        RawImage slot = Instantiate(Resources.Load<RawImage>("InstructionManual/toolSlot"));
        slot.texture = texture;
        slot.transform.SetParent(ToolDisplayTab.transform);
    }
    #endregion

    #region MAIN
    public void SelectTool()
    {
      
    }

    public void DeSelectTool()
    {

    }
    #endregion

    #region COMPONENT
    private void DisplayToolBySlot()
    {
        foreach (ItemTag tool in manual.tools)
            SpawnSlot(tool.icon);
    }
    #endregion
}
