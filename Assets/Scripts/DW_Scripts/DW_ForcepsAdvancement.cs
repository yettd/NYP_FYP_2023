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

    void Update()
    {
        if (isMove)
        {
            tool.transform.Translate(Vector3.down * 0.5f * Time.deltaTime);
            tooth.transform.Translate(Vector3.down * 0.5f * Time.deltaTime);
        }
    }

    #region COMPONENT
    private void LocateUpTooth()
    {
        GameObject forceps = Resources.Load<GameObject>("TutorialAssets/Tools/ForcepsTool");
        tool = Instantiate(forceps);
        tool.transform.position = GameObject.FindGameObjectWithTag(TutorialGame_Script.thisScript.get_GameInfo[(int)GameTagPlacement.DW_Tool].props_tag_name).transform.position;
        tool.transform.GetComponent<MeshRenderer>().material = Resources.Load<Material>("TutorialAssets/Forcesp_Texture");

        tooth = Instantiate(Resources.Load<GameObject>("TutorialAssets/BabyMolar"));
        tooth.GetComponent<MeshRenderer>().material = Resources.Load<Material>("TutorialAssets/Selected_CleanTooth");
        tooth.transform.position = toothTransform.position;

        Invoke("PullOutTooth", 1);
    }

    private void PullOutTooth()
    {
        isMove = true;
        Invoke("FinishingUpTool", 3);
    }

    private void FinishingUpTool()
    {
        isMove = false;
        Destroy(tool, 1);
        Destroy(tooth, 1);
        Debug.Log("Finished Playing: Forceps");
    }
    #endregion

    #region MAIN
    public void PerformTool(Transform _transform)
    {
        if (!isPerforming)
        {
            toothTransform = _transform;
            LocateUpTooth();
            isPerforming = true;
        }
    }
    #endregion
}
