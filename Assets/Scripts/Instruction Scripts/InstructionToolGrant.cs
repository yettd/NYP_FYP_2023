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
        Invoke("DisplayDefaultTool", 1);
    }

    #region SETUP
    private void LoadAssetManual()
    {
        if (TutorialNagivatorScript.getScript != null) manual = TutorialNagivatorScript.Instance().get_manual;
        else manual = Resources.Load<InstructionManual>("TutorialLevel/None");

        DisplayEmptyText.SetActive(manual.tools.Length == 0);
    }

    private void SpawnSlot(Texture texture)
    {
        RawImage slot = Instantiate(Resources.Load<RawImage>("InstructionManual/toolSlot"));
        slot.texture = texture;
        slot.transform.SetParent(ToolDisplayTab.transform);
        slot.transform.localScale=new Vector3(1, 1, 1);
    }

    private void EmptyOutSlot()
    {
        int slotCapacity = ToolDisplayTab.transform.childCount;

        if (slotCapacity != 0) 
            for (int i = 0; i < slotCapacity; i++) Destroy(ToolDisplayTab.transform.GetChild(i).gameObject);
    }

    private void DisplayDefaultTool()
    {
        DisplayToolBySlot(-1);
    }
    #endregion

    #region MAIN
    public void SelectTool()
    {
      
    }

    public void DeSelectTool()
    {

    }

    public void GetRequiredTool(GameObject obj)
    {
        DisplayToolBySlot(int.Parse(obj.name));
    }
    #endregion

    #region COMPONENT
    private void DisplayToolBySlot(int step)
    {
        // Clear away all tool shown
        EmptyOutSlot();

        // Gerenate tool are used or required
        if (step == -1)
        {
            foreach (ItemTag tool in manual.tools)
                SpawnSlot(tool.icon);
        }
        else
        {
            foreach (ItemTag tool in manual.tools)
                if (tool.itemName == manual.step[step].requiredTool)
                    SpawnSlot(tool.icon);
        }
    }
    #endregion
}
