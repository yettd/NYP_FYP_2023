using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    public static AudioPlayer Instance { get; private set; }

    [SerializeField] private List<AudioClip> audioClips = new List<AudioClip>();

    public AudioSource audioSrc;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
        audioSrc = GetComponent<AudioSource>();
    }

    public void PlayAudioOneShot(int index, float volume = 1f)
    {
        audioSrc.PlayOneShot(audioClips[index], volume);
    }
}
