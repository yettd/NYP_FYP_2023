using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuShowTeachGesture : MonoBehaviour
{ 
    public GameObject BlockerGameObject;
    public Toggle toggle;

    private static bool shownMainMenuGestureGuide = false;
 
    private void Awake()
    {
        if (BlockerGameObject)
        {
            if (!shownMainMenuGestureGuide)//Check if the user has pressed "Do not show again" thingy
                BlockerGameObject.SetActive(true);
            else
                BlockerGameObject.SetActive(false);
        }
    }

    public void SetdoNotShowAgainChecked()
    {
        shownMainMenuGestureGuide = toggle.isOn;
    }

    public void HideBlocker()
    {
        shownMainMenuGestureGuide = true;
        BlockerGameObject.SetActive(false);
    }

}
