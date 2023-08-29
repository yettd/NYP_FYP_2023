using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToolInfoVideoBtnColor : MonoBehaviour
{
    public Sprite dtVideo;
    public Sprite dhVideo;

    //On Enable, change the sprite of this component based on the DTH ID - DH / DT
    private void OnEnable()
    {
        if (!ButtonReferenceManager.Instance)
            return;
         if (ButtonReferenceManager.Instance.storedDTHButtonID == DTHEnum.DH)
         {
            GetComponent<Image>().sprite = dhVideo;
         }
         else if(ButtonReferenceManager.Instance.storedDTHButtonID == DTHEnum.DT)
         {
            GetComponent<Image>().sprite = dtVideo;
         }
    }
}
