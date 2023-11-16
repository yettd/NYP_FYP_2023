using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class carosuleSpin : MonoBehaviour
{
    [SerializeField] RectTransform[] miniGames;
    public Canvas c;
    RectTransform closeseGameObject;
    [SerializeField]RectTransform mainShelf;
    Tween moving;
    // Start is called before the first frame update
    void Start()
    {
        closeseGameObject = miniGames[0];
        closeseGameObject.GetComponent<GameSelected>().OpenUp();
       // NotChosen();
    }
    private void OnEnable()
    {
        StartCoroutine("Enabling");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FetchCloesestMiniGame()
    {
        if (Input.touchCount > 0 || Input.GetMouseButton(0))
        {
            StopCoroutine("doEffect");
            foreach (RectTransform a in miniGames)
            {
                float cdiffx = 0;
                float diffx = (a.position - c.transform.position).magnitude;

                cdiffx = (closeseGameObject.position - c.transform.position).magnitude;

                if (a != closeseGameObject && (closeseGameObject == null || diffx < cdiffx))
                {
                    closeseGameObject = a;
                }

            }
            moving.Complete();
            StartCoroutine("doEffect");
            NotChosen();
        }
       

    }

    IEnumerator doEffect()
    {
        yield return new WaitForSeconds(0.5f);
        Debug.Log(closeseGameObject);
        closeseGameObject.GetComponent<GameSelected>().OpenUp();
        closeseGameObject.DOScale(new Vector3(1f, 1f, 1f), 1);
        SnapToCenter(false);
    }
    IEnumerator Enabling()
    {
        yield return new WaitForEndOfFrame();
        closeseGameObject.GetComponent<GameSelected>().OpenUp();
        closeseGameObject.DOScale(new Vector3(1f, 1f, 1f), 1);
        SnapToCenter(true);
    }

    public void SnapToCenter(bool t)
    {
        Debug.Log(t);
        float diff = closeseGameObject.transform.position.x- c.transform.position.x ;
        moving = mainShelf.transform.DOMoveX(mainShelf.transform.position.x + diff * -1, t ? 0.01f:1 ) ;
    }
        
    public void NotChosen()
    {
        //close 
        foreach (RectTransform a in miniGames)
        {
            if (a != closeseGameObject)
            {
           
                a.GetComponent<GameSelected>().close();
                a.DOScale(new Vector3(.5f, .5f, .5f), 1);   
            }
        }

        //shrink
    }

    public void ScaleMiniGame()
    {

    }
}
