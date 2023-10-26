using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DW_AnestheicAdvancement : MonoBehaviour
{
    private GameObject tool;

    private enum MoveAction { locate, putAway };
    private bool[] isMove = { false, false };
    private Vector3[] direction = { Vector3.down, Vector3.up };

    private bool isPerforming = false;

    private const float playBackSpeed = 0.5f;
    private const float toolOnLocating_timelimit = 2;
    private const float toolOnApplying_timelimit = 8;
    private const float toolOnClearing_timelimit = 4;

    private DW_ActionListProgram actionProgram;

    void Update()
    {
        // Move to the point where the gum located
        PerformToolMovement(isMove[(int)MoveAction.locate], direction[(int)MoveAction.locate], playBackSpeed);

        // Clear away from the point
        PerformToolMovement(isMove[(int)MoveAction.putAway], direction[(int)MoveAction.putAway], playBackSpeed);

        // Ready for use: Action Status
        if (actionProgram != null) actionProgram.StartProgram();
    }

    #region SETUP
    private void SpawnToolAsRolePlay()
    {
        // Find the tool currently used
        string toolUsed = GameObject.FindGameObjectWithTag(TutorialGame_Script.thisScript.get_GameInfo[(int)GameTagPlacement.DW_Tool].props_tag_name).name;
        GameObject syringe = Resources.Load<GameObject>("TutorialAssets/Tools/AnimatedTools/" + toolUsed);

        // Spawn the tool without the selection marker
        tool = Instantiate(syringe);

        // Finalize the tool position to the plotted area
        Vector3 position = GameObject.FindGameObjectWithTag(TutorialGame_Script.thisScript.get_GameInfo[(int)GameTagPlacement.DW_Tool].props_tag_name).transform.position;
        tool.transform.position = tool.GetComponent<DW_AdvancementObject_Offset>().GetPosition(position);
    }

    private void PerformToolMovement(bool condition, Vector3 direction, float playBack)
    {
        // Move tool to the point until the condition inactive
        if (condition) tool.transform.Translate(direction * playBack * Time.deltaTime);
    }

    private void CreateTaskTimeLoad()
    {
        actionProgram.AddTaskActionLoad(toolOnLocating_timelimit);
        actionProgram.AddTaskActionLoad(toolOnApplying_timelimit);
        actionProgram.AddTaskActionLoad(toolOnClearing_timelimit);
    }
    #endregion

    #region COMPONENT
    private void LocateUpGum()
    {
        // Display the tool
        SpawnToolAsRolePlay();

        // Start moving of tool to the point of gum
        isMove[(int)MoveAction.locate] = true;

        // Process to the next step
        Invoke("ApplyingDose", toolOnLocating_timelimit);
    }

    private void ApplyingDose()
    {
        // Make it stop when tool are in position
        isMove[(int)MoveAction.locate] = false;

        // Play animated model
        tool.GetComponent<Animator>().SetTrigger("Action");

        // Process to the next step
        Invoke("PrepareToClearUp", toolOnApplying_timelimit);
    }

    private void PrepareToClearUp()
    {
        // Start put away the tool
        isMove[(int)MoveAction.putAway] = true;

        // Process to the next step
        Invoke("ClearUpUsedTool", toolOnClearing_timelimit);
    }

    private void ClearUpUsedTool()
    {
        // Make it stop when tool are in position
        isMove[(int)MoveAction.putAway] = false;

        // Despawn after it's done
        Destroy(tool);

        // Disable script for use
        enabled = false;
    }
    #endregion

    #region MAIN
    public void PerformTool()
    {
        // Build in feedback when performing task
        actionProgram = new DW_ActionListProgram();
        CreateTaskTimeLoad();
        actionProgram.IdentifyActionType("Ease Pain");

        if (!isPerforming)
        {
            // Start prepare of perfrom action
            LocateUpGum();

            // Make it so as it won't interfere what its about to do
            isPerforming = true;
        }
    }
    #endregion
}
