using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;
public class ScrollViewContainerController : MonoBehaviour
{
    [SerializeField] private GameObject containerPrefab;
    [SerializeField] private ScrollRect scroll;
    [SerializeField] private Transform contentTransform;
    [SerializeField] private GameObject topParentGameObject;
    [SerializeField] private Color textColor;

    public void LoadTheContent(DTHEnum dth)
    {
        //Change the text based on top title thingy background color
        textColor = topParentGameObject.GetComponent<TopParentColor>().GetcurrentTopParentColor();
        
        //Clear the table 
        ClearContent();

        if (dth == DTHEnum.DT)
        {
            //Having i as a index so that i can assign it into the prefab
            //Able to know which index the user last picked 
            int i = 0;

            //ButtonReferenceManager.Instance.dtTools is the list of tools that is stored in Resources/AllTheTools/DT
            foreach (DentistTool dentistTools in ButtonReferenceManager.Instance.dtTools)
            {
                //Assign the index and the Scriptable Object into the prefab
                GenerateContainerWithTool(i, dentistTools);
                i++;
            }
        }
        else if (dth == DTHEnum.DH)
        {
            //Having i as a index so that i can assign it into the prefab
            //Able to know which index the user last picked 
            int i = 0;

            //ButtonReferenceManager.Instance.dtTools is the list of tools that is stored in Resources/AllTheTools/DH
            foreach (DentistTool dentistTools in ButtonReferenceManager.Instance.dhTools)
            {
                //Assign the index and the Scriptable Object into the prefab
                GenerateContainerWithTool(i, dentistTools);
                i++;
            }
        }
        else
        {
            //Debug.Log("trying to generate content with NONE as DTHEnum");
        }

    }

    //Assigning the index and the scriptableObject data into the prefab
    void GenerateContainerWithTool(int index, DentistTool dentistTool)
    {
        GameObject container = Instantiate(containerPrefab, scroll.content);
        container.GetComponent<AppIconContainerController>().Initialize(index, dentistTool.Icon, dentistTool.Name);
        container.transform.GetChild(1).GetComponent<TMP_Text>().color = textColor;
    }
    
    public void ClearContent()
    {
        foreach (Transform child in contentTransform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }

    private void OnEnable()
    {
        ClearContent();
        if (ButtonReferenceManager.Instance)
        {
            LoadTheContent(ButtonReferenceManager.Instance.storedDTHButtonID);
        }
    }

    private void OnDisable()
    {
        ClearContent();
    }

}
