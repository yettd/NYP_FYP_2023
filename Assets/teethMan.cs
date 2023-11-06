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

    public ClickOn CO;
    public BackToOriginal Back;
    public ChangeText CT;

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

    public void back()
    {
        Back();
        ZoomIn = false;
    }
    public void ct(string t , bool a)
    {
        CT(t,a);
    }
}
