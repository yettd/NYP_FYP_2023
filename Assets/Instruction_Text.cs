using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Instruction_Text : MonoBehaviour
{
    public TextMeshProUGUI tmpro;
    // Start is called before the first frame update
    Vector3 ogpos;
    private void Start()
    {
        teethMan.tm.CT+=textChange;
        ogpos=transform.localPosition;
    }

    public void textChange(string t, bool a)
    {
        tmpro.text = t;
        if(a )
        {
            transform.localPosition= new Vector3(0,transform.localPosition.y,0);
        }
        else
        {
            transform.localPosition = ogpos;
        }
    }
}
