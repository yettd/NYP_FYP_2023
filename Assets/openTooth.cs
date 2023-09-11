using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class openTooth : MonoBehaviour
{
    public bool topBottom=false;

// Start is called before the first frame update
    private void OnMouseDown()
    {

        minigameTaskListController.Instance.setGame(topBottom);
        minigameTaskListController.Instance.startminigame();


    }
    private void Start()
    {

    }

}
