using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hitPoint : MonoBehaviour
{
    public fillingCure fc;
    // Start is called before the first frame update
    void Start()
    {
        fc=FindAnyObjectByType<fillingCure>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseEnter()
    {
        fc.GetHit(transform);
    }

    public void hit()
    {
        fc.GetHit(transform);
    }
}
