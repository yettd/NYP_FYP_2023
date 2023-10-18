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
        AudioManager.Instance.SetSFXVolume(isSoundMuted ? 0f : sfxVolumeSlider.value);
        UpdateSoundMuteButton();
    }

    private void ToggleMusicMute()
    {
        isMusicMuted = !isMusicMuted;
        AudioManager.Instance.SetMusicVolume(isMusicMuted ? 0f : musicVolumeSlider.value);
        UpdateMusicMuteButton();
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
