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
        procedure.SetActive(false);
        hygineAndTherapy.SetActive(true);
        ButtonReferenceManager.Instance.LoadToolsDatabases();
    }

    public Color GetSelected()
    {
        return SelectedColor;
    }

    private void Awake()
    {
        if (CM == null)
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

        openToolInfo?.Invoke();
        ShowItem.GetComponent<DisplayItem>().DisplayNew();
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
        GTQ.Show(false);
        if (toolOrProcedure)
        {
           
            ButtonReferenceManager.Instance.ARTorP = true;
            if(ButtonReferenceManager.Instance.storeCollectionID==CollectionEnum.S && ButtonReferenceManager.Instance.S.Length>0)
            {
                GTQ.Show(true);
                GTQ.changeText("Scaling");

            }
            if (ButtonReferenceManager.Instance.storeCollectionID == CollectionEnum.E && ButtonReferenceManager.Instance.E.Length > 0)
            {
                GTQ.Show(true);
                GTQ.changeText("Ex");

            }
            if (ButtonReferenceManager.Instance.storeCollectionID == CollectionEnum.F && ButtonReferenceManager.Instance.F.Length > 0)
            {
                GTQ.Show(true);
                GTQ.changeText("Filling");

            }
        }
        else
        {
            ButtonReferenceManager.Instance.ARTorP = false;
  
        }
    }

    public void displayShelfItem()
    {
        SetQuizButton();
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
