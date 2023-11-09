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
    public bool GIC;
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
                //zoom in gum
                minigameTaskListController.Instance.setGame(topBottom);
                teethMan.tm.CT("Swipe to rotate \n Click on teeth to zoom in further",false);
            }
            else
            {
                //zoom in the teeth;
                //minigameTaskListController.Instance.gonext();
                teethMan.tm.CallClickOn(gameObject.name);
                toothFilling tf;
                toothFillingGIC tfGIC;

                TryGetComponent<toothFilling>(out tf);
                teethMan.tm.CT("Click on the correct tool to use for Scaling ", true);
                if (tf)
                {

                    teethMan.tm.CT("Click on the correct tool to use for \nCR Restoration ", true);
                }
                TryGetComponent<toothFillingGIC>(out tfGIC);
                if (tfGIC)
                {

                    teethMan.tm.CT("Click on the correct tool to use for \n GIC Restoration", true);
                }

            }
        }
  
    }

    private void OnEnable()
    {

        //teethMan.tm.back();
    }

    private void Awake()
    {
        for (int i = 0; i < pro.Count; i++)
        {

            if (pro[i] == Procedure.Filling)
            {
                if(!GIC)
                {

                    gameObject.AddComponent<toothFilling>();
                }
                else
                {

                    gameObject.AddComponent<toothFillingGIC>();
                }

            }
        }
    }
    private void Start()
    {
        teethMan.tm.CO += HideTeeth;
        teethMan.tm.Back += Show;

        st = Resources.Load<showTask>("minigameTasklist/notask");
        if (pro.Contains(minigameTaskListController.Instance.procedure))
        {
            switch (minigameTaskListController.Instance.procedure)
            {
                case Procedure.Scaling:
                    
                    GetComponent<TeethDirtClean>().SetProblem();
                    st = Resources.Load<showTask>("minigameTasklist/scaling");
                    break;
                case Procedure.Filling:
                    Debug.Log("asda");
                    if (!GIC)
                    {
                        GetComponent<toothFilling>().setUpProblem();
                        st = Resources.Load<showTask>("minigameTasklist/Filling");
                        break;
                    }
                    GetComponent<toothFillingGIC>().setUpProblem();
                    st = Resources.Load<showTask>("minigameTasklist/Filling2");
                    break;
            }

            minigameTaskListController.Instance.IncreaseTeethWithProblem();
        }
       // mat = GetComponent<Renderer>().material;
        bc = GetComponent<BoxCollider>();
        mc = GetComponent<MeshCollider>();
        teethMan.tm.Back();
    }

    private void HideTeeth(string TeethName)
    {
        mat = GetComponent<Renderer>().material;
        if (TeethName != gameObject.name)
        {
            mat.SetFloat("_op", 0.1f);
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
