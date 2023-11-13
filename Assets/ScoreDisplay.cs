using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ScoreDisplay : MonoBehaviour
{
    [SerializeField] Sprite[] rating;
    [SerializeField] Image score;
    // Start is called before the first frame update

    public void DisplayScore(int rating)
    {
        score.sprite = this.rating[rating];
    }
}
