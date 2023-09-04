using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class TutorialDashBoard_Slot_Script : MonoBehaviour
{
    private enum SLOT_STATUS { LOCKED, COMPLETED, UNLOCK };
    private SLOT_STATUS status = SLOT_STATUS.UNLOCK;
    private string[] status_text = { "LOCKED!", "COMPLETED!", "OPEN!" };

    [SerializeField] private RawImage icon;
    [SerializeField] private TMP_Text textSlot;

    private int slot_index = 0;

    #region SETUP
    private void SetTextVisible(int status_index)
    {
        textSlot.text = status_text[status_index];
        textSlot.gameObject.SetActive(true);
    }
    #endregion

    #region MAIN
    public void SelectTutorial()
    {
        if (status != SLOT_STATUS.LOCKED)
        {
            PlayerPrefs.SetInt("TutorialStageLevel", slot_index);
            SceneManager.LoadScene("Tutorial_LandingScene");
        }
    }
    #endregion

    #region MODIFY COMPONENT
    public void SetLockedSlot()
    {
        status = SLOT_STATUS.LOCKED;
        SetTextVisible((int)status);
    }

    public void SetCompleteSlot()
    {
        status = SLOT_STATUS.COMPLETED;
        SetTextVisible((int)status);
    }

    public void SetUnlockSlot()
    {
        status = SLOT_STATUS.UNLOCK;
        SetTextVisible((int)status);
    }

    public void SetDestinationIndex(int index)
    {
        slot_index = index;
    }

    public void SetIcon(Texture texture)
    {
        icon.texture = texture;
    }
    #endregion
}
