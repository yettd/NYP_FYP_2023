using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DW_ViewSwitcher : MonoBehaviour
{
    [SerializeField] private Camera mainCam;
    [SerializeField] private Camera subCam;
    [SerializeField] private Camera microCam;

    private int currentCam;

    // Start is called before the first frame update
    void Start()
    {
        StartDefaultViewport();
    }

    #region SETUP
    private void StartDefaultViewport()
    {
        currentCam = 0;
    }

    private Vector3 GetCurrentViewport(int port)
    {
        switch (port)
        {
            case 1:
                return subCam.gameObject.transform.position;

            case 2:
                return microCam.gameObject.transform.position;

            default:
                return mainCam.gameObject.transform.position;
        }
    }
    #endregion

    #region MAIN
    public void SwitchViewport(int port)
    {
        currentCam = port;
        transform.position = GetCurrentViewport(port);     
    }
    #endregion
}
