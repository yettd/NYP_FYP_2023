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


    Vector2 Xy;
    Vector2  newXY;
    Vector2 diffXY;
    Vector2 totalDegree= Vector2.zero;
 
    private void Update()
    {
        if (Input.GetMouseButton(0) || Input.touchCount > 0)
        {
            if (!down)
            {
                Xy = Input.mousePosition;
                down = true;
            }

            if (!GetComponent<SphereCollider>().enabled)
            {
                return;
            }
            newXY.x = Input.mousePosition.x;
            newXY.y = Input.mousePosition.y;
            diffXY = newXY - Xy;
            Xy = newXY;
            totalDegree += diffXY * smooth;


            if (cameraChanger.Instance.ZoomIn.gameObject.activeSelf)
            {
                cameraChanger.Instance.RoateAround(totalDegree.x, totalDegree.y);
            }
            else
            {
                transform.rotation = Quaternion.Euler(transform.eulerAngles.x, totalDegree.x, transform.eulerAngles.z);
            }
        }

        if(Input.GetMouseButtonUp(0))
        {
            if(cameraChanger.Instance.GetZoom())
            {
                totalDegree = Vector2.zero;
            }
            totalDegree = Vector2.zero;
            down = false;
        }
    }


    public void changeCamview(Camera cam)
    {
        this.cam = cam;
    }

}
