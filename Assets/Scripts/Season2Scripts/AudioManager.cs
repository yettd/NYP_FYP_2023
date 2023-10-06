using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public AudioClip PingSound;
    public AudioSource audioSource;

    private const string VolumeKey = "Volume";

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        audioSource = GetComponent<AudioSource>();
        SetVolume(PlayerPrefs.GetFloat(VolumeKey, 1f));
    }
    public void SetVolume(float volume)
    {
        Debug.Log(volume);
        audioSource.volume = volume;
        PlayerPrefs.SetFloat(VolumeKey, volume); 
    }
    public void PlayClip(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
    //public void PlayAudioOneShot(AudioClip clip, float volume = 1f)
    //{
    //    audioSource.PlayOneShot(clip, volume);
    //}
    public void PlayPingSound()
    {
        //PlayAudioOneShot(PingSound, 1f);
        Debug.Log("Ping Played");
        PlayClip(PingSound);
    }

}
