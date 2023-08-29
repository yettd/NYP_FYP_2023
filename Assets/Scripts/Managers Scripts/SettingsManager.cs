using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// Marcus
public class SettingsManager : MonoBehaviour
{
    public static SettingsManager Instance { get; private set; }

    [Header("Volume Slider")]
    [SerializeField] private Slider volumeSlider = null;
    [SerializeField] private TMP_Text volumeText = null;
    private static float volumeValue = 1.0f;

    [HideInInspector]
    public GameObject audioPlayer;
    AudioPlayer audioPlay;

    private void Start()
    {
        // start at full volume
        PlayerPrefs.SetFloat("VolumeValue", volumeValue);
        // find audioplayer via tag
        audioPlayer = GameObject.FindGameObjectWithTag("AudioPlayerTag");
        audioPlay = audioPlayer.GetComponent<AudioPlayer>();
        // load volume on start
        LoadVolume();
    }

    // update volume slider text value
    public void VolumeSlider(float volume)
    {
        volumeText.text = volume.ToString("0.0");
    }

    // save volume
    // set volume based on value from slider, set into playerprefs then load volume
    public void SaveVolume()
    {
        volumeValue = volumeSlider.value;
        PlayerPrefs.SetFloat("VolumeValue", volumeValue);
        LoadVolume();
        //Debug.Log("saved volume: " + PlayerPrefs.GetFloat("VolumeValue"));
    }

    // get volume value saved in playerprefs
    public void LoadVolume()
    {
        volumeValue = PlayerPrefs.GetFloat("VolumeValue");
        volumeSlider.value = volumeValue;
        audioPlay.audioSrc.volume = volumeValue;
    }

    // on reset leaderboard button pressed
    public void OnResetLeaderboardPressed()
    {
        // load volume first
        LoadVolume();
        // do not reset donotshowagain playerpref
        if (PlayerPrefs.GetInt("doNotShowAgainChecked") != 0)
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.SetInt("doNotShowAgainChecked", 1);
        }
        else
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.SetInt("doNotShowAgainChecked", 0);
        }
        // then save so that volume is not reset
        SaveVolume();
        Debug.Log("Reset Leaderboard");
    }
}
