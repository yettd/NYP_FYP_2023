using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class TransitionLoader : MonoBehaviour
{
    public Animator Transition;

    public void Loadcollections()
    {
        StartCoroutine(LoadCollectionScene("collection"));
    }
    IEnumerator LoadCollectionScene(string collection)
    {
        Transition.SetTrigger("Wipe");

        yield return new WaitForSeconds(1f);

        SceneManager.LoadSceneAsync(collection);
    }
}
