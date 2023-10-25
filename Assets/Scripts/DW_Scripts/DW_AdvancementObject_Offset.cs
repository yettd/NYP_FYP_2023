using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DW_AdvancementObject_Offset : MonoBehaviour
{
    [SerializeField] private Vector3 offset;

    public Vector3 GetPosition(Vector3 position)
    {
        return position + offset;
    }
}
