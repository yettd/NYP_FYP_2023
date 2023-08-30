using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class collectionSpawn : MonoBehaviour
{
    [SerializeField] private GameObject shelf;
    [SerializeField] private GameObject Item;

    GameObject currentShelf;


    // Start is called before the first frame update
    void Start()
    {
        int i = 0;
        foreach (DentistTool dentistTools in ButtonReferenceManager.Instance.dtTools)
        {
          
            //Assign the index and the Scriptable Object into the prefab
            spawnObject(i, dentistTools);
            i++;
          
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnSelf()
    {

    }

    void spawnObject(int i , DentistTool dentist)
    {
        if(currentShelf==null || currentShelf.transform.GetChild(0).childCount>=4)
        {
            currentShelf = Instantiate(shelf, GetComponent<ScrollRect>().content);
        }

        GameObject container = Instantiate(Item, currentShelf.transform.GetChild(0));
        container.GetComponent<collectionButton>().Initialize(i, dentist.Icon, dentist.Name);
        //container.transform.GetChild(1).GetComponent<TMP_Text>().color = textColor;
    }
}
