using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DW_ExtraTooth_Addons : MonoBehaviour
{
    [SerializeField] private GameObject babyTooth;

    private List<GameObject> toothPlacement;
    private List<Vector3> offset;

    void Start()
    {
        toothPlacement = new List<GameObject>();
        offset = new List<Vector3>();
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
            offset.Add(placement.GetComponent<DW_NewToothInstance>().GetOffset());
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
        toothInstance.transform.position = toothPlacement[index].transform.position + offset[index];
    }

    private void ClearReferencePoint(int index)
    {
        // Uncheck reference after implementing new targeted instance
        toothPlacement[index].GetComponent<MeshRenderer>().enabled = false;
        toothPlacement[index].tag = TutorialGame_Script.thisScript.get_GameInfo[(int)GameTagPlacement.NotTagged].props_tag_name;
    }

    private void RefreshToothTexture()
    {
        foreach (GameObject teeth in TutorialGame_Script.thisScript.get_GameInfo[(int)GameTagPlacement.TeethSection].props)
            for (int tooth = 0; tooth < teeth.transform.childCount; tooth++)
                if (teeth.transform.GetChild(tooth).tag != TutorialGame_Script.thisScript.get_GameInfo[(int)GameTagPlacement.DamagedTooth].props_tag_name)
                    teeth.transform.GetChild(tooth).GetComponent<Renderer>().material = Resources.Load<Material>("TutorialAssets/Selected_CleanTooth");
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

        // Refresh tooth texture
        RefreshToothTexture();
    }
    #endregion
}
