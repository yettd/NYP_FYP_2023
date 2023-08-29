using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonID : MonoBehaviour
{

    public ButtonENUM buttonID;
    public DTHEnum dthButtonID;

    public void AssignBackButtonID()
    {
        ButtonReferenceManager.Instance.storedButtonID = buttonID;
    }

    public void AssignDTHButtonID()
    {
        ButtonReferenceManager.Instance.storedDTHButtonID = dthButtonID;
        //Debug.Log("stored dthButtonID with" + dthButtonID);

    }
}
