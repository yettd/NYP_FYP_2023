using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARcontrol : MonoBehaviour
{

    protected GameObject objectToControl;

    public virtual void assignOTC(GameObject OTC)
    {
        objectToControl = OTC;
    }
}
