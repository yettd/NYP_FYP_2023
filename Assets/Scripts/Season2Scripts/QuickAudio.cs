using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickAudio : MonoBehaviour
{
    private AudioManager audioManager;

    public void Awake()
    {
        audioManager = AudioManager.Instance;

        if (audioManager == null)
        {
            Debug.LogError("AudioManager not found.");
        }
    }

    void Update()
    {
        
    }

    public void playClickSound()
    {
        if (audioManager != null)
        {
            audioManager.PlaySFX(1);
        }
    }
}
