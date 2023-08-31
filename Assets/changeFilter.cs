using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changeFilter : MonoBehaviour
{
    public GameObject[] filterpanel;
    // Start is called before the first frame update
    public void Change()
    {
        foreach (GameObject a in filterpanel)
        {
            a.SetActive(!a.activeSelf);
        }
    }
}
