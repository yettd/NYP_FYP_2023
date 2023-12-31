using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

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
    public bool problem = false;
    
    [SerializeField] List<Procedure> pro = new List<Procedure>();
    // Start is called before the first frame update
    private void OnMouseDown()
    {
        if (minigameTaskListController.Instance.IsPause)
        {
            return;
        }

        //if camera is not zoom
        if (!cameraChanger.Instance.GetZoom())
        {
            //if(the minigame panel is not open open it;
            if (!minigameTaskListController.Instance.minigameOpen)
            {
                //zoom in gum
                minigameTaskListController.Instance.setGame(topBottom);
                teethMan.tm.CT("Swipe to rotate \n Click on a teeth to zoom in further",false);
            }
            else //else it will zoom in on to the tooth and the other tooth will become transparent
            {
                if (problem)
                {
                    //zoom in the teeth;
                    teethMan.tm.CallClickOn(gameObject.name);
                    toothFilling tf;
                    toothFillingGIC tfGIC;

                    TryGetComponent<toothFilling>(out tf);
                    if(minigameTaskListController.Instance.procedure==Procedure.Extration)
                    {
                        teethMan.tm.CT("Click on the correct tool to use for Extraction ", true);
                        teethMan.tm.s = "Click on the correct tool to use for Extraction ";
                        return;
                    }
                    teethMan.tm.CT("Click on the correct tool to use for Scaling ", true);
                    teethMan.tm.s = "Click on the correct tool to use for Scaling ";
                    if (tf)
                    {

                        teethMan.tm.CT("Click on the correct tool to use for \nCR Restoration ", true);
                        teethMan.tm.s = "Click on the correct tool to use for \nCR Restoration  ";
                    }
                    TryGetComponent<toothFillingGIC>(out tfGIC);
                    if (tfGIC)
                    {
                        teethMan.tm.CT("Click on the correct tool to use for \n GIC Restoration", true);
                        teethMan.tm.s = "Click on the correct tool to use for \n GIC Restoration ";
                    }
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
        if (pro.Contains(minigameTaskListController.Instance.procedure)) // if the minigame correlate with the teeth run their setup or add their respective code
        {
            switch (minigameTaskListController.Instance.procedure)
            {
                case Procedure.Scaling:
                    
                    GetComponent<TeethDirtClean>().SetProblem();
                    problem = true;
                    st = Resources.Load<showTask>("minigameTasklist/scaling");
                    break;
                case Procedure.Filling:
                    Debug.Log("asda");
                    if (!GIC)
                    {
                        GetComponent<toothFilling>().setUpProblem();
                        problem = true;
                        st = Resources.Load<showTask>("minigameTasklist/Filling");
                        break;
                    }
                    GetComponent<toothFillingGIC>().setUpProblem();
                    problem = true;
                    st = Resources.Load<showTask>("minigameTasklist/Filling2");
                    break;
                case Procedure.Extration:
                    st = Resources.Load<showTask>("minigameTasklist/Extraction");
                    problem = true;
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
        // if gameobject is not TeethName will become transparent
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
            minigameTaskListController.Instance.startminigame(st,WhichStepIsOn);//give the stem to minigame and which step it was last left off
            cameraChanger.Instance.ZoomInCam(gameObject);
        }
    }
    private void Show()
    {

        // change back the opacity of the teeth
        mat = GetComponent<Renderer>().material;
        // bc.enabled = true;
        mc.enabled = true;
        mat.SetFloat("_op", 1);

        if(focus)
        {
            focus = false;
            WhichStepIsOn = minigameTaskListController.Instance.getCSValue();//if it was the tooth that was focus on set whichstepison to the last step id

        }

    }
}
