using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DW_NewToothInstance : MonoBehaviour
{
    [SerializeField] private Vector3 offset;
    [SerializeField] private int tierIndex;

    public Vector3 GetOffset()
    {
        return offset;
    }

    public int GetTeethIndex()
    {
        return tierIndex;
    }
}
