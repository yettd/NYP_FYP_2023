using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotateJaws : MonoBehaviour
{

    float x;
    float newx;
    bool down;
    float smooth=1;
    float diffx;
    float totalDegree=0;
    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        down = true;

        Debug.Log("down");
        x = Input.mousePosition.x;
    }

    private void OnMouseDrag()
    {
        newx = Input.mousePosition.x;

        diffx = newx - x;
        x = newx;
        totalDegree += diffx * smooth;
        totalDegree = Mathf.Clamp(totalDegree, -90f, 90f);
        gameObject.transform.rotation = Quaternion.Euler(transform.eulerAngles.x,  totalDegree, transform.eulerAngles.z) ;

    }

    private void OnMouseUp()
    {
        down = false;
    }
}
