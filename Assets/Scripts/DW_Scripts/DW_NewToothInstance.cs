using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DW_NewToothInstance : MonoBehaviour
{
    [SerializeField] private Vector3 offset;
    [SerializeField] private int tierIndex = 0;

    void Start()
    {
        // Begin with a countdown
        Invoke("StartOfToothInstance", 0.5f);
    }

    #region SETUP
    private void StartOfToothInstance()
    {
        // Identify the tooth that show the teeth which are currently placed
        tierIndex = FindToothInTeeth(gameObject);
    }

    private int FindToothInTeeth(GameObject target)
    {
        // Start searching up on the game info
        for (int teeth = 0; teeth < TutorialGame_Script.thisScript.get_GameInfo[(int)GameTagPlacement.TeethSection].props.Length; teeth++)
        {
            // Seacrh up the entire tooth in teeth
            for (int tooth = 0; tooth < TutorialGame_Script.thisScript.get_GameInfo[(int)GameTagPlacement.TeethSection].props[teeth].transform.childCount; tooth++)
            {
                // Find all the tooth with the correct match object
                if (TutorialGame_Script.thisScript.get_GameInfo[(int)GameTagPlacement.TeethSection].props[teeth].transform.GetChild(tooth).gameObject == target)

                    // Make the final plot of the teeth index
                    return teeth;
            }
        }

        // Can't find any tooth in teeth
        return -1;
    }
    #endregion

    #region COMPONENT
    public Vector3 GetOffset()
    {
        // Adjust the position of the new instance tooth
        return offset;
    }

    public int GetTeethIndex()
    {
        // Given the index and used it as the final index
        return tierIndex;
    }
    #endregion
}
