using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DW_AudioLibrary
{
    #region MAIN
    public void PlayAudioWithTaskCompletion(bool condition)
    {
        if (condition) { AudioManager.Instance.PlaySFX(5); }
    }

    public void PlayAudioWhenLose(bool condition)
    {
        if (condition) { AudioManager.Instance.PlaySFX(12); }
    }
    #endregion
}
