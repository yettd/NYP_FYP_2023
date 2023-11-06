using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class achivmen : MonoBehaviour
{
    public static achivmen instance;

    AllAchivment[] allAchivments;
    [SerializeField]Image[] achivementImg;
    [SerializeField]List<Image> achivementList;

    // Start is called before the first frame update
    void Start()
    {


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
            }
            else
            {
                achivementList[AA.id].sprite = AA.AchivmentImageLock;
            }
        }
    }

}
