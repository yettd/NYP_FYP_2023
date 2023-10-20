using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CM : MonoBehaviour
{
    // Start is called before the first frame update
    private AudioManager audioManager;
    public GameObject hygineAndTherapy;
    public GameObject procedure;
    void Start()
    {
        audioManager = AudioManager.Instance;

        if (audioManager == null)
        {
            Debug.LogError("AudioManager not found.");
        }

        audioManager.PlaySFX(3);
        procedure.SetActive(false);
        hygineAndTherapy.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
