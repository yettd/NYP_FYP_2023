using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public AudioClip[] backgroundMusicTracks;
    public AudioClip[] sfxClips;
    //public AudioClip pingSound;

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
        bool isMusicMuted = PlayerPrefs.GetInt("IsMusicMuted", 0) == 1;
        bool isSoundMuted = PlayerPrefs.GetInt("IsSoundMuted", 0) == 1;

        musicSource = gameObject.AddComponent<AudioSource>();
        sfxSource = gameObject.AddComponent <AudioSource>();

        musicSource.loop = true;

        SetMusicVolume(PlayerPrefs.GetFloat(MusicVolumeKey, 1f));
        SetSFXVolume(PlayerPrefs.GetFloat(SFXVolumeKey, 1f));
        MuteMusic(isMusicMuted);
        MuteSFX(isSoundMuted);

        PlayBackgroundMusic(0); 
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
        if (sfxIndex >= 0 && sfxIndex < sfxClips.Length)
        {
            sfxSource.PlayOneShot(sfxClips[sfxIndex]);
        }
    }
    public void PlayCelebrateSFX()
    {
        if(!sfxSource.isPlaying)
        {
         sfxSource.PlayOneShot(sfxClips[13]);

        }

    }

    //public void PlayPingSound()
    //{
    //    PlaySFX(Array.IndexOf(sfxClips, pingSound));
    //}

    public void MuteMusic(bool mute)
    {
        musicSource.mute = mute;
    }

    public void MuteSFX(bool mute)
    {
        sfxSource.mute = mute;
    }

    public void stopPlaying()
    {
        sfxSource.Stop();
    }
}
