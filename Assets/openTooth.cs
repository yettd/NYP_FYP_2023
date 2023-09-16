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

        minigameTaskListController.Instance.setGame(topBottom);
        Debug.Log($"{minigameTaskListController.Instance.GetGumd()} : {topBottom}");
        minigameTaskListController.Instance.startminigame();
        asd.text = $"clicked on {gameObject.name}";

    }
    private void Start()
    {
        asd = GameObject.Find("asd").GetComponent<TextMeshProUGUI>();
    }

}
