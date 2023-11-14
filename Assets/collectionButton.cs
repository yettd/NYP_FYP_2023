using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class collectionButton : MonoBehaviour
{
    [SerializeField] private Image imageIcon;
    [SerializeField] private TMP_Text textMeshPro;
    private int index;
    private Button button;
    private MenuManager menuManager;

    private void Start()
    {
       // menuManager = GameObject.FindGameObjectWithTag("MenuTag").GetComponent<MenuManager>();
    }
    public void Initialize(int index, Sprite image, string toolName)//Init the data from scriptable into the prefab
    {
        this.index = index;
        imageIcon.sprite = image;
        textMeshPro.text = toolName;
        name = toolName;
        SetupButtons();
    }

    private void SetupButtons()
    {
        button = GetComponent<Button>();
        //Each button is unique and have their own index
        button.onClick.AddListener(() =>
        {
            //Assign the ID to ToolSelection so that we can know where we came from
            //  ButtonReferenceManager.Instance.storedButtonID = ButtonENUM.TOOLSELECTION;
            collectionManager.CM.Display(index);
          //  menuManager.OnToolClicked();
        });
    }
}
