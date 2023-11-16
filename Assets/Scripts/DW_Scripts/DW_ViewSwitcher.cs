using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DW_ViewSwitcher : MonoBehaviour
{
    private DW_AutoToolScaler autoScalerScript;

    private int assignCam = 0;
    private int currentCam = 0;

    void Start()
    {
        // Inti component of auto tool scale
        autoScalerScript = GetComponent<DW_AutoToolScaler>();    
    }

    void Update()
    {
        // Update cam as of change of viewport
        SwitchCam();

        // Update the tool scale which consist the given data
        //autoScalerScript.AutoToolScale();
    }

    #region SETUP
    private Camera GetCurrentViewport()
    {
        int scaleDataLength = autoScalerScript.get_scaleData.Length;

        // Finding of active cam and return the value to the assigned variable
        for (int cam = 0; cam < scaleDataLength; cam++)
            if (autoScalerScript.get_scaleData[cam].cameraRef.gameObject.activeInHierarchy)
                return autoScalerScript.get_scaleData[cam].cameraRef;

        // Return its default value to the assigned variable
        return autoScalerScript.get_scaleData[scaleDataLength - 1].cameraRef;
    }

    private int GetCamProperty()
    {
        int scaleDataLength = autoScalerScript.get_scaleData.Length;

        // Finding of active cam and return the index to the assigned variable
        for (int camIndex = 0; camIndex < scaleDataLength; camIndex++)
            if (autoScalerScript.get_scaleData[camIndex].cameraRef.gameObject.activeInHierarchy)
                return camIndex;

        // Return its default value to the assigned variable
        return 0;
    }

    private float GetCamToMapToolDepth()
    {
        int scaleDataLength = autoScalerScript.get_scaleData.Length;

        // Finding of active cam and return the depth to the assigned variable
        for (int depth = 0; depth < scaleDataLength; depth++)
            if (autoScalerScript.get_scaleData[depth].cameraRef.gameObject.activeInHierarchy)
                return autoScalerScript.get_scaleData[depth].camDepthOffset;

        // Return its default value to the assigned variable
        return autoScalerScript.get_scaleData[scaleDataLength - 1].camDepthOffset;
    }
    #endregion

    #region MAIN
    public bool GetMainCamViewport()
    {
        // Get the port to the main camera
        return GetCurrentViewport() == autoScalerScript.get_scaleData[autoScalerScript.get_scaleData.Length - 1].cameraRef;
    }

    public void SwitchCam()
    {
        // Set the value of the camera to use
        assignCam = GetCamProperty();

        // Update it when its changes of camera port
        if (assignCam != currentCam)
        {
            // Find the closest active camera that been changes and update it to DW_Camera
            transform.position = GetCurrentViewport().gameObject.transform.position;
            GetComponent<Camera>().orthographicSize = GetCurrentViewport().gameObject.GetComponent<Camera>().orthographicSize;
            // Set the assign value of the camera to next time use again
            currentCam = assignCam;
        }
    }

    public float GetToolDepthMapToCam()
    {
        // Get the depth of the tool through its given value
        return GetCamToMapToolDepth();
    }
    #endregion
}
