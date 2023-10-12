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
        RemovePreviousMarker();

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
        }

        // Done
        isCreated = true;
    }

    private void RemovePreviousMarker()
    {
        // Identify the previous gum section
        GameObject[] gumSection = GetObjectsByTag((int)GameTagPlacement.GumSection);

        // Remove the gum section tagged with it
        foreach (GameObject section in gumSection) section.tag = TutorialGame_Script.thisScript.get_GameInfo[(int)GameTagPlacement.NotTagged].props_tag_name;
    }
    #endregion
}
