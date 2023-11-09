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
        sets();
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

                achivementList[AA.id].color = new Vector4(1,1,1,1);
             
                achivementList[AA.id].sprite = AA.AchivmentImage;
                achivementList[AA.id].gameObject.AddComponent<Button>().onClick.AddListener(() =>
                    {
                        ALP.SetActive(true);
                        ALP.transform.parent = achivementList[AA.id].transform;
                        ALP.transform.localPosition = new Vector3(0, 110, 0);
                        ALP.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = $"<b><size=200>{AA.AchivmentName}</size></b>\n <size=150>{AA.Des}</size>";
                    });
            }
            else
            {
                Button btn;
                achivementList[(int)AA.id].TryGetComponent<Button>(out btn);
                if(btn != null)
                {
                    btn.enabled= false;
                }
                achivementList[AA.id].color = new Vector4(0, 0, 0, 0.5f);
                achivementList[AA.id].sprite = AA.AchivmentImage;
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
    public bool GetIfUnlock(int id = -1, string NameOfAchivement = "")
    {
        foreach (AllAchivment AA in allAchivments)
        {
            if (id == AA.id || NameOfAchivement == AA.AchivmentName)
            {
                return (AA.have);
            }
        }
        return false;
    }


}
