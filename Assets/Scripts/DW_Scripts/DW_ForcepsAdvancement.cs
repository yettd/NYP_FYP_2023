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

    private const float pullOutTooth_timeOut = 1.5f;
    private const float wrapOutFromPulling_timeOut = 3;

    private DW_ActionListProgram actionProgram;

    void Update()
    {
        // Pull out tooth by moving the tool and tooth
        PerformTheToolAndTooth(isMove, 0.5f);

        // Ready for use: Action Status
        if (isPerforming && actionProgram != null) actionProgram.StartProgram();

        // Start-up tool performing if not
        if (!isPerforming && TutorialGame_Script.thisScript.get_onGoingFeedback != InterfaceFeedBack.PerformingUsedTool)
            BeginTheProcess();
    }

    #region SETUP
    private void SpawnToolAsRolePlay()
    {
        // Find the tool currently used
        GameObject toolUsed = GameObject.FindGameObjectWithTag(TutorialGame_Script.thisScript.get_GameInfo[(int)GameTagPlacement.DW_Tool].props_tag_name);

        // Assigned tool
        if (toolUsed)
        {
            // Start the process of pulling out
            GetComponent<DW_ForcepsPlacement>().ConfirmToothPlacement();

            // Turn marker to orginal object state
            GameObject forceps = Resources.Load<GameObject>("TutorialAssets/Tools/AnimatedTools/" + toolUsed.name);

            // Spawn the tool without the selection marker
            tool = Instantiate(forceps);

            // Finalize the tool position to the plotted area
            Vector3 position = toolUsed.transform.position;
            tool.transform.position = tool.GetComponent<DW_AdvancementObject_Offset>().GetPosition(position);

            // Remove active DW_Tool out of play
            Destroy(toolUsed);
        }
        else
            // Can't find tool used
            TutorialGame_Script.thisScript.UseFeedbackDisplay(InterfaceFeedBack.CancelledInQueueTool, true, "Removal");
    }

    private void SpawnToothAsRolePlay()
    {
        // Find the assign tool have been setup
        if (tool)
        {
            // Make an instance of a tooth
            tooth = Instantiate(Resources.Load<GameObject>("TutorialAssets/" + toothUsed));

            // Make tooth fit the grabbing point
            tooth.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);

            // Locate the instance to the existing tooth
            tooth.transform.position = toothTransform.position;
        }
    }

    private void DespawnRolePlay(float delay)
    {
        // Clear away tool
        Destroy(tool, delay);

        // Clear away tooth
        Destroy(tooth, delay);
    }

    private void PerformTheToolAndTooth(bool condition, float playBack)
    {
        // Pefrom movement until condition inactive
        if (condition)
        {
            // Move of tool
            tool.transform.Translate(Vector3.down * playBack * Time.deltaTime);

            // Move of tooth
            tooth.transform.Translate(Vector3.down * playBack * Time.deltaTime);
        }
    }

    private void CreateTaskTimeLoad()
    {
        // Add to task and convert it
        actionProgram.AddTaskActionLoad(pullOutTooth_timeOut);
        actionProgram.AddTaskActionLoad(wrapOutFromPulling_timeOut);

        // Write in the detail of the task performing
        actionProgram.IdentifyActionType("Removal");
    }
    #endregion

    #region COMPONENT
    private void BeginTheProcess()
    {
        // Start prepare of perfrom action
        LocateUpTooth();

        // Make it so as it won't interfere what its about to do
        isPerforming = true;
    }

    private void LocateUpTooth()
    {
        // Display the tool
        SpawnToolAsRolePlay();

        // Display the tooth
        SpawnToothAsRolePlay();

        // Find the assign tool have been setup
        if (tool)
        {
            // Play animated tool
            if (tool.GetComponent<Animator>() != null) tool.GetComponent<Animator>().SetTrigger("Action");

            // Completed
            Invoke("PullOutTooth", pullOutTooth_timeOut);
        }
        else
            // Remove script for use
            Destroy(GetComponent<DW_ForcepsAdvancement>());
    }

    private void PullOutTooth()
    {
        // Set to simulate the pull
        isMove = true;

        // Simulate until it hit the time out
        Invoke("FinishingUpTool", wrapOutFromPulling_timeOut);
    }

    private void FinishingUpTool()
    {
        // Make it stop
        isMove = false;

        // Make it disappear from site
        DespawnRolePlay(1);

        // Remove script for use
        Destroy(GetComponent<DW_ForcepsAdvancement>());
    }
    #endregion

    #region MAIN
    public void PerformTool(GameObject target)
    {
        // Build in feedback when performing task
        actionProgram = new DW_ActionListProgram();
        CreateTaskTimeLoad();

        // Get the latest tooth position
        toothTransform = target.transform;

        // Get tooth id for used
        toothUsed = target.name;
    }
    #endregion
}
