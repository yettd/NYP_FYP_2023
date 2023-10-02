using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testcode : MonoBehaviour
{
    public Camera ca;
    [SerializeField] Texture2D _brush;
    Material mat;
    // Start is called before the first frame update
    void Start()
    {
        mat = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        mat.SetFloat("_op", 0);
    }
    private void OnMouseDrag()
    {
        
        
    }
}
