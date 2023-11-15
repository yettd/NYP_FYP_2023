using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class rotateJaws : MonoBehaviour
{

    float smooth=0.1f;
    public Camera cam;
    float x;
    float newx;
    bool down;
    float diffx;

    public bool overUI;

    Vector2 Xy;
    Vector2  newXY;
    Vector2 diffXY;
    Vector2 totalDegree= Vector2.zero;
 
    private void Update()
    {
       
        if ((Input.GetMouseButton(0) || Input.touchCount > 0)&& overUI==false)
        {

            if (!down)
            {
                Xy = Input.mousePosition;
                down = true;
                totalDegree = Vector2.zero;
                return;
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
                cameraChanger.Instance.RoateAround(totalDegree.x, -totalDegree.y);
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
            down = false;
        }
    }


    public void changeCamview(Camera cam)
    {
        this.cam = cam;
    }

}
