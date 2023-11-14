using UnityEngine;
using UnityEngine.UI;

public class VolumeControl : MonoBehaviour
{
    [SerializeField] private Slider musicVolumeSlider;
    [SerializeField] private Slider sfxVolumeSlider;
    [SerializeField] private Button muteSoundButton;
    [SerializeField] private Button muteMusicButton;
    [SerializeField] private Sprite soundMutedSprite;
    [SerializeField] private Sprite soundUnmutedSprite;
    [SerializeField] private Sprite musicMutedSprite;
    [SerializeField] private Sprite musicUnmutedSprite;

    private bool isSoundMuted = false;
    private bool isMusicMuted = false;

    private void Start()
    {
        musicVolumeSlider.value = PlayerPrefs.GetFloat("MusicVolume", 1f);
        sfxVolumeSlider.value = PlayerPrefs.GetFloat("SFXVolume", 1f);

        isMusicMuted = PlayerPrefs.GetInt("IsMusicMuted", 0) == 1;
        isSoundMuted = PlayerPrefs.GetInt("IsSoundMuted", 0) == 1;

        AudioManager.Instance.MuteMusic(isMusicMuted);
        AudioManager.Instance.MuteSFX(isSoundMuted);

        UpdateMusicMuteButton();
        UpdateSoundMuteButton();

        musicVolumeSlider.onValueChanged.AddListener(AdjustMusicVolume);
        sfxVolumeSlider.onValueChanged.AddListener(AdjustSFXVolume);
        muteSoundButton.onClick.AddListener(ToggleSoundMute);
        muteMusicButton.onClick.AddListener(ToggleMusicMute);
    }

    private void AdjustMusicVolume(float volume)
    {
        AudioManager.Instance.SetMusicVolume(volume);
        isMusicMuted = false; 
        UpdateMusicMuteButton();
    }

    private void AdjustSFXVolume(float volume)
    {
        AudioManager.Instance.SetSFXVolume(volume);
        isSoundMuted = false; 
        UpdateSoundMuteButton();
    }

    private void ToggleSoundMute()
    {
        isSoundMuted = !isSoundMuted;
        AudioManager.Instance.MuteSFX(isSoundMuted);
        PlayerPrefs.SetInt("IsSoundMuted", isSoundMuted ? 1 : 0);
        UpdateSoundMuteButton();
        if (!isSoundMuted)
        {
            sfxVolumeSlider.value = PlayerPrefs.GetFloat("SFXVolume", 1f);
        }
    }

    private void ToggleMusicMute()
    {
        isMusicMuted = !isMusicMuted;
        AudioManager.Instance.MuteMusic(isMusicMuted);
        PlayerPrefs.SetInt("IsMusicMuted", isMusicMuted ? 1 : 0);
        UpdateMusicMuteButton();
        if (!isMusicMuted)
        {
            musicVolumeSlider.value = PlayerPrefs.GetFloat("MusicVolume", 1f);
        }
    }
    private void UpdateSoundMuteButton()
    {
        if (isSoundMuted)
        {
            muteSoundButton.image.sprite = soundMutedSprite;
        }
        else
        {
            muteSoundButton.image.sprite = soundUnmutedSprite;
        }
    }
    private void UpdateMusicMuteButton()
    {
        if (isMusicMuted)
        {
            muteMusicButton.image.sprite = musicMutedSprite;
        }
        else
        {
            muteMusicButton.image.sprite = musicUnmutedSprite;
        }
    }
}
