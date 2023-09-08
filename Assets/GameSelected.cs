using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
public class GameSelected : MonoBehaviour
{

    [SerializeField] RectTransform GameInfo;
    float currentPosGI;
    [SerializeField] RectTransform GameScore;
    float currentPosGS;
    // Start is called before the first frame update
    void Start()
    {
        currentPosGI = GameInfo.anchoredPosition.x;
        currentPosGS = GameScore.anchoredPosition.x;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            OpenUp();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            close();
        }
    }

    public void OpenUp()
    {
        GameInfo.DOAnchorPosX(0,1);
        GameScore.DOAnchorPosX(0, 1);
    }

    public void close()
    {
        GameInfo.DOAnchorPosX(currentPosGI, 1);
        GameScore.DOAnchorPosX(currentPosGS, 1);
    }
}
