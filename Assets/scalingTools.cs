using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scalingTools : Tolls
{
    private AudioManager audioManager;
    [SerializeField] RectTransform canvas;
    [SerializeField] Texture2D _brush;
    // Start is called before the first frame update
    private void awake()
    {
        if (audioManager == null)
        {
            Debug.LogError("AudioManager not found.");
        }

    }
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
            audioManager.PlaySFX(1);
            Debug.Log("asdasd");
            TDC.Clean(hit, _brush,ray);
        }
    }
}
