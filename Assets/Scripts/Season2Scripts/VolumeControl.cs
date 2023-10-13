using UnityEngine;
using UnityEngine.UI;

public class VolumeControl : MonoBehaviour
{
    [SerializeField] private Slider musicVolumeSlider;
    [SerializeField] private Slider sfxVolumeSlider;

    private void Start()
    {
        musicVolumeSlider.value = PlayerPrefs.GetFloat("MusicVolume", 1f);
        sfxVolumeSlider.value = PlayerPrefs.GetFloat("SFXVolume", 1f);

        musicVolumeSlider.onValueChanged.AddListener(AdjustMusicVolume);
        sfxVolumeSlider.onValueChanged.AddListener(AdjustSFXVolume);
    }

    private void AdjustMusicVolume(float volume)
    {
        AudioManager.Instance.SetMusicVolume(volume);
    }

    private void AdjustSFXVolume(float volume)
    {
        AudioManager.Instance.SetSFXVolume(volume);
    }
}
