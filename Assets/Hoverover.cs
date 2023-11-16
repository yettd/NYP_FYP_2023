using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Hoverover : MonoBehaviour , IPointerEnterHandler, IPointerExitHandler
{
    public rotateJaws rj;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (rj == null)
        {
            rj=FindObjectOfType<rotateJaws>();
        }
        rj.overUI = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        rj.overUI = false;
    }

 



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
