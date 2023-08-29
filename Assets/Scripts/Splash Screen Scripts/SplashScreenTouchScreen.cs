using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;


public class SplashScreenTouchScreen : MonoBehaviour
{
    [SerializeField] GameObject NypLogoGameObject;
    [SerializeField] GameObject LogoGameObject;
    [SerializeField] GameObject TapScreenTextGameObject;

    bool ableToTapScreen = false;

    private Color nypLogoColor;
    private Color logoColor;
    private Color textColor;

    private Image nypLogoImage;
    private Image logoImage;
    private TMP_Text tapScreenTMP;
    private void OnEnable()
    {
        StartCoroutine(FadeInLogo(.75f));//higher float = faster load

    }

    private void Awake()
    {
        //Assign Images variables
        logoImage    = LogoGameObject.GetComponent<Image>();
        nypLogoImage = NypLogoGameObject.GetComponent<Image>();
        tapScreenTMP = TapScreenTextGameObject.GetComponent<TMP_Text>();

        //Set every image & text alpha to 0 at the start
        logoColor    = new Color(logoImage.color.r, logoImage.color.g, logoImage.color.b, 0f);
        nypLogoColor = new Color(nypLogoImage.color.r, nypLogoImage.color.g, nypLogoImage.color.b, 0f);
        textColor    = new Color(tapScreenTMP.color.r, tapScreenTMP.color.g, tapScreenTMP.color.b, 0f);

        logoImage.color    = logoColor;
        nypLogoImage.color = nypLogoColor;
        tapScreenTMP.color = textColor;
    }


    public void TouchedScreen()
    {
        if (ableToTapScreen)//Force the user to wait till see the "Tap Screen"
        {
            SceneManager.LoadScene("Main Scene");
        }
    }

  

    private IEnumerator FadeInLogo(float fadeSpeed)
    {
        float fadeAmount;
        Color tempLogoColor = LogoGameObject.GetComponent<Image>().color;
        Color tempNypLogoColor = NypLogoGameObject.GetComponent<Image>().color;


        while (logoImage.color.a < 1)//while loop to fade in the logos... ik while loop is bad
        {
            fadeAmount = tempLogoColor.a + (fadeSpeed * Time.deltaTime);//increment the fadeAmount from 0f to 1f based on fadeSpeed

            //Set the fadeAmount into a temp Color variables
            tempNypLogoColor = new Color(tempNypLogoColor.r, tempNypLogoColor.g, tempNypLogoColor.b, fadeAmount);
            tempLogoColor = new Color(tempLogoColor.r, tempLogoColor.g, tempLogoColor.b, fadeAmount);

            //Replace the logos color with the temp color
            nypLogoImage.color = tempNypLogoColor;
            logoImage.color = tempLogoColor;

            yield return null;  
        }
        
        //After Logo is shown, show the tap screen
        Color tempTextColor = tapScreenTMP.color;
        fadeSpeed *= 2;//Have the fade in faster then the logo
       
        while (tapScreenTMP.color.a < 1) //While loop to fade in the tap screen ... i get it. while loop bad
        {
            if (tapScreenTMP.color.a > .25f) //allow the user to be able to proceed to the next screen once the alpha reach about a certain point
            {
                ableToTapScreen = true;
            }

            fadeAmount = tempTextColor.a + (fadeSpeed * Time.deltaTime);

            //Set the fadeAmount into a temp Color variables
            tempTextColor = new Color(tempTextColor.r, tempTextColor.g, tempTextColor.b, fadeAmount);

            //Replace the logos color with the temp color
            tapScreenTMP.color = tempTextColor;

            yield return null;
        }
    }

}
