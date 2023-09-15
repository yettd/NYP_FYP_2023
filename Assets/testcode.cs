using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testcode : MonoBehaviour
{
    public Camera ca;
    [SerializeField] Texture2D _brush;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetMouseButton(0))
        //{
        //    RaycastHit hit;

        //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        //    if (Physics.Raycast(ray, out hit))
        //    {
        //        hit.collider.GetComponent<TeethDirtClean>().Clean(hit);
             
        //    }



        //}
    }
    private void OnMouseDrag()
    {
        Vector3 pos = ca.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(pos.x, pos.y, transform.position.z);
        Ray ray = ca.ScreenPointToRay(ca.WorldToScreenPoint(pos));
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit))
        {
            TeethDirtClean TDC;
            hit.collider.TryGetComponent<TeethDirtClean>(out TDC);
            if(TDC)
            {
            Debug.Log(hit.collider.gameObject.name);
                TDC.Clean(hit,_brush);
            }
            //switch (minigameTaskListController.Instance.procedure)
            //{
            //    case Procedure.Scaling:
            //        hit.collider.GetComponent<TeethDirtClean>().Clean(hit);
            //        break;
            //}
        }
    }
}
