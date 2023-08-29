using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Video;
using UnityEngine.Sprites;
using UnityEngine.UI;
using TMPro;

public class VideoController : MonoBehaviour
{
    [SerializeField]private VideoPlayer videoPlayer;
    [SerializeField]private TMP_Text toolNameText;
    private Texture vidTex;

    public Button playPauseButton;
    public Sprite startSprite;
    public Sprite pauseSprite;

    public TMP_Text videoTimeText;

    public GameObject placeHolder;

    [Header("Scripts")]
    public SceneChanger sceneChanger;
    private void Start()
    {
        placeHolder.SetActive(false);
        // get tool name and video from dt/dh and tool clicked
        if (ButtonReferenceManager.Instance.storedDTHButtonID == DTHEnum.DT)
        {
            toolNameText.text = ButtonReferenceManager.Instance.dtTools[ButtonReferenceManager.Instance.storedIndex].Name;
            videoPlayer.clip = ButtonReferenceManager.Instance.dtTools[ButtonReferenceManager.Instance.storedIndex].videoClip;
        }
        else if (ButtonReferenceManager.Instance.storedDTHButtonID == DTHEnum.DH)
        {
            toolNameText.text = ButtonReferenceManager.Instance.dhTools[ButtonReferenceManager.Instance.storedIndex].Name;
            videoPlayer.clip = ButtonReferenceManager.Instance.dhTools[ButtonReferenceManager.Instance.storedIndex].videoClip;
        }

        // placeholder for video section for tools with no video yet
        // if no video, put placeholder
        if (videoPlayer.clip == null)
        {
            placeHolder.SetActive(true);
        }
        else
        {
            placeHolder.SetActive(false);
        }

        // play video
        videoPlayer.Play();
        playPauseButton.image.sprite = pauseSprite;
    }

    private void Update()
    {
        videoTimeText.text = videoPlayer.time.ToString("0.00");
    }

    // rewind video 5s
    public void OnSkipBackward()
    {
        videoPlayer.time -= 5f;
        //AudioPlayer.Instance.PlayAudioOneShot(0);
    }

    // fast forward video 5s
    public void OnSkipForward()
    {
        videoPlayer.time += 5f;
        //AudioPlayer.Instance.PlayAudioOneShot(0);
    }

    // play/pause video on button press
    public void OnStartPausePressed()
    {
        if (videoPlayer.isPlaying == false)
        {
            videoPlayer.Play();
            playPauseButton.image.sprite = pauseSprite;
        }
        else
        {
            videoPlayer.Pause();
            playPauseButton.image.sprite = startSprite;
        }
        //AudioPlayer.Instance.PlayAudioOneShot(0);
    }

    public void OnHomeClicked()
    {
        // reset DTH ID to none, button ID to mainscene when home is pressed
        ButtonReferenceManager.Instance.storedDTHButtonID = DTHEnum.NONE;
        ButtonReferenceManager.Instance.storedButtonID = ButtonENUM.MAINSCENE;
        sceneChanger.ChangeToMainScene();
    }
}
