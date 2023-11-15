using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class MinigameScore : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI[] ratingTime;
    [SerializeField] Sprite[] ratingSprite;
    [SerializeField] Image[] ratingimage;
    GameCompletion GC;
    // Start is called before the first frame update
    void Start()
    {
        string a = Saving.save.LoadFromJson("timeandRating");
        if (a != null)
        {
            GC = JsonUtility.FromJson<GameCompletion>(Saving.save.LoadFromJson("timeandRating"));

        }
        else
        {
            GC = new GameCompletion();
        }

        ratingTime[0].text=GC.scalingTime.ToString();
        ratingimage[0].sprite = ratingSprite[GC.scalingRating];

        ratingTime[1].text = GC.FillingTime.ToString();
        ratingimage[1].sprite = ratingSprite[GC.FillingRating];
        ratingTime[2].text = GC.scalingTime.ToString();
        ratingimage[2].sprite = ratingSprite[GC.ExtrationRating];
    }

    private void SetRatingAndScore()
    {
            
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
