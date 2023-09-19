using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class displayARControl : MonoBehaviour
{
    public RectTransform control;
    public RectTransform sidepanal;

    public void displayControl()
    {
        if(control.anchoredPosition.y >0)
        {
            control.DOAnchorPos(new Vector3(0, -200, 0),1);
            sidepanal.DOAnchorPos(new Vector3(200, 0, 0), 1);

        }
        else
        {

            control.DOAnchorPos(new Vector3(0, 1, 0), 1);
            sidepanal.DOAnchorPos(new Vector3(0, 0, 0), 1);
        }
    }
}
