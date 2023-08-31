using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class collectionManager : MonoBehaviour
{
    public static collectionManager CM;
    public GameObject ShowItem;
    public GameObject ShelfObject;

    public bool toolOrProcedure = false;
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

        DontDestroyOnLoad(this.gameObject);
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

    public void ChangeFilter(bool a)
    {
        toolOrProcedure = a;
    }

    public void displayShelfItem()
    {
        ShelfObject.GetComponent<collectionSpawn>().ChangeItems();

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
