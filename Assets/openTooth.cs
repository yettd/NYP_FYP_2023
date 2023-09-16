using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class openTooth : MonoBehaviour
{
    public bool topBottom=false;
    TextMeshProUGUI asd;
// Start is called before the first frame update
    private void OnMouseDown()
    {
        if(minigameTaskListController.Instance.currentStep==Steps.LOCATINGS || minigameTaskListController.Instance.currentStep == Steps.LOCATINGF|| minigameTaskListController.Instance.currentStep == Steps.LOCATINGE)
        { 
            minigameTaskListController.Instance.setGame(topBottom);
            asd.text = $"clicked on {gameObject.name}";
        }

    }
    private void Start()
    {
        asd = GameObject.Find("asd").GetComponent<TextMeshProUGUI>();
    }

}
