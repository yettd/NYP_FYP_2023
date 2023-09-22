using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class GoToQuiz : MonoBehaviour
{
    public TMP_Text t;

    string typeQuiz;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void changeText(string text)
    {
        //typeQuiz = text;
        //t.text = $"Take {text} Quiz";
    }
    public void Show(bool show)
    {
        gameObject.SetActive(false);
        if(show)
        {

            gameObject.SetActive(true);
        }
    }

    public void GoToQuizScene()
    {
        if(ButtonReferenceManager.Instance.storeCollectionID==CollectionEnum.E)
        {
        SceneManager.LoadScene("ExtractioQuiz");

        }
        else if(ButtonReferenceManager.Instance.storeCollectionID == CollectionEnum.S)
        {
            SceneManager.LoadScene("ExtractioQuiz");

        }
        else if (ButtonReferenceManager.Instance.storeCollectionID == CollectionEnum.F)
        {
            SceneManager.LoadScene("ExtractioQuiz");

        }
    }
}
