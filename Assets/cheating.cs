using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cheating : MonoBehaviour
{
    // Start is called before the first frame update
    public void cleanALLTeeth()
    {
        minigameTaskListController.Instance.ResumeGame();
        TeethDirtClean[] a = FindObjectsByType<TeethDirtClean>(FindObjectsSortMode.None); ;

        foreach (TeethDirtClean b in a)
        {
            b.Cheat();
        }
    }
}
