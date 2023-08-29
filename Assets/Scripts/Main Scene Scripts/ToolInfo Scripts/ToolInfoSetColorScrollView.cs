using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ToolInfoSetColorScrollView : MonoBehaviour
{
    [SerializeField] TopParentColor topParentColor;

    private void OnEnable()
    {
        //On Enable, just change to Color of this component into the topParentColor Color
        GetComponent<Image>().color = topParentColor.GetcurrentTopParentColor();
    }
}
