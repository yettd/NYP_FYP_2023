using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowMouth : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject[] gums;


    Vector3 ogPos;
    Quaternion ogrotation;
    GameObject ogParent;

    GameObject model;

    [SerializeField] GameObject cursor;
    [SerializeField] Camera cam;
    [SerializeField] RectTransform canvas;
    private void OnEnable()
    {
        if(minigameTaskListController.Instance.GetGumd())
        {
            model = gums[0];
        }
        else
        {

            model = gums[1];
        }
        Debug.Log("asdasd");
        ogPos = model.transform.position;
        ogrotation = model.transform.rotation;
        ogParent = model.transform.parent.gameObject;
        model.transform.parent = transform.GetChild(0).transform;
        foreach (Transform child in model.transform)
        {
            child.gameObject.layer = LayerMask.NameToLayer("UI");
        }

        model.transform.localPosition = new Vector3(0, 0, 0);
        model.transform.localScale = new Vector3(1,1,1);
        model.transform.localRotation = Quaternion.Euler(0, 90, 0);
        model.transform.parent = model.transform.parent.transform.GetChild(0).transform;
    }
    private void OnDisable()
    {
        foreach (Transform child in model.transform)
        {
            child.gameObject.layer = 0;
        }
        transform.GetChild(0).transform.GetChild(0).transform.rotation = Quaternion.Euler(0, 0, 0);
        model.transform.parent = ogParent.gameObject.transform;
        model.transform.position = ogPos;
        model.transform.localRotation = Quaternion.Euler(0, 0, 0);
        model.transform.localScale = new Vector3(1, 1, 1);
    }

    private void OnMouseDown()
    {

    }
    public void GoBack()
    {

        Debug.Log("adasd");
    }

    private void Update()
    {
        if(Input.GetMouseButton(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved))
        {

            //Vector3 pos = cam.ScreenToWorldPoint(Input.GetTouch(0).position);
            Vector3 pos = cam.ScreenToWorldPoint(Input.mousePosition);
            cursor.transform.position=new Vector3(pos.x, pos.y, cursor.transform.position.z);
            
        }
    }
}
