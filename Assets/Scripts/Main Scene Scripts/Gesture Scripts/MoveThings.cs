using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveThings : MonoBehaviour
{
    public GameObject MainMenuGameObject;
    public List<GameObject> listOfPanels = new List<GameObject>();
    private Vector2 InitialPosition;
    private Vector2 CurrentPosition;

    private Touch touch;
    private GestureManager gestureManager;


    private void Start()
    {
        gestureManager = GetComponent<GestureManager>();
    }

    private void Update()
    {
        // oni 1 finger on the screen
        //Get position of touch
        if (Input.touchCount == 1)
        {
            touch = Input.GetTouch(0);
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    if (gestureManager.inPosition)
                        InitialPosition = touch.position;
                    break;
                case TouchPhase.Moved:
                    if (gestureManager.inPosition)
                        CurrentPosition = touch.position - InitialPosition;
                    break;
                case TouchPhase.Stationary:
                    break;
                case TouchPhase.Ended:
                    CurrentPosition = new Vector2(0, 0);
                    break;
                case TouchPhase.Canceled:
                    CurrentPosition = new Vector2(0, 0);
                    break;
                default:
                    break;
            }
            if (CheckWhichPanelActive() != MainMenuGameObject)
            {
                //Move the background based on CurrentPosition but slow down abit
                CheckWhichPanelActive().GetComponent<RectTransform>().anchoredPosition = new Vector2(CurrentPosition.x * .25f, CheckWhichPanelActive().GetComponent<RectTransform>().anchoredPosition.y);
            }
        }
        else if (Input.touchCount == 0)
        {
            //Reset the panel back to normal
            CheckWhichPanelActive().GetComponent<RectTransform>().anchoredPosition = new Vector2(0f, CheckWhichPanelActive().GetComponent<RectTransform>().anchoredPosition.y);
        }
      
    }

    GameObject CheckWhichPanelActive()//Just to check which GameObject should be move
    {
        for (int i = 0; i < listOfPanels.Count; i++)
        {
            if (listOfPanels[i].activeInHierarchy)
            {
                return listOfPanels[i];
            }
        }
        //in any case somehow smth wrong happen, just return the first one in the list
        return listOfPanels[0];
    }
}
