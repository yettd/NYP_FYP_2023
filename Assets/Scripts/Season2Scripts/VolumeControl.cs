using UnityEngine;
using UnityEngine.UI;

public class VolumeControl : MonoBehaviour
{
    [SerializeField] private Slider volumeSlider;

    private void Start()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("Volume", 1f);
        volumeSlider.onValueChanged.AddListener(AdjustVolume);
    }
    private void AdjustVolume(float volume)
    {
        AudioManager.Instance.SetVolume(volume);
    }
}
