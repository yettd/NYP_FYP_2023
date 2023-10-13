using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public AudioClip backgroundMusic;
    public AudioClip PingSound;

    private AudioSource musicSource;
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

        musicSource = gameObject.AddComponent<AudioSource>();
        sfxSource = gameObject.AddComponent<AudioSource>();

        musicSource.loop = true;
        musicSource.clip = backgroundMusic;

        SetMusicVolume(PlayerPrefs.GetFloat(MusicVolumeKey, 1f));
        SetSFXVolume(PlayerPrefs.GetFloat(SFXVolumeKey, 1f));

        PlayBackgroundMusic();
    }

    public void PlayBackgroundMusic()
    {
        musicSource.Play();
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

    public void PlaySFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }

    public void PlayPingSound()
    {
        PlaySFX(PingSound);
    }
}
