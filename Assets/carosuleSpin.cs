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
    // Start is called before the first frame update
    void Start()
    {
        closeseGameObject = miniGames[0];
        closeseGameObject.GetComponent<GameSelected>().OpenUp();
       // NotChosen();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.A))
        {
            mainShelf.transform.position+=new Vector3(1,0,0);
            Debug.Log(mainShelf.transform.position.x);
        }
        if (Input.GetKeyDown(KeyCode.K))
        {

        SnapToCenter();
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log(closeseGameObject.transform.position.x);
        }
    }

    public void FetchCloesestMiniGame()
    {
        StopCoroutine("doEffect");
        foreach(RectTransform a in miniGames)
        {
            float cdiffx=0;
            float diffx = (a.position - c.transform.position).magnitude;

            cdiffx = (closeseGameObject.position - c.transform.position).magnitude;
            
            if ( a!=closeseGameObject && (closeseGameObject == null || diffx < cdiffx)  )
            {
                closeseGameObject = a;
            }
           
        }
        StartCoroutine("doEffect");
        NotChosen();
       

    }

    IEnumerator doEffect()
    {
        yield return new WaitForSeconds(.5f);
        closeseGameObject.GetComponent<GameSelected>().OpenUp();
        closeseGameObject.DOScale(new Vector3(1f, 1f, 1f), 1);
        SnapToCenter();
    }

    public void SnapToCenter()
    {
        float diff = closeseGameObject.transform.position.x- c.transform.position.x ;
        Debug.Log(mainShelf.transform.position.x + diff);
        mainShelf.transform.DOMoveX(mainShelf.transform.position.x+diff*-1, 1  );
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
