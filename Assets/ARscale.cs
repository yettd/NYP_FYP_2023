using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ARscale : ARcontrol
{
    // Start is called before the first frame update
    [SerializeField]
    Slider scaleSlider;
    Vector3 ogscale;
    private void Start()
    {
        scaleSlider.value = 1;
    }

    public override void assignOTC(GameObject OTC)
    {
        base.assignOTC(OTC);
        ogscale = OTC.transform.localScale;
    }

    public void changeScale()
    {
        objectToControl.transform.localScale = ogscale* scaleSlider.value;
    }

}
