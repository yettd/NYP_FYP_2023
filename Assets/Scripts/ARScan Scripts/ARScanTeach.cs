using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ARScanTeach : MonoBehaviour
{
    [SerializeField] Image darkBackground;
    [SerializeField] GameObject graceGameObject;
    [SerializeField] GameObject textGameObject;

    public bool ShowGuide = true;
    public void PressedGotIt()
    {
        darkBackground.enabled = false;
        graceGameObject.SetActive(false);
        textGameObject.SetActive(false);
    }

    private void Awake()
    {
        if (ShowGuide)
        {
            darkBackground.enabled = true;
            graceGameObject.SetActive(true);
            textGameObject.SetActive(true);
        }
    }
}
