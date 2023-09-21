using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class winAnimation : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.DOScale(new Vector3(.6f, .6f, .6f),1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
