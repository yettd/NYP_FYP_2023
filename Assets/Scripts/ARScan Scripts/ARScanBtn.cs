using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARScanBtn : MonoBehaviour
{
    private BackController backController;

    [SerializeField] GameObject toolInfoGameObject;
    [SerializeField] SceneChanger sceneChanger;

    private void Awake()
    {
        backController = GetComponent<BackController>();
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
            //if not, means nth else is on(since video is on diff scene)
            backController.GoBackTo();
        }
    }

    public void HomeClick()
    {
        ButtonReferenceManager.Instance.storedDTHButtonID = DTHEnum.NONE;
        ButtonReferenceManager.Instance.storedButtonID = ButtonENUM.MAINSCENE;

        sceneChanger.ChangeToMainScene();
    }
}
