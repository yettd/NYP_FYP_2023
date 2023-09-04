using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StepInstructionComponent : MonoBehaviour
{
    private string scenePath;

    #region SETUP
    public void SetScenePath(string sceneName)
    {
        scenePath = sceneName;
    }
    #endregion

    #region MAIN
    public void BeginGameSceneInstruction()
    {
        SceneManager.LoadScene(scenePath);
    }
    #endregion
}
