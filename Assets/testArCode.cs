using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class testArCode : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
       // StartCoroutine("asd");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator asd()
    {
        yield return new WaitForSeconds(0.01f);
        SceneManager.LoadScene(0);
    }
}
