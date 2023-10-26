using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tripleSyringe : fillingTool
{
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

        toothFilling TDC;


        hit.collider.TryGetComponent<toothFilling>(out TDC);
        if (TDC)
        {
            TDC.GoToStep(hit);
        }
    }

    private void OnDestroy()
    {
        Destroy(c.gameObject);
    }

    public void setWashBlow(bool blow)
    {
        WaterBlow=blow; 
    }
    
}
