using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class drawStop : MonoBehaviour
{
    public fillingCure d;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //if the mouse go over the gameObject will reset the progress for contour
    //Can be ajusted for easier on the gameObject by changing the size
    private void OnMouseDrag()
    {
        d.STOP();
    }
    private void OnMouseOver()
    {
        d.STOP();
    }

    public void stopDrawing()
    {
        d.STOP();
    }
}
