using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DW_ForcepsAdvancement : MonoBehaviour
{
    private GameObject tool;
    private GameObject tooth;

    private bool isPerforming;
    private bool isMove;

    private Transform toothTransform;
    private string toothUsed;

    void Update()
    {
        PerformTheToolAndTooth(isMove, 0.5f);
    }

    #region SETUP
    private void SpawnToolAsRolePlay()
    {
        // Find the tool currently used
        string toolUsed = GameObject.FindGameObjectWithTag(TutorialGame_Script.thisScript.get_GameInfo[(int)GameTagPlacement.DW_Tool].props_tag_name).name;
        GameObject forceps = Resources.Load<GameObject>("TutorialAssets/Tools/" + toolUsed);

        // Spawn the tool without the selection marker
        tool = Instantiate(forceps);

        // Finalize the tool position to the plotted area
        tool.transform.position = GameObject.FindGameObjectWithTag(TutorialGame_Script.thisScript.get_GameInfo[(int)GameTagPlacement.DW_Tool].props_tag_name).transform.position;
    }

    private void SpawnToothAsRolePlay()
    {
        tooth = Instantiate(Resources.Load<GameObject>("TutorialAssets/" + toothUsed));
        tooth.transform.position = toothTransform.position;
    }

    private void DespawnRolePlay(float delay)
    {
        Destroy(tool, delay);
        Destroy(tooth, delay);
    }

    private void PerformTheToolAndTooth(bool condition, float playBack)
    {
        if (condition)
        {
            tool.transform.Translate(Vector3.down * playBack * Time.deltaTime);
            tooth.transform.Translate(Vector3.down * playBack * Time.deltaTime);
        }
    }
    #endregion

    #region COMPONENT
    private void LocateUpTooth()
    {
        // Display the tool
        SpawnToolAsRolePlay();

        // Display the tooth
        SpawnToothAsRolePlay();

        // Completed
        Invoke("PullOutTooth", 1);
    }

    private void PullOutTooth()
    {
        // Set to simulate the pull
        isMove = true;

        // Simulate until it hit the time out
        Invoke("FinishingUpTool", 3);
    }

    private void FinishingUpTool()
    {
        // Make it stop
        isMove = false;

        // Make it disappear from site
        DespawnRolePlay(1);
    }
    #endregion

    #region MAIN
    public void PerformTool(GameObject target)
    {
        if (!isPerforming)
        {
            // Get the latest tooth position
            toothTransform = target.transform;

            // Get tooth id for used
            toothUsed = target.name;

            // Start prepare of perfrom action
            LocateUpTooth();

            // Make it so as it won't interfere what its about to do
            isPerforming = true;
        }
    }
    #endregion
}
