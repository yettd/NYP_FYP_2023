using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class teethMan : MonoBehaviour
{
    // Start is called before the first frame update
    public static teethMan tm;
    public delegate void ClickOn(string TeethName);
    public delegate void BackToOriginal();
    public delegate void ChangeText(string text, bool a);
    public delegate void Disappear();
    public delegate void disablehoverover(bool a);

    public ClickOn CO;
    public BackToOriginal Back;
    public ChangeText CT;
    public Disappear dis;
    public ClickOn changeToolColor;
    public disablehoverover DOO;

    public string s;
    public bool ZoomIn=false;
    private void Awake()
    {
        tm = this;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CallClickOn(string teethOn)
    {
        CO(teethOn);
        ZoomIn = true;
    }
    public void Changetoolcolor(string a)
    {
        changeToolColor(a);
    }

    public void back()
    {
        Back();
        ZoomIn = false;
    }
    public void ct(string t , bool a)
    {
        CT(t,a);
    }
    public void backText( bool a)
    {
        CT(s, a);
    }
    public void gone(bool a)
    {
        dis();
    }
    public void callDOO(bool a)
    {
        //DOO(a);
    }
}
