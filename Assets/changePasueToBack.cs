using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class changePasueToBack : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] Texture[] s;
    public RawImage i;

    public void ChangeButtonSprite()
    {
        if (minigameTaskListController.Instance.minigameOpen==false)
        {
            i.texture = s[0];
            //   minigameTaskListController.Instance.SetTeetch(gameObject);
        }
        else
        {
            i.texture = s[1];
        }
    }
}
