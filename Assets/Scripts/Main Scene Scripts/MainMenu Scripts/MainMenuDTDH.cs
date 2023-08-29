using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuDTDH : MonoBehaviour
{
    const string TAG_MENUMGR = "MenuTag";

    [SerializeField] PlayAnimationClick playAnimationClick;
    [SerializeField] ButtonID buttonID;

    MenuManager menuManager;

    public void ClickedDTorDH()
    {
        //Start coroutine so that i can use WaitForSeconds, trying to get the animation play before changing scene
        StartCoroutine(ClickedBtn());
    }

    void Start()
    {
        menuManager = GameObject.FindGameObjectWithTag(TAG_MENUMGR).GetComponent<MenuManager>();
    }

    IEnumerator ClickedBtn()
    {
        //Play animation
        playAnimationClick.Clicked();

        //I cant think of a soft coded way to do this, wait for the animation to be done
        yield return new WaitForSeconds(playAnimationClick.GetAnimationDuration());
        
        //Assign the button DTH ID - DT / DH / NONE
        buttonID.AssignDTHButtonID();
        //Assign the button ID -  MAINSCENE / TOOLSELECTION / ARBIT / TOOLINFO / ASSESSMENT / DEMOVID / SETTINGS
        buttonID.AssignBackButtonID();
        //switch to the tool selection panel
        menuManager.OnDHorDTClicked();
    }


}
