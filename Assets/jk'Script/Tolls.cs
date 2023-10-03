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
    float zPos;
    float velocity;
    Vector3 oldPos;
    protected Ray ray;
    public float ThresholdForMax;
    protected virtual void Start()
    {
        zPos = transform.position.z;
        oldPos = transform.position;
    }
    private void Update()
    {

    }
    // Start is called before the first frame update
    private void OnMouseDrag()
    {
        float dis = Vector3.Distance(transform.position, oldPos);
        if (dis / Time.deltaTime != 0)
        {

            velocity = dis / Time.deltaTime;
        }
        oldPos = transform.position;


        Vector3 pos = cameraChanger.Instance.GetCurrentCam().ScreenToWorldPoint(Input.mousePosition) + cameraChanger.Instance.GetCurrentCam().transform.forward;
        transform.parent.transform.position = new Vector3(pos.x, pos.y, pos.z);
        //if(Mathf.Abs(transform.parent.transform.eulerAngles.x) >= 90)
        //{
        //    transform.parent.transform.position = new Vector3(pos.x, oldPos.y, pos.z);
        //}
        //else
        //{

        //}
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
        ray = new Ray(usePoint.position, cameraChanger.Instance.GetCurrentCam().transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (true /* && velocity<=ThresholdForMax*/)
            {
                usetool(hit);
            }
        
        }
        
    }
    void OnDrawGizmosSelected()
    {
        // Draws a 5 unit long red line in front of the object
        Gizmos.color = Color.red;
        Vector3 direction = transform.TransformDirection(Vector3.forward) * 5;
        Gizmos.DrawRay(ray);
    }
    protected virtual void usetool( RaycastHit hit)
    {
        
    }
}
