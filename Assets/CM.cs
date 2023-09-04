using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CM : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject hygineAndTherapy;
    public GameObject procedure;
    void Start()
    {
        procedure.SetActive(true);
        hygineAndTherapy.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
