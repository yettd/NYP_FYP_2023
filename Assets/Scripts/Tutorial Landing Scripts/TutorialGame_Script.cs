using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialGame_Script : MonoBehaviour
{
    public void Back()
    {
        SceneManager.LoadScene("Tutorial_LandingScene");
    }
}
