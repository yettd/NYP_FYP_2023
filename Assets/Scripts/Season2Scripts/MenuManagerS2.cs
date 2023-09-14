using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManagerS2 : MonoBehaviour
{
    [Header("Main Menu")]
    [SerializeField] private GameObject MainMenuPanel;
    [SerializeField] private Button SettingsBtn;
    [SerializeField] private Button PlayGameBtn;
    [SerializeField] private Button CollectionsBtn;

    [Header("Settings")]
    [SerializeField] private GameObject SettingsPanel;
    [SerializeField] private Slider AudioSettingSlider;

    [Header("Playgame")]
    [SerializeField] private GameObject PlayGamePanel;
    [SerializeField] private Button ExtractionBtn;
    [SerializeField] private Button FillingBtn;
    [SerializeField] private Button ScalingBtn;

    [Header("Universal")]
    [SerializeField] private Button BackBtn;

    bool Settingsactive;
    bool PlayGameactive;
    bool MainMenuactive;

    private void Awake()
    {
        if (MainMenuactive == false)
        {
            MainMenuPanel.transform.gameObject.SetActive(true);
            MainMenuactive = true;
        }
    }
    private void Update()
    {
       
    }
    public void LoadCollectionScene(string collection)
    {
        SceneManager.LoadScene(collection);
    }
    public void LoadExtractionScene(string ExtractionMode)
    {
        SceneManager.LoadScene(ExtractionMode);
    }
    public void LoadFillingScene(string FillingMode)
    {
        SceneManager.LoadScene(FillingMode);
    }
    public void LoadScalingScene(string ScalingMode)
    {
        SceneManager.LoadScene(ScalingMode);
    }
    public void openSettingsPanel()
    {
        AudioPlayer.Instance.PlayAudioOneShot(0, .5f);
        if (Settingsactive == false)
        {
            SettingsPanel.transform.gameObject.SetActive(true);
            Settingsactive = true;
        }
        else
        {
            SettingsPanel.transform.gameObject.SetActive(false);
            Settingsactive = false;
        }
        if (MainMenuactive == true)
        {
            MainMenuPanel.transform.gameObject.SetActive(false);
            MainMenuactive = false;
        }
    }

    public void openPlayGamePanel()
    {
        AudioPlayer.Instance.PlayAudioOneShot(0, .5f);
        if (PlayGameactive == false)
        {
            PlayGamePanel.transform.gameObject.SetActive(true);
            PlayGameactive = true;
        }
        else
        {
            PlayGamePanel.transform.gameObject.SetActive(false);
            PlayGameactive = false;
        }
        if (MainMenuactive == true)
        {
            MainMenuPanel.transform.gameObject.SetActive(false);
            MainMenuactive = false;
        }
    }

    public void openMainMenuPanel()
    {
        AudioPlayer.Instance.PlayAudioOneShot(0, .5f);
        if (MainMenuactive == false)
        {
            MainMenuPanel.transform.gameObject.SetActive(true);
            MainMenuactive = true;
        }
        else
        {
            MainMenuPanel.transform.gameObject.SetActive(false);
            MainMenuactive = false;
        }
        if (PlayGameactive == true)
        {
            PlayGamePanel.transform.gameObject.SetActive(false);
            PlayGameactive = false;
        }
        if (Settingsactive ==true)
        {
            SettingsPanel.transform.gameObject.SetActive(false);
            Settingsactive =  false;
        }
    }

    public void GotoTutortialScene()
    {
        SceneManager.LoadScene(TutorialNagivatorScript.thisScript.GetManualLoaded("Scaling"));
    }
}
