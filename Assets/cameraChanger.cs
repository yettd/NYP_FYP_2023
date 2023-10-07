using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class cameraChanger : MonoBehaviour
{
   
    bool changed;
    private Camera currentCam;
    public Camera[] allCam;
    public Camera ZoomIn;
    int current = 0;
    public GameObject flip;
    bool zoom = false;
    GameObject focusOn;
    // Start is called before the first frame update
    
    public void CC()
    {
        current++;
        if(current>=allCam.Length)
        {
            current = 0;
        }
        for (int i = 0; i < allCam.Length; i++)
        {
            if(i!=current)
            {
                allCam[i].gameObject.SetActive(false);
            }
            else
            {
                allCam[i].gameObject.SetActive(true);
            }
        }
    }
    public Camera GetCurrentCam()
    {
        return currentCam;
        //return allCam[current];
    }

    public static cameraChanger Instance;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        currentCam = allCam[0];
    }

    public void closeCamera()
    {
        for (int i = 0; i < allCam.Length; i++)
        {
            allCam[i].gameObject.SetActive(false);
           
        }
    }
    public void startCamera()
    {
        current = 0;
        allCam[0].gameObject.SetActive(true);
    }

    public void ZoomInCam( GameObject g)
    {
        ZoomIn.enabled = true;
        zoom = true;
        focusOn = g;
        ZoomIn.gameObject.SetActive(true);
        
        currentCam = ZoomIn;
        Debug.Log(cameraChanger.Instance.GetCurrentCam().name);
        ZoomIn.transform.position =new Vector3(g.transform.position.x, -267.1f, -35);
        Quaternion _lookRotation =Quaternion.LookRotation((g.transform.position - ZoomIn.transform.position).normalized);
        ZoomIn.transform.rotation = _lookRotation;

    }

    public void ZoomOutCam()
    {
        ZoomIn.enabled = false;
        ZoomIn.gameObject.SetActive(false);
        zoom = false;
    }

    public void RoateAround(float degree,float y)
    {
        ZoomIn.gameObject.transform.RotateAround(focusOn.transform.position, Vector3.up, degree * Time.deltaTime);
        ZoomIn.gameObject.transform.RotateAround(focusOn.transform.position, Vector3.right, y * Time.deltaTime);
        ZoomIn.transform.rotation = Quaternion.Euler(ZoomIn.transform.eulerAngles.x, ZoomIn.transform.eulerAngles.y, 0);
        //ZoomIn.gameObject.transform.LookAt(focusOn.transform.position,transform.up);
    }

    public GameObject GetfocusOn()
    {
        return focusOn;
    }


}
