using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class contour : Tolls
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
        fillingCure fc;
        drawStop ds;
        hitPoint hp;
        toothFilling TDC;
        hit.collider.TryGetComponent<toothFilling>(out TDC);
        if (TDC)
        {
            TDC.GoToStep(hit);
            return;
        }

        hit.collider.TryGetComponent<drawStop>(out ds);
        if (ds)
        {
            ds.stopDrawing();
        }
        hit.collider.TryGetComponent<hitPoint>(out hp);
        if (hp)
        {
            hp.hit();
        }
        hit.collider.TryGetComponent<fillingCure>(out fc);
        if (fc)
        {
            fc.Stuff(hit);
        }
    }
}
