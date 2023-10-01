using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class raycastDirection : MonoBehaviour
{
    BoxCollider bc;
    // Start is called before the first frame update
    void Start()
    {
        bc = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RaycastDir(RaycastHit hit, Ray ray)
    {
        //Vector3 dirToRay =  hit.normal;
        //Vector3 forward = bc.center + transform.forward;

        //Debug.LogError(dirToRay);
        //Debug.Log($"{dirToRay.normalized} :: {forward.normalized}");

        //float angle = Vector3.Angle(forward, hit.point);

        //Vector3 cross = Vector3.Cross(transform.forward, hit.point);
        //if (cross.y < 0)
        //{
        //    Debug.Log("left");
        //}
        //else
        //{
        //    Debug.Log("right");
        //}


        Vector3 hitPointLocal = transform.GetChild(0).InverseTransformPoint(hit.point);

        float xAbs = Mathf.Abs(hitPointLocal.x);
        float zAbs = Mathf.Abs(hitPointLocal.z);

        string positionRelativeToEnemy = "";

        if (xAbs > zAbs)
        {
            // Hit point is either "left" or "right" relative to the enemy
            if (hitPointLocal.x > 0)
            {
                positionRelativeToEnemy = "right";
            }
            else
            {
                positionRelativeToEnemy = "left";
            }
        }
        else
        {
            // Hit point is either "front" or "back" relative to the enemy
            if (hitPointLocal.z > 0)
            {
                positionRelativeToEnemy = "front";
            }
            else
            {
                positionRelativeToEnemy = "back";
            }
        }
        Debug.Log(positionRelativeToEnemy);
    }
}
