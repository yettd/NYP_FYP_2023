using DG.Tweening;
using System.Collections;
using System.Collections.ObjectModel;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManagerS2 : MonoBehaviour
{
    private AudioManager audioManager;
    public Animator BookOpen;
    public Animator Scroll;

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

    [Header("InfoPage")]
    [SerializeField] private GameObject InfoPanel;
    public Image image;
    public Sprite[] images;
    private int currentImageIndex = 0;
    private bool disableButton = false;
    public Image nextImage;
    public Image previousImage;
    public Button Next;
    public Button Previous;

    [Header("Universal")]
    [SerializeField] private GameObject CreditPanel;


    [Header("Universal")]
    [SerializeField] private Button BackBtn;
    [SerializeField] private GameObject Camera;

    [Header("StampCollection")]
    [SerializeField] private GameObject StampCollection;
    [SerializeField] private GameObject achivmentText;
    [SerializeField] private GameObject ImageBook;


    bool Settingsactive;
    bool SettingsPageactive;
    bool PlayGameactive;
    bool MainMenuactive;
    bool CollectionsActive;
    bool StampActive;
    bool Infoactive;
    bool ImageBookActive;
    bool CreditActive;


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
    public void GoTocollection()
    {
        SceneManager.LoadScene("collection");
        if (audioManager != null)
        {
            audioManager.PlayBackgroundMusic(0);
        }
    }
    public void Loadcollections()
    {
        StartCoroutine(LoadCollectionScene("collection"));
    }
    IEnumerator LoadCollectionScene(string collection)
    {
        //Wipetransition.SetTrigger("Wipe");

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
        Vibrator.Vibrate(10000);
        Handheld.Vibrate();
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
        if (Infoactive == true)
        {
            InfoPanel.transform.gameObject.SetActive(false);
            Infoactive = false;
        }
        Next.interactable = true;
        Previous.interactable = true;
        nextImage.color = Color.white;
        previousImage.color = Color.white;
        disableButton = false;
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

        //transition.SetTrigger("start");
    }

    public void openInfoPAGE()
    {
        audioManager.PlaySFX(1);
        if (Infoactive == false)
        {
            InfoPanel.transform.gameObject.SetActive(true);
            Infoactive = true;
        }
        else
        {
            InfoPanel.transform.gameObject.SetActive(false);
            Infoactive = false;
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
        if (SettingsPageactive == true)
        {
            SettingsPAGE.transform.gameObject.SetActive(false);
            SettingsPageactive = false;
        }
        currentImageIndex = 0;
        image.sprite = images[currentImageIndex];

        Previous.interactable = false;
        previousImage.color = Color.gray;
    }

    public void GotoTutortialScene(string index)
    {
       SceneManager.LoadScene(TutorialNagivatorScript.Instance().GetManualLoaded(index));
    }

    public void NextInfo()
    {
        //audioManager.PlaySFX(2);
        if (currentImageIndex < images.Length -1)
        {
            audioManager.PlaySFX(2);
            currentImageIndex++;
            image.sprite = images[currentImageIndex];
        }
        else if (currentImageIndex == images.Length - 1)
        {
            nextImage.color = Color.grey;
            disableButton = true;
        }
        Previous.interactable = true;
        previousImage.color = Color.white;
    }
    public void PreviousInfo()
    {
        if (currentImageIndex >0)
        {
            audioManager.PlaySFX(2);
            currentImageIndex--;
            image.sprite = images[currentImageIndex];
        }
        else if (currentImageIndex == 0)
        {
            previousImage.color = Color.grey;
            disableButton = true;
        }
        Next.interactable = true;
        nextImage.color = Color.white;
    }

    public void openStampCollection()
    {
        if (StampActive == false)
        {
            StampCollection.transform.gameObject.SetActive(true);
            StampActive = true;
        }
        //BookOpen.SetTrigger("BookOpen");
    }

    public IEnumerator bookAnimation()
    {
        yield return new WaitForSeconds(0.25f);
        openStampCollection();
    }

    public void OpenStampAndBook()
    {
        if (ImageBookActive== false)
        {
            ImageBook.transform.gameObject.SetActive(true);
            ImageBookActive = true;
        }
        if (audioManager != null)
        {
            audioManager.PlaySFX(7);
        }
        BookOpen.SetTrigger("BookOpen");
        StartCoroutine(bookAnimation());
    }

    public void CloseStampCollection()
    {
        if (audioManager != null)
        {
            audioManager.PlaySFX(7);
        }
        if (StampActive == true)
        {
            achivmentText = GameObject.Find("achivment(Clone)");
            if(achivmentText != null)
            {
                achivmentText.SetActive(false);
            }

            StampCollection.transform.gameObject.SetActive(false);
            StampActive = false;
        }
        if (ImageBookActive == true)
        {
            ImageBook.transform.gameObject.SetActive(false);
            ImageBookActive = false;
        }
    }

    public void openCredit()
    {
        Scroll.SetTrigger("Scroll");
        if (CreditActive == false)
        {
            CreditPanel.transform.gameObject.SetActive(true);
            CreditActive = true;
        }
    }

    public void closeCredit()
    {
        if (CreditActive == true)
        {
            CreditPanel.transform.gameObject.SetActive(false);
            CreditActive = false;
        }
    }
}
