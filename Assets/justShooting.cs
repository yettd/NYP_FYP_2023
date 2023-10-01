using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class justShooting : MonoBehaviour
{
    Ray ray;
    // Start is called before the first frame update
    void Start()
    {
        ray = new Ray(transform.position, transform.forward);
    }

    // Update is called once per frame
    void Update()
    {

        if(Physics.Raycast(ray,out RaycastHit hiy) && Input.GetKeyDown("j"))
        {
            hiy.collider.gameObject.GetComponent<raycastDirection>().RaycastDir(hiy,ray);
        }
    }
}
