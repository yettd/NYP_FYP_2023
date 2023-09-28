using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotateJaws : MonoBehaviour
{

    float smooth=0.1f;
    public Camera cam;
    float x;
    float newx;
    bool down;
    float diffx;
    float totalDegree = 0;
    private void OnMouseDown()
    {
        down = true;

        x = Input.mousePosition.x;
    }
    private void OnMouseDrag()
    {
        newx = Input.mousePosition.x;
        if (!GetComponent<SphereCollider>().enabled)
        {
            return;
        }
        float rotX = Input.GetAxis("Mouse X") * smooth;
        float rotY = Input.GetAxis("Mouse Y") * smooth;

        diffx = newx - x;
        x = newx;
        totalDegree += diffx * smooth;
        totalDegree = Mathf.Clamp(totalDegree, -90, 90);
        transform.rotation = Quaternion.Euler(transform.eulerAngles.x, totalDegree, transform.eulerAngles.z);
        Debug.Log(totalDegree);


    }

    private void OnMouseUp()
    {
        down = false;
    }
    //private void OnMouseDrag()
    //{
    //    //if(!GetComponent<SphereCollider>().enabled)
    //    //{
    //    //    return;
    //    //}
    //    //float rotX = Input.GetAxis("Mouse X") * smooth;
    //    //float rotY = Input.GetAxis("Mouse Y") * smooth;

    //    //Vector3 right = Vector3.Cross(cam.transform.up, transform.position - cam.transform.position);
    //    //Vector3 up = Vector3.Cross(transform.position - cam.transform.position, right);
    //    //transform.rotation = Quaternion.AngleAxis(-rotX, up) * transform.rotation;
    //    //transform.rotation = Quaternion.AngleAxis(rotY, right) * transform.rotation;


    //}
    public void changeCamview(Camera cam)
    {
        this.cam = cam;
    }

}
