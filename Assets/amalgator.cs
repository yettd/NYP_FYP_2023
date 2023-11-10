using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class amalgator : MonoBehaviour
{
    Animator animator;
    int shakeAmt = 10;
    public GICcapsule gicCap;

    public GameObject fakeGic;
    // Start is called before the first frame update
    void Start()
    {
        fakeGic.SetActive(false);
        animator = GetComponent<Animator>(); 
    }

    

    public void shake(GICcapsule gIC)
    {

        fakeGic.SetActive(true);
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

        fakeGic.SetActive(false);
        teethMan.tm.ct("put it in the applicator to start using it", true);
        gicCap.gameObject.SetActive(true);
        gicCap.SetShake();
    }

}
