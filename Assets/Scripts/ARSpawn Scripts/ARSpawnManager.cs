using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARSpawnManager : MonoBehaviour
{
    public GameObject BlockerGameObject;
    public GameObject dragTutorial;
    public GameObject pinchTutorial;
    public GameObject twistTutorial;
    private bool shownDragGuide;
    private bool shownPinchGuide;
    private bool shownTwistGuide;

    Lean.Touch.LeanTwistRotateAxis leanTwistRotateAxis;

    [Header("Scripts")]
    public SceneChanger sceneChanger;
    private void Awake()
    {
        // start with dragging tutorial
        if (!shownDragGuide && !shownPinchGuide && !shownTwistGuide)
        {
            dragTutorial.SetActive(true);
            pinchTutorial.SetActive(false);
            twistTutorial.SetActive(false);
            BlockerGameObject.SetActive(true);
        }
    }

    private void Update()
    {
        // check if drag tutorial is done, then show pinch
        if (shownDragGuide && !shownPinchGuide && !shownTwistGuide)
        {
            dragTutorial.SetActive(false);
            pinchTutorial.SetActive(true);
            twistTutorial.SetActive(false);
            BlockerGameObject.SetActive(true);
        }
        // check if pinch tutorial is done, then show twist
        if (shownDragGuide && shownPinchGuide && !shownTwistGuide)
        {
            dragTutorial.SetActive(false);
            pinchTutorial.SetActive(false);
            twistTutorial.SetActive(true);
            BlockerGameObject.SetActive(true);
        }
    }

    public void HideDrag() // when drag is shown
    {
        dragTutorial.SetActive(false);
        shownDragGuide = true;
    }
    public void HidePinch() // when pinch is shown
    {
        pinchTutorial.SetActive(false);
        shownPinchGuide = true;
    }
    public void HideBlocker() // when twist is shown
    {
        BlockerGameObject.SetActive(false);
        shownTwistGuide = true;
    }

    public void OnHomeClicked()
    {
        // reset DTH ID to none, button ID to mainscene when home is pressed
        ButtonReferenceManager.Instance.storedDTHButtonID = DTHEnum.NONE;
        ButtonReferenceManager.Instance.storedButtonID = ButtonENUM.MAINSCENE;
        sceneChanger.ChangeToMainScene();
    }
}
