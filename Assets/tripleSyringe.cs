using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class tripleSyringe : fillingTool
{
    public Image[] buttonPress;
    // Start is called before the first frame update
    public bool WaterBlow = false;
    Canvas c;
    protected override void Start()
    {
        base.Start();
        foreach (Transform t in transform.parent)
        {

            if (t.TryGetComponent<Canvas>(out c))
            {
            
                c.gameObject.transform.parent = null;
            }
        }
        //letgoToUse = false;
    }

    // Update is called once per frame
    protected override void usetool(RaycastHit hit)
    {

        base.usetool(hit);
    }

    private void OnDestroy()
    {
        Destroy(c.gameObject);
    }

    public void setWashBlow(bool blow)
    {
        WaterBlow=blow; 
    }
    public void buttonePress(Image button)
    {
        foreach (Image t in buttonPress)
        {
            t.color= new Vector4(1f, 1f, 1f, 1f);
        }
        button.color = new Vector4(0.509434f, 0.509434f, 0.509434f, 0.5f);
    }

}
