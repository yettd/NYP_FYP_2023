using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class TransitionLoader : MonoBehaviour
{
    public Animator BasicTransition;
    public Animator CupboardTransition;

    public float TransitionTime = 10f;
    public void Loadcollections()
    {
        StartCoroutine(LoadLevelTutorial(4));
    }
    public void LoadMainMenu()
    {
        StartCoroutine(LoadLevelMainMenu(0));
    }
    public void LoadMiniGameTutorial()
    {
        StartCoroutine(LoadLevelTutorial(9));
    }
    public void LoadGamescene()
    {
        StartCoroutine(LoadLevelTutorial(6));
    }
    //public void LoadGameScene()
    //{
    //    StartCoroutine(LoadLevel(6));
    //}

    public IEnumerator LoadLevelCollection(int levelIndex)
    {
        BasicTransition.SetTrigger("start");

        yield return new WaitForSeconds(TransitionTime);

        SceneManager.LoadSceneAsync(levelIndex);
    }

    public IEnumerator LoadLevelTutorial(int levelIndex)
    {
        CupboardTransition.SetTrigger("StartCupboard");
        Debug.Log("retard running lah");
        yield return new WaitForSeconds(TransitionTime);

        SceneManager.LoadSceneAsync(levelIndex);
    }

    public IEnumerator LoadLevelMainMenu(int levelIndex)
    {
        BasicTransition.SetTrigger("start");

        yield return new WaitForSeconds(TransitionTime);

        SceneManager.LoadSceneAsync(levelIndex);
    }

}
