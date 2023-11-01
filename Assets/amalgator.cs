using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class amalgator : MonoBehaviour
{
    Animator animator;
    int shakeAmt = 10;
    public GICcapsule gicCap;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>(); 
    }

    

    public void shake(GICcapsule gIC)
    {
        gicCap = gIC;
        animator.SetTrigger("close");
        StartCoroutine("RelaseGFIC");
    }

    IEnumerator RelaseGFIC()
    {
        yield return new WaitForSeconds(shakeAmt);
        animator.SetTrigger("open");

        StartCoroutine("GiveBACK");
    }

    IEnumerator GiveBACK()
    {
        yield return new WaitForSeconds(1);
        gicCap.gameObject.SetActive(true);
        gicCap.SetShake();
    }

}
