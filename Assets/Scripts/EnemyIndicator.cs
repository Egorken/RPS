using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class EnemyIndicator : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void EnemyRockInd()
    {
        animator.SetTrigger("rockind");
    }
    public void EnemyScissorsInd()
    {
        animator.SetTrigger("scissorsind");
    }
    public void EnemyPaperInd()
    {
        animator.SetTrigger("paperind");
    }
    public void EnemyInd()
    {
        animator.SetTrigger("dotsanim");
    }
}