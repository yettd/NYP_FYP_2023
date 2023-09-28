using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MenuManagerS2 : MonoBehaviour
{
    [Header("Main Menu")]
    [SerializeField] private GameObject MainMenuPanel;
    [SerializeField] private Button SettingsBtn;
    [SerializeField] private Button PlayGameBtn;
    [SerializeField] private Button CollectionsBtn;

    [Header("Collections")]
    [SerializeField] private GameObject CollectionsPanel;
    [SerializeField] private Button SettingsBtn2;
    [SerializeField] private Button PlayGameBtn2;
    [SerializeField] private Button CollectionsBtn2;

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
    [SerializeField] private GameObject Camera;


    bool Settingsactive;
    bool PlayGameactive;
    bool MainMenuactive;
    bool CollectionsActive;
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
        if (CollectionsActive == true)
        {
            CollectionsPanel.transform.gameObject.SetActive(false);
            CollectionsActive = false;
        }
        if (PlayGameactive == true)
        {
            PlayGamePanel.transform.gameObject.SetActive(false);
            PlayGameactive = false;
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
        if (Settingsactive == true)
        {
            SettingsPanel.transform.gameObject.SetActive(false);
            Settingsactive = false;
        }
        if (CollectionsActive == true)
        {
            CollectionsPanel.transform.gameObject.SetActive(false);
            CollectionsActive = false;
        }

        Camera.transform.DOMove(new Vector3(-2, 6, 96f),1);
        Camera.transform.DORotate(new Vector3(45, -90, 0), 1);
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
        if (CollectionsActive == true)
        {
            CollectionsPanel.transform.gameObject.SetActive(false);
            CollectionsActive = false;
        }

        //original
        Camera.transform.DOMove(new Vector3(34, 46.2f, -10), 1);
        Camera.transform.DORotate(new Vector3(31.176f, 0, 0), 1);

    }

    public void openCollectionsPanel()
    {
        AudioPlayer.Instance.PlayAudioOneShot(0, .5f);
        if (CollectionsActive == false)
        {
            CollectionsPanel.transform.gameObject.SetActive(true);
            CollectionsActive = true;
        }
        else
        {
            CollectionsPanel.transform.gameObject.SetActive(false);
            CollectionsActive = false;
        }
        if (PlayGameactive == true)
        {
            PlayGamePanel.transform.gameObject.SetActive(false);
            PlayGameactive = false;
        }
        if (Settingsactive == true)
        {
            SettingsPanel.transform.gameObject.SetActive(false);
            Settingsactive = false;
        }
        if (MainMenuactive == true)
        {
            MainMenuPanel.transform.gameObject.SetActive(false);
            MainMenuactive = false;
        }

        //cardboard
        Camera.transform.DOMove(new Vector3(45.6f, -39, 140), 1);
        Camera.transform.DORotate(new Vector3(0, 0, 0), 1);
    }

    public void GotoTutortialScene(string index)
    {
       SceneManager.LoadScene(TutorialNagivatorScript.Instance().GetManualLoaded(index));
    }
}
