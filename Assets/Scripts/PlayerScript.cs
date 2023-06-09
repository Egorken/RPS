using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerScript : MonoBehaviour
{
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void OnButton1Click()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("IdleTrigger"))
        {
            animator.SetTrigger("Attack1Trigger");
        }
    }

    public void OnButton2Click()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("IdleTrigger"))
        {
            animator.SetTrigger("Attack2Trigger");
        }
    }

    public void OnButton3Click()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("IdleTrigger"))
        {
            animator.SetTrigger("Attack3Trigger");
        }
    }

    public void Attack1End()
    {
        animator.SetTrigger("IdleTrigger");
    }

    public void HitT()
    {
        animator.SetTrigger("HitTrigger");
    }
}
