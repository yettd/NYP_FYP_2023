using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Instruction_Text : MonoBehaviour
{
    public TextMeshProUGUI tmpro;
    // Start is called before the first frame update
    Vector3 ogpos;
    string ogtext;
    RectTransform rt;
    private void Start()
    {
        rt= GetComponent<RectTransform>();
        teethMan.tm.CT+=textChange;
        teethMan.tm.dis = becomeGone;
        ogpos = rt.anchoredPosition;
    }

    public void becomeGone()
    {
        gameObject.SetActive(false);
    }

    public void textChange(string t, bool a)
    {
        tmpro.text = t;
        if(a )
        {
            rt.anchoredPosition= new Vector3(0, 150, 0);
        }
        else
        {
            rt.anchoredPosition = ogpos;
        }
    }
   
   
}
