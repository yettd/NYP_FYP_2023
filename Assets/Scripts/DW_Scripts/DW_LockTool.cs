using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DW_LockTool : MonoBehaviour
{
    private bool isLocked;

    // Update is called once per frame
    void Update()
    {
        if (!Application.isEditor)
        {
            isLocked = GameObject.FindGameObjectWithTag(TutorialGame_Script.thisScript.get_GameInfo[(int)GameTagPlacement.DW_Tool].props_tag_name) ||
                TutorialGame_Script.thisScript.get_onGoingFeedback == InterfaceFeedBack.PerformingUsedTool;

            GetComponent<rotateJaws>().enabled = !isLocked;
        }
    }
}
