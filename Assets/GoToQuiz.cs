using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GoToQuiz : MonoBehaviour
{
    public TMP_Text t;
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
        t.text = $"Take {text} Quiz";
    }
    public void Show(bool show)
    {
        gameObject.SetActive(false);
        if(show)
        {

            gameObject.SetActive(true);
        }
    }
}
