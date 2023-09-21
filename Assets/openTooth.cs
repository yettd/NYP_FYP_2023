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
// Start is called before the first frame update
    private void OnMouseDown()
    {
        if(minigameTaskListController.Instance.IsPause)
        {
            return;
        }

        if(minigameTaskListController.Instance.currentStep==Steps.LOCATINGS || minigameTaskListController.Instance.currentStep == Steps.LOCATINGF|| minigameTaskListController.Instance.currentStep == Steps.LOCATINGE)
        { 
            minigameTaskListController.Instance.setGame(topBottom);
         //   minigameTaskListController.Instance.SetTeetch(gameObject);
        }

    }
    private void Start()
    {
        if(Problem)
        {
            minigameTaskListController.Instance.IncreaseTeethWithProblem();
        }
    }

}
