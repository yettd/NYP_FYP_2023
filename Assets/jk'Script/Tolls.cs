using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tolls : MonoBehaviour
{
    [SerializeField] Camera cam;
    [SerializeField] Transform usePoint;
    protected bool letgoToUse=false;

    protected virtual void Start()
    {
        cam = GameObject.Find("Camera").GetComponent<Camera>();
    }
    // Start is called before the first frame update
    private void OnMouseDrag()
    {
        Vector3 pos = cam.ScreenToWorldPoint(Input.mousePosition);
        transform.parent.transform.position = new Vector3(pos.x, pos.y, transform.position.z);
        Ray ray = cam.ScreenPointToRay(cam.WorldToScreenPoint(pos));
        if (letgoToUse == false)
        {
            raycastStuff();
        }
    }

    private void OnMouseUp()
    {
        if(letgoToUse)
        {
            raycastStuff();
        }
    }

    private void raycastStuff()
    {
        if (Physics.Raycast(usePoint.position, cam.transform.forward, out RaycastHit hit))
        {
            Debug.Log("asd");
            TeethDirtClean TDC;
            hit.collider.TryGetComponent<TeethDirtClean>(out TDC);
            if (TDC)
            {

                usetool(TDC,hit);
            }
        }
    }

    protected virtual void usetool(TeethDirtClean TDC, RaycastHit hit)
    {
        
    }
}
