using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DW_AdvanceAnestheicTool : MonoBehaviour
{
    private const string pathName = "TutorialAssets/MarkerPoint_Gum";
    private bool isCreated = false;

    #region SETUP
    private GameObject[] GetObjectsByTag(int index)
    {
        // Find objects in a group by passing a value of gameInfo index
        return GameObject.FindGameObjectsWithTag(TutorialGame_Script.thisScript.get_GameInfo[index].props_tag_name);
    }
    #endregion

    #region MAIN
    public void UseAdvanceScript()
    {
        // Remove the current uses of marker
        isCreated = RemovePreviousMarker();

        // Create a new marker point by tagging it to the object
        if (!isCreated) CreateMarkerToAnestheic();
    }
    #endregion

    #region COMPONENT
    private void CreateMarkerToAnestheic()
    {
        // Identify all damaged tooth
        GameObject[] damagedTooth = GetObjectsByTag((int)GameTagPlacement.DamagedTooth);

        // Create mark point for the uses of anestheic
        foreach (GameObject sideOfTooth in damagedTooth)
        {
            GameObject sector = Instantiate(Resources.Load<GameObject>(pathName), sideOfTooth.transform.position, Quaternion.identity);
            sector.GetComponent<MeshRenderer>().enabled = false;
            sector.transform.SetParent(sideOfTooth.transform);

            // Set offset to selection marker
            sector.transform.position += sector.GetComponent<DW_GumSectorData>().GetOffsetPoint();

            // Set selection marker of size
            sector.transform.localScale = sector.GetComponent<DW_GumSectorData>().GetMarkerSize();
        }
    }

    private bool RemovePreviousMarker()
    {
        // Identify the previous gum section
        GameObject[] gumSection = GetObjectsByTag((int)GameTagPlacement.GumSection);

        // Remove the gum section tagged with it
        foreach (GameObject section in gumSection)
        {
            // Clear away sector which that isn't apply to any anestheicDosed
            if (!section.GetComponent<DW_AnestheicPlacement>().IsAnestheicDosed())
            {
                Destroy(section);
                return false;
            }

            // No change made to the sector
            else
                return true;

        }

        // Make it a new instance
        return false;
    }
    #endregion
}
