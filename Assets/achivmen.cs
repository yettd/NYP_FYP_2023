using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class achivmen : MonoBehaviour
{
    public static achivmen instance;

    AllAchivment[] allAchivments;
    [SerializeField]Image[] achivementImg;
    [SerializeField]List<Image> achivementList;

    GameObject achimentListPanel;
    GameObject ALP;
    // Start is called before the first frame update
    void Start()
    {
        achimentListPanel = Resources.Load<GameObject>("achivment/achivment");
        ALP = Instantiate(achimentListPanel) as GameObject;
        ALP.SetActive(false);
    }
    public void sets()
    {
        allAchivments = Resources.LoadAll<AllAchivment>("achivment");
        achivementImg = FindObjectsByType<Image>(FindObjectsSortMode.InstanceID);

        int i = 0;
        foreach (Image image in achivementImg)
        {
       
            if (image.transform.parent == null || (!image.transform.parent.name.Contains("left") && !image.transform.parent.name.Contains("right")))
            {
                Debug.Log(image.transform.parent);
                achivementImg[i] = null;
            }
            if (achivementImg[i] != null)
            {
              
                achivementList.Add(achivementImg[i]);
            }
            i++;
        }
        display();
    }

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
    }
    // Update is called once per frame
    
    public void display()
    {
        foreach(AllAchivment AA in allAchivments)
        {
            if(AA.have)
            {
          
             
                achivementList[AA.id].sprite = AA.AchivmentImage;
                achivementList[AA.id].gameObject.AddComponent<Button>().onClick.AddListener(() =>
                    {
                        ALP.SetActive(true);
                        ALP.transform.parent = achivementList[AA.id].transform;
                        ALP.transform.localPosition = new Vector3(0, 110, 0);
                        ALP.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = $"<b><size=200>{AA.name}</size></b>\n <size=150>{AA.Des}</size>";
                    });
            }
            else
            {
                achivementList[AA.id].sprite = AA.AchivmentImageLock;
            }
        }
    }

    public void UnlockAchivement(int id = -1 , string NameOfAchivement="")
    {
        foreach (AllAchivment AA in allAchivments)
        {
            if(id == AA.id || NameOfAchivement == AA.AchivmentName)
            {
                AA.have = true;
            }
        }
    }


}
