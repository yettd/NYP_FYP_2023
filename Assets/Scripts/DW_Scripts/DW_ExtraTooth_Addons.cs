using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DW_ExtraTooth_Addons : MonoBehaviour
{
    [SerializeField] private GameObject babyTooth;

    private List<GameObject> toothPlacement;
    private List<DW_NewToothInstance> newSetup;

    void Start()
    {
        toothPlacement = new List<GameObject>();
        newSetup = new List<DW_NewToothInstance>();
    }

    #region SETUP
    private void RefreshData()
    {
        // Find all reference damaged tooth and store it value
        foreach (GameObject placement in TutorialGame_Script.thisScript.get_GameInfo[(int)GameTagPlacement.DamagedTooth].props)
        {
            // Store value of the orginal object
            toothPlacement.Add(placement);

            // Store the offset of the orginal object
            newSetup.Add(placement.GetComponent<DW_NewToothInstance>());
        }
    }

    private void SpawnTargetPoint(int index)
    {
        // Spawn and replace the tooth
        GameObject toothInstance = Instantiate(babyTooth);
        toothInstance.name = babyTooth.name;

        // Identify the selected teeth and tag with damaged tooth
        toothInstance.transform.SetParent(TutorialGame_Script.thisScript.get_GameInfo[(int)GameTagPlacement.TeethSection].props[toothPlacement[index].GetComponent<DW_NewToothInstance>().GetTeethIndex()].transform);
        toothInstance.tag = TutorialGame_Script.thisScript.get_GameInfo[(int)GameTagPlacement.DamagedTooth].props_tag_name;

        // Finalize the setup of the instance damaged tooth
        toothInstance.transform.localRotation = Quaternion.Euler(newSetup[index].GetRotation().x, newSetup[index].GetRotation().y, newSetup[index].GetRotation().z);
        toothInstance.transform.position = toothPlacement[index].transform.position + newSetup[index].GetOffset();
        toothInstance.transform.localScale = newSetup[index].GetScale();

        // Update tooth settings
        //toothInstance.GetComponent<DW_ToothSettings>().toothIndex = index;
    }

    private void ClearReferencePoint(int index)
    {
        // Uncheck reference after implementing new targeted instance
        toothPlacement[index].GetComponent<MeshRenderer>().enabled = false;
        toothPlacement[index].tag = TutorialGame_Script.thisScript.get_GameInfo[(int)GameTagPlacement.NotTagged].props_tag_name;
    }

    private void RefreshToothSetup()
    {
        // Declare tooth setup condition
        bool setup = false;

        // Search for all teeth section
        foreach (GameObject teeth in TutorialGame_Script.thisScript.get_GameInfo[(int)GameTagPlacement.TeethSection].props)
        {
            // Bring out all tooth for setup
            for (int tooth = 0; tooth < teeth.transform.childCount; tooth++)
            {
                // Find the damaged tooth to set right condition
                setup = teeth.transform.GetChild(tooth).tag != TutorialGame_Script.thisScript.get_GameInfo[(int)GameTagPlacement.DamagedTooth].props_tag_name;

                // Texture: Setup condition doesn't find any problem then perform this task
                if (setup) teeth.transform.GetChild(tooth).GetComponent<Renderer>().material = Resources.Load<Material>("TutorialAssets/Selected_CleanTooth");

                // Problem: Setup condition assigned accordingly
                teeth.transform.GetChild(tooth).GetComponent<openTooth>().problem = !setup;
            }
        }
    }

    #endregion

    #region MAIN
    public void GetToothExtraction()
    {
        // Retreive the data
        RefreshData();

        // Find all reference damaged tooth for replacement
        for (int index = 0; index < toothPlacement.ToArray().Length; index++)
            SpawnTargetPoint(index);

        // Clear away all original tooth after that 
        for (int index = 0; index < toothPlacement.ToArray().Length; index++)
            ClearReferencePoint(index);

        // Refresh tooth setup
        RefreshToothSetup();
    }
    #endregion
}
