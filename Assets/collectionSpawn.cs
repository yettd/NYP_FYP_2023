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
        collectionManager.CM.toolOrProcedure = true;
        ButtonReferenceManager.Instance.storeCollectionID = CollectionEnum.S;
        ChangeItems();
    }
    
    void Clear()
    {
        foreach (Transform child in GetComponent<ScrollRect>().content)
        {
            GameObject.Destroy(child.gameObject);
        }
        currentShelf = null;
    }

    public void ChangeItems()
    {
        Clear();
        int i = 0;
        if (collectionManager.CM.toolOrProcedure)
        {
            if (ButtonReferenceManager.Instance.storeCollectionID == CollectionEnum.S)
            {
                foreach (DentistTool dentistTools in ButtonReferenceManager.Instance.S)
                {
                    //Assign the index and the Scriptable Object into the prefab
                    spawnObject(i, dentistTools);
                    i++;

                }
            }
            else if (ButtonReferenceManager.Instance.storeCollectionID == CollectionEnum.E)
            {

                foreach (DentistTool dentistTools in ButtonReferenceManager.Instance.E)
                {

                    //Assign the index and the Scriptable Object into the prefab
                    spawnObject(i, dentistTools);
                    i++;

                }
            }
            else if (ButtonReferenceManager.Instance.storeCollectionID == CollectionEnum.F)
            {

                foreach (DentistTool dentistTools in ButtonReferenceManager.Instance.F)
                {

                    //Assign the index and the Scriptable Object into the prefab
                    spawnObject(i, dentistTools);
                    i++;

                }
            }
        }
        else
        {
            if (ButtonReferenceManager.Instance.storedDTHButtonID == DTHEnum.DH)
            {

                foreach (DentistTool dentistTools in ButtonReferenceManager.Instance.dhTools)
                {

                    //Assign the index and the Scriptable Object into the prefab
                    spawnObject(i, dentistTools);
                    i++;

                }
            }
            else if (ButtonReferenceManager.Instance.storedDTHButtonID == DTHEnum.DT)
            {

                foreach (DentistTool dentistTools in ButtonReferenceManager.Instance.dtTools)
                {

                    //Assign the index and the Scriptable Object into the prefab
                    spawnObject(i, dentistTools);
                    i++;

                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnSelf()
    {
        if (currentShelf == null || currentShelf.transform.GetChild(0).childCount >= 4)
        {
            currentShelf = Instantiate(shelf, GetComponent<ScrollRect>().content);
        }
    }

    void spawnObject(int i , DentistTool dentist)
    {
        SpawnSelf();

        GameObject container = Instantiate(Item, currentShelf.transform.GetChild(0));
        if(dentist.rusty)
        {
            container.GetComponent<collectionButton>().Initialize(i, dentist.RustyIcon, dentist.Name);

            return;
        }
        container.GetComponent<collectionButton>().Initialize(i, dentist.Icon, dentist.Name);
        //container.transform.GetChild(1).GetComponent<TMP_Text>().color = textColor;
    }
}
