using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class displayARControl : MonoBehaviour
{
    public RectTransform control;

    public void displayControl()
    {
        if(control.anchoredPosition.y >0)
        {
            control.anchoredPosition = new Vector3(0, -55, 0);
        }
        else
        {

            control.anchoredPosition = new Vector3(0, 55, 0);
        }
    }
}
