using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ARSpawnShowTeachGesture : MonoBehaviour
{
    public GameObject BlockerGameObject;
    public Toggle toggle;


    private static bool shownARSpawnGestureGuide = false;
    //private bool playerPrefdoNotShowAgainChecked;
    private void Awake()
    {
        //playerPrefdoNotShowAgainChecked = (PlayerPrefs.GetInt("doNotShowAgainChecked") != 0);

        if (BlockerGameObject)
        {
            if (!shownARSpawnGestureGuide)
                BlockerGameObject.SetActive(true);
            else
            {
                BlockerGameObject.SetActive(false);
            }
        }
       
    }

    public void SetdoNotShowAgainChecked()
    {
        shownARSpawnGestureGuide = toggle.isOn;

    }

    public void HideBlocker()
    {
        shownARSpawnGestureGuide = true;
        BlockerGameObject.SetActive(false);
    }

}
