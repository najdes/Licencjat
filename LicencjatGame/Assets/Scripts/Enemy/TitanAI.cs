using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TitanAI : Titan
{
    public GameObject playerDestination;
    public GameObject slamZone;
    public Titan titan;
    Animator animator;
    Vector3 playerPos;
    Vector3 titanPos;
    Vector3 distance;
    NavMeshAgent meshAgent;

    bool isTriggered = false;
    bool isShouting = false;
    bool isTakingDmg = false;
    bool isAttacking = false;
    int shout=1;
    float shouting = 79f;
    float specialAttack = 0f;
    private void Start()
    { 
        playerDestination = GameObject.Find("PlayerModel");
        animator = GetComponent<Animator>();
        meshAgent = GetComponent<NavMeshAgent>();
        meshAgent.speed = speed;
    }
    private void Update()
    {       
        specialAttack += Time.deltaTime;
        playerPos = playerDestination.transform.position;
        titanPos = meshAgent.transform.position;
        distance = titanPos - playerPos;
        meshAgent.SetDestination(playerPos);

        if (isShouting || isTakingDmg || isAttacking)
            return;

        slamZone.SetActive(false);
        shouting += Time.deltaTime;

        if(titan.IsAlive() && distance.magnitude > 30f && !isTriggered)
        {
            meshAgent.speed = speed;
            animator.SetBool("isWalking", true);
            if (TookDamage()) StartCoroutine(TakeDmg());
            MakeShout();
        }
        else if(titan.IsAlive() && distance.magnitude <= 30f || isTriggered)
        {

            meshAgent.speed = 8;
            animator.SetBool("isWalking", false);
            animator.SetBool("isRunning", true);
            isTriggered = true;
            meshAgent.isStopped = false;

            if (meshAgent.remainingDistance < 3f)
            {
                LookAtPlayer();
                meshAgent.isStopped = true;
                animator.SetBool("isRunning", false);
                if (specialAttack >= 25f)
                {
                    StartCoroutine(SpecialAttack());
                }
                else
                {
                    animator.SetBool("isAttacking2", true);
                    animator.SetBool("isAttacking3", false);
                }
            }
            
            else
            {
                meshAgent.isStopped = false;
                animator.SetBool("isRunning", true);
                animator.SetBool("isAttacking2", false);
                animator.SetBool("isAttacking3", false);
                animator.SetBool("isShouting1", false);
                animator.SetBool("isShouting2", false);
                if (TookDamage()) StartCoroutine(TakeDmg());
                MakeShout();
            }

        }
        else
        {
            meshAgent.isStopped = true;         
        } 
       
    }

    private void LookAtPlayer()
    {
            distance.y = 0f;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(distance) * Quaternion.Euler(0, 180, 0), Time.time * 1f);
    }

    void MakeShout()
    {
        if (shouting >= 80f)
        {
           
             StartCoroutine(Shout());
           
        }
        
        
    }
    IEnumerator Shout()
    {
        isShouting = true;
        animator.SetBool("isWalking", false);
        animator.SetBool("isRunning", false);
        meshAgent.isStopped = true;
        if (shout == 1)
        {
            animator.SetBool("isShouting1", true);
        }

        else
        {

            animator.SetBool("isShouting2", true);
        }

        shout++;
        if (shout > 2) shout = 1;
        shouting = 0f;
        yield return new WaitForSeconds(2f);
        animator.SetBool("isShouting1", false);
        animator.SetBool("isShouting2", false);
        isShouting = false;
        meshAgent.isStopped = false;
    }
    IEnumerator TakeDmg()
    {
        isTakingDmg = true;
        animator.SetBool("isWalking", false);
        animator.SetBool("isRunning", false);
        animator.SetBool("isHit", true);
        meshAgent.isStopped = true;
        yield return new WaitForSeconds(0.3f);
        isTakingDmg=false;
        meshAgent.isStopped = false;
        animator.SetBool("isHit", false);
    }
    IEnumerator SpecialAttack()
    {
        isAttacking = true;
        animator.SetBool("isWalking", false);
        animator.SetBool("isRunning", false);
        animator.SetBool("isAttacking2", false);
        animator.SetBool("isAttacking3", true);
        yield return new WaitForSeconds(4f);
        isAttacking = false;
        animator.SetBool("isAttacking3", false);
        specialAttack = 0f;
        slamZone.SetActive(false);
    }
}
