using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class cameraChanger : MonoBehaviour
{
   
    bool changed;
    private Camera currentCam;
    public Camera[] allCam;
    int current = 0;
    public GameObject flip;
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
        return allCam[current];
    }

    public static cameraChanger Instance;

    private void Awake()
    {
        Instance = this;
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
   


}
