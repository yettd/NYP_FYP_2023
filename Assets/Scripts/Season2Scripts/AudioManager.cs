using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Background Music")]
    public AudioClip[] backgroundMusicTracks;
    private AudioSource musicSource;

    [Header("Sound Effects")]
    public AudioClip[] soundEffects; 
    public AudioClip pingSound; 
    public AudioClip hurtSound;
    private AudioSource sfxSource;

    private const string MusicVolumeKey = "MusicVolume";
    private const string SFXVolumeKey = "SFXVolume";

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

        musicSource = gameObject.AddComponent < AudioSource();
        sfxSource = gameObject.AddComponent < AudioSource();

        musicSource.loop = true;

        // Load music and SFX volumes from PlayerPrefs
        SetMusicVolume(PlayerPrefs.GetFloat(MusicVolumeKey, 1f));
        SetSFXVolume(PlayerPrefs.GetFloat(SFXVolumeKey, 1f));
    }

    public void PlayBackgroundMusic(int trackIndex)
    {
        if (trackIndex >= 0 && trackIndex < backgroundMusicTracks.Length)
        {
            musicSource.clip = backgroundMusicTracks[trackIndex];
            musicSource.Play();
        }
    }

    public void SetMusicVolume(float volume)
    {
        musicSource.volume = volume;
        PlayerPrefs.SetFloat(MusicVolumeKey, volume);
    }

    public void SetSFXVolume(float volume)
    {
        sfxSource.volume = volume;
        PlayerPrefs.SetFloat(SFXVolumeKey, volume);
    }

    public void PlaySFX(int sfxIndex)
    {
        if (sfxIndex >= 0 && sfxIndex < soundEffects.Length)
        {
            sfxSource.PlayOneShot(soundEffects[sfxIndex]);
        }
    }

    // Public method to play the "Ping" sound effect
    public void PlayPingSound()
    {
        PlaySFX(Array.IndexOf(soundEffects, pingSound));
    }

    // Public method to play the "Hurt" sound effect
    public void PlayHurtSound()
    {
        PlaySFX(Array.IndexOf(soundEffects, hurtSound));
    }
}
