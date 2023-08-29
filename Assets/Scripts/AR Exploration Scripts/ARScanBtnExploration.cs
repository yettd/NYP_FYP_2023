using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARScanBtnExploration : MonoBehaviour
{

    [SerializeField] GameObject toolInfoGameObject;
    ARExplorationSceneChanger sceneChanger;
    private void Awake()
    {
        sceneChanger = GetComponent<ARExplorationSceneChanger>();
    }

    //check if tool info is up
    public void PressedBack()
    {
        if (toolInfoGameObject.activeInHierarchy)
        {
            //if it is, back means close tool info
            toolInfoGameObject.SetActive(false);
        }
        else
        {
            sceneChanger.GoToExploration();
        }
    }

}
