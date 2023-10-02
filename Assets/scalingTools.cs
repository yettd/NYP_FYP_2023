using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scalingTools : Tolls
{
    [SerializeField] RectTransform canvas;
    [SerializeField] Texture2D _brush;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        letgoToUse = false;
    }

    // Update is called once per frame
    protected override void usetool( RaycastHit hit)
    {

        TeethDirtClean TDC;


        hit.collider.TryGetComponent<TeethDirtClean>(out TDC);
        if(TDC)
        {

        TDC.Clean(hit, _brush,ray);
        }
    }
}
