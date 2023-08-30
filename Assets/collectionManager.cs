using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class collectionManager : MonoBehaviour
{
    public static collectionManager CM;
    public GameObject ShowItem;

     int storeWhichItem;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        if(CM==null)
        {
            CM = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Display(int d)
    {
        storeWhichItem = d;
        if(!ShowItem.activeSelf)
        {
        ShowItem.SetActive(true);

        }
        else
        {
            ShowItem.GetComponent<DisplayItem>().DisplayNew();
        }

    }

    public int GetStore()
    {
        return storeWhichItem;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
