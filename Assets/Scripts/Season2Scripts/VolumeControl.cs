using UnityEngine;
using UnityEngine.UI;

public class VolumeControl : MonoBehaviour
{
    [SerializeField] private Slider volumeSlider;


    vol v;


    private void Start()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("Volume", 1f);
        volumeSlider.onValueChanged.AddListener(AdjustVolume);
        if(Saving.save.LoadSoundFromJson()!=null)
        {

        string a = Saving.save.LoadSoundFromJson();
            v = JsonUtility.FromJson<vol>(a);
        }

    }
    private void AdjustVolume(float volume)
    {
        AudioManager.Instance.SetVolume(volume);
        v.volume = volume;

        Saving.save.saveSoundToJson(v);

    }

    class vol
    {
        public float volume;
    }
}
