using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class openTooth : MonoBehaviour
{
    public bool topBottom=false;
    [SerializeField]bool Problem;
    TextMeshProUGUI asd;
    Material mat;
    BoxCollider bc;
    MeshCollider mc;
    public showTask st;
    bool focus;
    int WhichStepIsOn;
    [SerializeField] List<Procedure> pro = new List<Procedure>();
    // Start is called before the first frame update
    private void OnMouseDown()
    {
        if (minigameTaskListController.Instance.IsPause)
        {
            return;
        }

        if (!cameraChanger.Instance.GetZoom())
        {
            if (!minigameTaskListController.Instance.minigameOpen)
            {
                minigameTaskListController.Instance.setGame(topBottom);
         
            }
            else
            {
                //zoom in the teeth;
                //minigameTaskListController.Instance.gonext();
                teethMan.tm.CallClickOn(gameObject.name);

            }
        }
  
    }

    private void OnEnable()
    {

      //  teethMan.tm.back();
    }

    private void Awake()
    {
        for (int i = 0; i < pro.Count; i++)
        {
            if (pro[i]==Procedure.Scaling)
            {
                gameObject.AddComponent<TeethDirtClean>();
            }

            else if (pro[i] == Procedure.Filling)
            {
                gameObject.AddComponent<toothFilling>();
            }
        }
    }
    private void Start()
    {
        teethMan.tm.CO += HideTeeth;
        teethMan.tm.Back += Show;
        if (pro.Contains(minigameTaskListController.Instance.procedure))
        {

            switch (minigameTaskListController.Instance.procedure)
            {
                case Procedure.Scaling:
                    
                    GetComponent<TeethDirtClean>().SetProblem();
                    st = Resources.Load<showTask>("minigameTasklist/scaling");
                    break;
                case Procedure.Filling:
                    GetComponent<toothFilling>().setUpProblem();
                    st = Resources.Load<showTask>("minigameTasklist/Filling");
                    break;
            }

            minigameTaskListController.Instance.IncreaseTeethWithProblem();
        }
       // mat = GetComponent<Renderer>().material;
        bc = GetComponent<BoxCollider>();
        mc = GetComponent<MeshCollider>();

    }

    private void HideTeeth(string TeethName)
    {
        mat = GetComponent<Renderer>().material;
        if (TeethName != gameObject.name)
        {
            mat.SetFloat("_op", 0.1f);
            Debug.Log(mat.GetFloat("_op"));
            //bc.enabled = false;
            mc.enabled = false;

        }
        else
        {
            focus = true;

            minigameTaskListController.Instance.startminigame(st,WhichStepIsOn);
            cameraChanger.Instance.ZoomInCam(gameObject);
        }
    }
    private void Show()
    {

        mat = GetComponent<Renderer>().material;
        // bc.enabled = true;
        mc.enabled = true;
        mat.SetFloat("_op", 1);

        if(focus)
        {
            focus = false;
            WhichStepIsOn = minigameTaskListController.Instance.getCSValue();
     
        }

    }
}
