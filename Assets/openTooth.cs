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
    // Start is called before the first frame update
    private void OnMouseDown()
    {
        if (minigameTaskListController.Instance.IsPause)
        {
            return;
        }

        if (minigameTaskListController.Instance.currentStep == Steps.LOCATINGS || minigameTaskListController.Instance.currentStep == Steps.LOCATINGF || minigameTaskListController.Instance.currentStep == Steps.LOCATINGE)
        {
            if (!minigameTaskListController.Instance.minigameOpen)
            {
                minigameTaskListController.Instance.setGame(topBottom);
                Debug.Log("Click");
            }
            else
            {
                //zoom in the teeth;
                minigameTaskListController.Instance.gonext();
                teethMan.tm.CallClickOn(gameObject.name);

            }
        }
  
    }

    

    private void OnEnable()
    {

    }
    private void Start()
    {
        teethMan.tm.CO += HideTeeth;
        teethMan.tm.Back += Show;
        if (Problem)
        {
            minigameTaskListController.Instance.IncreaseTeethWithProblem();
        }
        mat = GetComponent<Renderer>().material;
        bc = GetComponent<BoxCollider>();
        mc = GetComponent<MeshCollider>();
    }

    private void HideTeeth(string TeethName)
    {
        if(TeethName != gameObject.name)
        {
            mat.SetFloat("_op", 0.1f);
            bc.enabled = false;
            mc.enabled = false;

        }
    }
    private void Show()
    {
        bc.enabled = true;
        mc.enabled = true;
        mat.SetFloat("_op", 1);

    }
}
