using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowMouth : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject[] gums;

    private void OnEnable()
    {
        if(minigameTaskListController.Instance.GetGumd())
        {
            gums[0].SetActive(true);
        }
        else
        {
            gums[1].SetActive(true);
        }
    }
    private void OnDisable()
    {
        gums[1].SetActive(false);
        gums[0].SetActive(false);
    }
}
