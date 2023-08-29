using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuScoreboard : MonoBehaviour
{
    const string TAG_MENUMGR = "MenuTag";
    [SerializeField] PlayAnimationClick playAnimationClick;
    MenuManager menuManager;

    void Start()
    {
        menuManager = GameObject.FindGameObjectWithTag(TAG_MENUMGR).GetComponent<MenuManager>();
    }
    public void ClickedScoreboard()
    {
        //Start coroutine so that i can use WaitForSeconds, trying to get the animation play before changing Leaderboard Scene
        StartCoroutine(ClickedBtn());
    }
    IEnumerator ClickedBtn()
    {
        playAnimationClick.Clicked();

        //I got lazy lol and hard code to .5f
        yield return new WaitForSeconds(.5f);

        //Change scene to Leaderboard Scene
        menuManager.OnScoresClicked();
        yield return null;
    }


}
