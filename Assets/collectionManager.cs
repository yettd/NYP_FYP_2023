using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
public class collectionManager : MonoBehaviour
{
    public static collectionManager CM;
    public GameObject ShowItem;
    public GameObject ShelfObject;

    filterButtonID[] FilterButton;
    [SerializeField]
    Color defaultC;
    [SerializeField]
    Color SelectedColor;
    public GameObject hygineAndTherapy;
    public GameObject procedure;
    public bool toolOrProcedure = false;
    int storeWhichItem;
    [SerializeField]GoToQuiz GTQ;
    public UnityEvent openToolInfo;
    
    // Start is called before the first frame update
    void Start()
    {
        FilterButton = FindObjectsOfType<filterButtonID>();
        procedure.SetActive(true);
        hygineAndTherapy.SetActive(false);
    }

    public Color GetSelected()
    {
        return SelectedColor;
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

        //DontDestroyOnLoad(this.gameObject);
    }

    public void Display(int d)
    {
        ButtonReferenceManager.Instance.storedIndex = d;
        Debug.Log(ButtonReferenceManager.Instance.storedIndex);
        if(!ShowItem.activeSelf)
        {
            //ShowItem.SetActive(true);
        }
        else
        {
            openToolInfo?.Invoke();
            ShowItem.GetComponent<DisplayItem>().DisplayNew();
        }

    }

    void ResetButtonColor()
    {
        foreach (filterButtonID BtnIT in FilterButton)
        {
            BtnIT.GetComponent<Image>().color = defaultC;
        }
    }

    public void ChangeFilter(bool a)
    {
        ResetButtonColor();
        toolOrProcedure = a;
    }

    public void SetQuizButton()
    {
        if(toolOrProcedure)
        {
            GTQ.Show(true);
            ButtonReferenceManager.Instance.ARTorP = true;
            if(ButtonReferenceManager.Instance.storeCollectionID==CollectionEnum.S)
            {
                GTQ.changeText("Scaling");

            }
            if (ButtonReferenceManager.Instance.storeCollectionID == CollectionEnum.E)
            {
                GTQ.changeText("Ex");

            }
            if (ButtonReferenceManager.Instance.storeCollectionID == CollectionEnum.F)
            {
                GTQ.changeText("Filling");

            }
        }
        else
        {
            ButtonReferenceManager.Instance.ARTorP = false;
            GTQ.Show(false);
        }
    }

    public void displayShelfItem()
    {
        ShelfObject.GetComponent<collectionSpawn>().ChangeItems();

        SetQuizButton();
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
