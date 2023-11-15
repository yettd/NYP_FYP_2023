using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StorageItemScript : MonoBehaviour
{
    private StorageComponent component;
    private ItemTag item;
    private DW_ToolsNagivator toolsNagivator;

    private GameObject itemModel;
    public moveTools MTS;

    #region SETUP
    private void Start()
    {
        teethMan.tm.changeToolColor += changeShade;
    }
    public void SetItemComponent(StorageComponent component, ItemTag item)
    {
        toolsNagivator = DW_ToolsNagivator.GetToolNagivator();

        this.component = component;
        this.item = item;
    }
    #endregion

    #region MAIN
    public void SelectItem()
    {
        // Select item for uses
        component.GetItemSelection(item);

        // Target selected area on item currently used
        if (TutorialNagivatorScript.Instance().get_manual.toolAccessId == 1) toolsNagivator.GetToolsOnUse(item.itemName);
    }

    public void SelectTool()
    {
        if (TutorialNagivatorScript.Instance().get_manual.toolAccessId != 1)
        {
            minigameTaskListController.Instance.ToolsSelected(item.itemName, item.model);
            teethMan.tm.Changetoolcolor(item.itemName);
        }
    }
    public void changeShade(string a)
    {

        GetComponent<RawImage>().color = new Vector4(1, 1, 1, 1f);
        if (item.itemName == a)
        {

            GetComponent<RawImage>().color = new Vector4(0, 0, 0, 0.3f);
        }
    }
    #endregion
}
