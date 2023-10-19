using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DW_GumSectorData : MonoBehaviour
{
    [SerializeField] private Vector3 offset;
    [SerializeField] private Vector3 size;

    #region MAIN
    public Vector3 GetOffsetPoint()
    {
        return offset;
    }

    public Vector3 GetMarkerSize()
    {
        return size;
    }
    #endregion
}
