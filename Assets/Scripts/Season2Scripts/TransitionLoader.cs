using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class TransitionLoader : MonoBehaviour
{
    private AudioManager audioManager;
    public Animator BasicTransition;
    public Animator CupboardTransition;
    public float TransitionTime = 10f;

    //public void Awake()
    //{
    //    audioManager = AudioManager.Instance;
    //    if (audioManager == null)
    //    {
    //        Debug.LogError("AudioManager not found.");
    //    }
    //}
    public void Loadcollections()
    {
        StartCoroutine(LoadLevelTutorial(4));
    }
    public void LoadMainMenu()
    {
        StartCoroutine(LoadLevelTutorial(0));
    }
    public void LoadTutorialLanding()
    {
        StartCoroutine(LoadLevelTutorial(0));
    }
    public void LoadMiniGameTutorialScaling()
    {
        //audioManager.PlaySFX(1);
        StartCoroutine(LoadLevelScaling("Scaling"));
    }
    public void LoadMiniGameTutorialFilling()
    {
        //audioManager.PlaySFX(1);
        StartCoroutine(LoadLevelFilling("Filling"));
    }
    public void LoadMiniGameTutorialExctraction()
    {
        //audioManager.PlaySFX(1);
        StartCoroutine(LoadLevelExtraction("Extracting"));
    }
    public void LoadGamescene()
    {
        if(TutorialNagivatorScript.getScript.get_manual.Title== "Extraction")
        {
            StartCoroutine(LoadLevelTutorial(12));
            return;
        }
        StartCoroutine(LoadLevelTutorial(6));
    }
    //public void LoadGameScene()
    //{
    //    StartCoroutine(LoadLevel(6));
    //}

    public IEnumerator LoadLevelCollection(int levelIndex)
    {
        //BasicTransition.SetTrigger("start");
        CupboardTransition.SetTrigger("StartCupboard");
        yield return new WaitForSeconds(TransitionTime);
        AudioManager.Instance.PlayBackgroundMusic(3);
        SceneManager.LoadSceneAsync(levelIndex);
    }

    public IEnumerator LoadLevelTutorial(int levelIndex)
    {
        CupboardTransition.SetTrigger("StartCupboard");
        Debug.Log("retard running lah");
        yield return new WaitForSeconds(TransitionTime);
        AudioManager.Instance.PlayBackgroundMusic(3);

        SceneManager.LoadSceneAsync(levelIndex);
    }
    public IEnumerator LoadLevelScaling(string index)
    {
        CupboardTransition.SetTrigger("StartCupboard");
        Debug.Log("retard running lah");
        yield return new WaitForSeconds(TransitionTime);
        AudioManager.Instance.PlayBackgroundMusic(3);

        SceneManager.LoadScene(TutorialNagivatorScript.Instance().GetManualLoaded(index));
    }
    public IEnumerator LoadLevelFilling(string index)
    {
        CupboardTransition.SetTrigger("StartCupboard");
        Debug.Log("retard running lah");
        yield return new WaitForSeconds(TransitionTime);
        AudioManager.Instance.PlayBackgroundMusic(3);

        SceneManager.LoadScene(TutorialNagivatorScript.Instance().GetManualLoaded(index));
    }
    public IEnumerator LoadLevelExtraction(string index)
    {
        CupboardTransition.SetTrigger("StartCupboard");
        Debug.Log("retard running lah");
        yield return new WaitForSeconds(TransitionTime);
        AudioManager.Instance.PlayBackgroundMusic(3);

        SceneManager.LoadScene(TutorialNagivatorScript.Instance().GetManualLoaded(index));
    }
    public IEnumerator LoadLevelMainMenu(int levelIndex)
    {
        BasicTransition.SetTrigger("start");

        yield return new WaitForSeconds(TransitionTime);
        audioManager.PlayBackgroundMusic(1);
        SceneManager.LoadSceneAsync(levelIndex);
    }
    public void GotoTutortialScene(string index)
    {
        SceneManager.LoadScene(TutorialNagivatorScript.Instance().GetManualLoaded(index));
    }
}
