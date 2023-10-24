using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class getMesh : MonoBehaviour
{

    public Mesh mesh;
    public Material mat;
    // Start is called before the first frame update
    public Mesh getM()
    {
        return mesh;
    }
    public Material getR()
    {
        return mat;
    }
}
