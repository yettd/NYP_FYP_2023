using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManagerS2 : MonoBehaviour
{
    private AudioManager audioManager;
    public Animator transition;
    public Animator Wipetransition;

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

    [SerializeField] private GameObject SettingsPAGE;
    [SerializeField] private GameObject SettingsTransition;


    [Header("Playgame")]
    [SerializeField] private GameObject PlayGamePanel;
    [SerializeField] private Button ExtractionBtn;
    [SerializeField] private Button FillingBtn;
    [SerializeField] private Button ScalingBtn;

    [Header("Universal")]
    [SerializeField] private Button BackBtn;
    [SerializeField] private GameObject Camera;


    bool Settingsactive;
    bool SettingsPageactive;
    bool PlayGameactive;
    bool MainMenuactive;
    bool CollectionsActive;
    private void Awake()
    {
        audioManager = AudioManager.Instance;

        if (audioManager == null)
        {
            Debug.LogError("AudioManager not found.");
        }

        if (audioManager != null)
        {
            audioManager.PlayBackgroundMusic(1);
        }

        if (MainMenuactive == false)
        {
            MainMenuPanel.transform.gameObject.SetActive(true);
            MainMenuactive = true;
        }
    }
    private void Update()
    {
       
    }
    public void GoToMain()
    {
        SceneManager.LoadScene(0);
    }
    public void Loadcollections()
    {
        StartCoroutine(LoadCollectionScene("collection"));
    }
    IEnumerator LoadCollectionScene(string collection)
    {
        Wipetransition.SetTrigger("Wipe");

        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene(collection);
        if (audioManager != null)
        {
            audioManager.PlayBackgroundMusic(0);
        }
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
        if (audioManager != null)
        {
            audioManager.PlaySFX(1);
        }
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
        if (SettingsPageactive == true)
        {
            SettingsPAGE.transform.gameObject.SetActive(false);
            SettingsPageactive = false;
        }
        //book
        Camera.transform.DOMove(new Vector3(-40, 0, 190f), 1);
        Camera.transform.DORotate(new Vector3(90, 0, 0), 1);

        //yield return new WaitForSeconds(transitionTime);
    }

    public void openPlayGamePanel()
    {
        audioManager.PlaySFX(1);
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
        if (SettingsPageactive == true)
        {
            SettingsPAGE.transform.gameObject.SetActive(false);
            SettingsPageactive = false;
        }
        Camera.transform.DOMove(new Vector3(-2, 6, 96f),1);
        Camera.transform.DORotate(new Vector3(45, -90, 0), 1);
    }

    public void openMainMenuPanel()
    {
        audioManager.PlaySFX(1);
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
        if (SettingsPageactive == true)
        {
            SettingsPAGE.transform.gameObject.SetActive(false);
            SettingsPageactive = false;
        }
        //original
        Camera.transform.DOMove(new Vector3(34, 12.8f, -16), 1);
        Camera.transform.DORotate(new Vector3(10, 0, 0), 1);
    }

    public void openCollectionsPanel()
    {
        audioManager.PlaySFX(1);
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
        if (SettingsPageactive == true)
        {
            SettingsPAGE.transform.gameObject.SetActive(false);
            SettingsPageactive = false;
        }
        //new
        Camera.transform.DOMove(new Vector3(2.1f, -8f, 151f), 1);
        Camera.transform.DORotate(new Vector3(0,-90, 0), 1);
        ////cardboard(original)
        //Camera.transform.DOMove(new Vector3(45.6f, -39, 140), 1);
        //Camera.transform.DORotate(new Vector3(0, 0, 0), 1);

    }

    public void openSettingsPAGE()
    {
        audioManager.PlaySFX(1);
        if (SettingsPageactive == false)
        {
            SettingsPAGE.transform.gameObject.SetActive(true);
            SettingsPageactive = true;
        }
        else
        {
            SettingsPAGE.transform.gameObject.SetActive(false);
            SettingsPageactive = false;
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
        if (Settingsactive == true)
        {
            SettingsPanel.transform.gameObject.SetActive(false);
            Settingsactive = false;
        }

        transition.SetTrigger("start");
    }
    public void GotoTutortialScene(string index)
    {
       SceneManager.LoadScene(TutorialNagivatorScript.Instance().GetManualLoaded(index));
    }
}
