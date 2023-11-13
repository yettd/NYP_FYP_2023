using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Hoverover : MonoBehaviour , IPointerEnterHandler, IPointerExitHandler
{
    rotateJaws rj;
  
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (rj == null)
        {
            rj=FindObjectOfType<rotateJaws>();
        }
        rj.overUI = true;
        Debug.Log("test");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        rj.overUI = false;
        Debug.Log("testDONE");
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
