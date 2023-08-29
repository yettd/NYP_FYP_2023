using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonClickAudio : MonoBehaviour
{
    // function assigned to some buttons to play audio on click
    public void ButtonClickSound(int index)
    {
        AudioPlayer.Instance.PlayAudioOneShot(index);
    }
}
