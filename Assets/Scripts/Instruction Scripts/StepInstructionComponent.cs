using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StepInstructionComponent : MonoBehaviour
{
    private string scenePath;
    [SerializeField] private RawImage icon;

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

    #region MODIFY COMPONENT
    public void SetIcon(Texture texture)
    {
        icon.texture = texture;
    }
    #endregion
}
