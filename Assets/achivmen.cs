using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using System;

public class achivmen : MonoBehaviour
{
    public static achivmen instance;

    AllAchivment[] allAchivments;
    [SerializeField]Image[] achivementImg;
    [SerializeField]List<Image> achivementList;

    GameObject achimentListPanel;
    GameObject ALP;

   [SerializeField] GameObject Left;
    [SerializeField] GameObject Right;

    achivements am;
    bool seted;
    // Start is called before the first frame update
    void Start()
    {
        allAchivments = Resources.LoadAll<AllAchivment>("achivment");
        //get achivement savefile
        string a = Saving.save.LoadFromJson("achiv");
        if (a == null)
        {
            //if dont exist create one and see allAchivments to not have
            foreach (AllAchivment item in allAchivments)
            {
                item.have=false;
            }
            am=new achivements();
            Saving.save.saveToJson(a, "achiv");
        }
        achimentListPanel = Resources.Load<GameObject>("achivment/achivment");
        ALP = Instantiate(achimentListPanel) as GameObject;
        ALP.SetActive(false);
        sets();
    }
    public void sets()
    {
        //set the imagelist to
        for (int i = 0; i < Left.transform.childCount; i++)
        {
            achivementList.Add(Left.transform.GetChild(i).GetComponent<Image>());
        }

        for (int i = 0; i < Right.transform.childCount; i++)
        {
            achivementList.Add(Right.transform.GetChild(i).GetComponent<Image>());
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
        //display all the achivement in the book
        foreach(AllAchivment AA in allAchivments)
        {
            if(AA.have)
            {
                Debug.Log(AA.id);
                achivementList[AA.id].color = new Vector4(1,1,1,1);
                achivementList[AA.id].sprite = AA.AchivmentImage;
                Button btn;
                achivementList[(int)AA.id].TryGetComponent<Button>(out btn);
                if (btn == null)
                {

                    achivementList[AA.id].gameObject.AddComponent<Button>().onClick.AddListener(() =>
                        {
                            ALP.SetActive(true);
                            ALP.transform.parent = achivementList[AA.id].transform;
                            ALP.transform.localPosition = new Vector3(0, 110, 0);
                            ALP.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = $"<b><size=200>{AA.AchivmentName}</size></b>\n <size=150>{AA.Des}</size>";
                        });
                }
            }
            else
            {
                Button btn;
                achivementList[(int)AA.id].TryGetComponent<Button>(out btn);
                if(btn != null)
                {
                    btn.enabled= false;
                }
                Debug.Log(achivementList[AA.id].transform.parent);
                achivementList[AA.id].color = new Vector4(0, 0, 0, 0.5f);
                achivementList[AA.id].sprite = AA.AchivmentImage;
            }
        }
    }

    public void UnlockAchivement(int id = -1 , string NameOfAchivement="")
    {
        //unlock the achivment
        bool unlock=true;
        foreach (AllAchivment AA in allAchivments)
        {
            if(id == AA.id || NameOfAchivement == AA.AchivmentName)
            {
                AA.have = true;
            }
            if((AA.id!=13 && !AA.have))
            {
                Debug.Log("other achive : " + AA.AchivmentName);
                unlock = false;
            }
            if(AA.id==13 && AA.have)
            {
                Debug.Log("itself achive : " + AA.AchivmentName);
                unlock = false;
            }
        }
        if(unlock)//if all but the last is unlock. Unlock the last one
        {
            UnlockAchivement(13, "all");
        }
        
    }
    public bool GetIfUnlock(int id = -1, string NameOfAchivement = "")
    {
        //return if unlock
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
public class achivements
{

}
