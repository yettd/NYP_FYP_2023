using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAnimationClick : MonoBehaviour
{
    Animator animator;
    [Header("Put in the animation you made here to get the duration")]
    [SerializeField] AnimationClip animationClip;
    private float animationDuration;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        
        animationDuration = animationClip.length;
    }

    public void Clicked()
    {
        animator.SetTrigger("OnClick");
    }

    public float GetAnimationDuration()
    {
        return animationDuration;
    }
}
