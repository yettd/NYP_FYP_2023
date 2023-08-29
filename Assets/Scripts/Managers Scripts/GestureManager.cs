using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GestureManager : MonoBehaviour
{
    public TMP_Text TextObject;
    public BackController backController;
    public GameObject MainScene;
    [HideInInspector]
    public bool inPosition;


    private bool SucceedBack;
    private Touch touch;
    private Vector2 beginTouchPos, endTouchPos;
    private float initialXPlacement;

    private void Start()
    {
        //Set how far left of the screen the player will have to swipe to go back
        initialXPlacement = Screen.width * .1f;//rn is based on the 10% of screen width
    }

    private void OnEnable()
    {
        //Reset the variables once go back to the screen
        inPosition = false;
        SucceedBack = false;
    }
   
    private void Update()
    {
        if (MainScene.activeInHierarchy)//In case the gesture manager for some reason in a different scene...
        {
            return;
        }
        // oni 1 finger on the screen
        if (Input.touchCount == 1)
        {
            touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began://User will have to touch from the left screen
                    beginTouchPos = touch.position;
                    CheckIfRightSpot(beginTouchPos.x);
                    break;
                case TouchPhase.Ended://User will have to let go once they reach about half way of the screen to go back
                    endTouchPos = touch.position;
                    if (inPosition && endTouchPos.x > Screen.width * 0.4f)
                    {
                        TextObject.text = "back";
                        SucceedBack = true;
                    }
                    inPosition = false;
                    break;
            }
        }
        else
        {
            //User will have to let go once they reach about half way of the screen to go back
            endTouchPos = touch.position;
            if (inPosition && endTouchPos.x > Screen.width * 0.4f)
            {
                TextObject.text = "back";
                SucceedBack = true;
            }
            inPosition = false;
        }

        //If all condition met, go back to previous state
        if (SucceedBack)
        {
            SucceedBack = false;
            backController.GoBackTo();

        }
    }

    void CheckIfRightSpot(float position)
    {
        if (position < initialXPlacement)
        {
            TextObject.text = "in the rite spot";//This is for debug
            inPosition = true;
        }
        else
        {
            TextObject.text = "in the wrong spot";//This is for debug
        }
    }

    
}
