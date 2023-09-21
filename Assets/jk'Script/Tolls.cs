using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tolls : MonoBehaviour
{
    [SerializeField] Camera cam;
    [SerializeField] Transform usePoint;
    protected bool letgoToUse=false;

    GameObject toohtSelected;
    Vector3 hitpos;

    protected virtual void Start()
    {
       
    }
    private void Update()
    {

    }
    // Start is called before the first frame update
    private void OnMouseDrag()
    {
        Vector3 pos = cameraChanger.Instance.GetCurrentCam().ScreenToWorldPoint(Input.mousePosition);
        transform.parent.transform.position = new Vector3(pos.x, pos.y, transform.position.z);
        Ray ray = cameraChanger.Instance.GetCurrentCam().ScreenPointToRay(cameraChanger.Instance.GetCurrentCam().WorldToScreenPoint(pos));
        if (letgoToUse == false)
        {
            raycastToSelcetTooth();
        }
    }

    private void OnMouseUp()
    {
        if(letgoToUse)
        {
            raycastToSelcetTooth();
        }
    }

    private void raycastToSelcetTooth()
    {
        if (Physics.Raycast(usePoint.position, cameraChanger.Instance.GetCurrentCam().transform.forward, out RaycastHit hit))
        {
            TeethDirtClean TDC;
            hit.collider.TryGetComponent<TeethDirtClean>(out TDC);
            if (TDC)
            {

                usetool(TDC, hit);
            }
            //if (toohtSelected !=hit.collider.gameObject)
            //{
            //    Debug.Log("asdasdasd");
            //    hitpos = hit.point;
            //    toohtSelected = hit.collider.gameObject;
            //}
            //if(toohtSelected)
            //{
            //    if (Physics.Raycast(usePoint.position, hitpos - usePoint.transform.position , out RaycastHit hitTooth))
            //    {
              
            //    }
            //}
        }
    }

    protected virtual void usetool(TeethDirtClean TDC, RaycastHit hit)
    {
        
    }
}
