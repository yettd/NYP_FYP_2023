using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialDashBoard_Selection_Script : MonoBehaviour
{
    [SerializeField] private GameObject selectionTab;
    private TutorialDashBoard_Script dashBoard;

    private const int content_selectionVisible = 3;
    private int tutorialLength = 0;
    private int currentPage_selection = 1;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("LoadContentForUse", 0.5f);
    }

    #region SETUP
    private void LoadContentForUse()
    {
        dashBoard = GetComponent<TutorialDashBoard_Script>();

        tutorialLength = dashBoard.get_tutorial.Length;
        RefreshPageParameter();
    }

    private RawImage GetTitleImage(string title)
    {
        RawImage reference = Resources.Load<RawImage>("TutorialAssets/meta/Slot");
        Texture icon = Resources.Load<Texture>("TutorialAssets/TutorialStageIcon/" + title);
        RawImage clone = Instantiate(reference, transform.position, Quaternion.identity);

        clone.GetComponent<TutorialDashBoard_Slot_Script>().SetIcon(icon);
        return clone;
    }

    private void ClearSlotArray()
    {
        for (int slot = 0; slot < selectionTab.transform.childCount; slot++)
            Destroy(selectionTab.transform.GetChild(slot).gameObject);
    }
    #endregion

    #region MAIN
    public void GetNextPage()
    {
        currentPage_selection++;
        RefreshPageParameter();
    }

    public void GetPreviousPage()
    {
        currentPage_selection--;
        RefreshPageParameter();
    }
    #endregion

    #region COMPONENT
    private void RefreshPageParameter()
    {
        ClearSlotArray(); // Clear all generated slot
        int formula = (currentPage_selection - 1) * content_selectionVisible; // Use X - 1 with the number of slot visible. X = current page

        for (int slot = 0; slot < content_selectionVisible; slot++)
        {
            if (CheckEntryGenerateParameter(formula + slot)) // Check for slot which is not able to generate due to array boundary
                 GerenateSelectionSlot(dashBoard.get_tutorial[formula + slot].Title, 
                     dashBoard.get_tutorial[formula + slot].saveId); // Generate slot into selection tab with data
        }
    }

    private void GerenateSelectionSlot(string title, int index)
    {
        // Slot is been generated and set to selection tab
        RawImage slot = GetTitleImage(title);
        slot.transform.SetParent(selectionTab.transform);

        // Set destination index for quick access for stage progression
        slot.GetComponent<TutorialDashBoard_Slot_Script>().SetDestinationIndex(index);

        if (GetTutorialProgress(index)) slot.GetComponent<TutorialDashBoard_Slot_Script>().SetCompleteSlot(); // Completed Task
        else if (!GetTutorialAvailability(index)) slot.GetComponent<TutorialDashBoard_Slot_Script>().SetLockedSlot(); // Locked Task
        else slot.GetComponent<TutorialDashBoard_Slot_Script>().SetUnlockSlot(); // Open Task
    }
    #endregion

    #region CONDITION
    public bool CheckPreviousPageAvailability()
    {
        return currentPage_selection * content_selectionVisible > content_selectionVisible;
    }

    public bool CheckNextPageAvailability()
    {
        return currentPage_selection * content_selectionVisible < tutorialLength;
    }

    private bool GetTutorialProgress(int index)
    {
        if (PlayerPrefs.HasKey("Tutorial_" + index + "_Completed"))
            return true;
        else
            return false;
    }

    private bool GetTutorialAvailability(int index)
    {
        if (index - 1 > 0) return GetTutorialProgress(index - 1);
        else return true;
    }

    private bool CheckEntryGenerateParameter(int index)
    {
        return index < tutorialLength;
    }
    #endregion
}
