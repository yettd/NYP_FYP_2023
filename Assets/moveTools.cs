using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveTools : MonoBehaviour
{
    [SerializeField] Camera cam;
    [SerializeField] RectTransform canvas;
    [SerializeField] Texture2D _brush;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        //if (Input.GetMouseButton(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved))
        //{

        //    //Vector3 pos = cam.ScreenToWorldPoint(Input.GetTouch(0).position);
        //    Vector3 pos = cam.ScreenToWorldPoint(Input.mousePosition);
        //    transform.position = new Vector3(pos.x, pos.y, transform.position.z);

        //}
    }
    private void OnMouseDrag()
    {
        Vector3 pos = cam.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(pos.x, pos.y, transform.position.z);
        Ray ray = cam.ScreenPointToRay(cam.WorldToScreenPoint(pos));
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit))
        {
            TeethDirtClean TDC;
            hit.collider.TryGetComponent<TeethDirtClean>(out TDC);
            if (TDC)
            {
                Debug.Log(hit.collider.gameObject.name);
              //  TDC.Clean(hit, _brush);
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
