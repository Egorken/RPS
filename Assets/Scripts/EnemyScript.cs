using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyScript : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void EnemyRock()
    {
            animator.SetTrigger("EnemyA1");
    }
    public void EnemyScissors()
    {
            animator.SetTrigger("EnemyA2");
    }
    public void EnemyPapper()
    {
            animator.SetTrigger("EnemyA3");
    }
    public void IdleEnemy()
    {
        animator.SetTrigger("IdleTriggerEnemy");
    }
}
