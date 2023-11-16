using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DW_LockTool : MonoBehaviour
{
    private bool isLocked;
    //public GameObject LockedGUI;

    // Update is called once per frame
    void Update()
    {
        isLocked = GameObject.FindGameObjectWithTag(TutorialGame_Script.thisScript.get_GameInfo[(int)GameTagPlacement.DW_Tool].props_tag_name) ||
                TutorialGame_Script.thisScript.get_onGoingFeedback == InterfaceFeedBack.PerformingUsedTool;
      
        GetComponent<rotateJaws>().enabled = !isLocked;
        //LockedGUI.SetActive(isLocked);
    }
    private void OnDisable()
    {
        teethMan.tm.changeToolColor("");
        isLocked = false;
        if (GameObject.FindGameObjectWithTag(TutorialGame_Script.thisScript.get_GameInfo[(int)GameTagPlacement.DW_Tool].props_tag_name) != null)
        {
            Destroy(GameObject.FindGameObjectWithTag(TutorialGame_Script.thisScript.get_GameInfo[(int)GameTagPlacement.DW_Tool].props_tag_name));
        }
    }
}
