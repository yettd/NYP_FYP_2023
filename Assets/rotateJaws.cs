using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotateJaws : MonoBehaviour
{

    float smooth=1f;
    public Camera cam;

    private void OnMouseDrag()
    {
        if(!GetComponent<SphereCollider>().enabled)
        {
            return;
        }
        float rotX = Input.GetAxis("Mouse X") * smooth;
        float rotY = Input.GetAxis("Mouse Y") * smooth;

        Vector3 right = Vector3.Cross(cam.transform.up, transform.position - cam.transform.position);
        Vector3 up = Vector3.Cross(transform.position - cam.transform.position, right);
        transform.rotation = Quaternion.AngleAxis(-rotX, up) * transform.rotation;
        transform.rotation = Quaternion.AngleAxis(rotY, right) * transform.rotation;


    }

}
