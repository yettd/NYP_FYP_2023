using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fillingTool : Tolls
{
    [SerializeField] RectTransform canvas;
    [SerializeField] Texture2D _brush;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
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
}
