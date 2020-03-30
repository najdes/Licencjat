using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
public class ZombieAnim : MonoBehaviour
{

    public GameObject theZombie;
    public Zombie zombie;
    //public GameObject player;
    public bool isAttacking = false;
    //public bool isWalking = false;

    private void Start()
    {
        zombie = transform.root.GetComponent<Zombie>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!zombie.isDead)
        {
            if (!isAttacking)
            {
                isAttacking = true;
                GetComponent<MeshCollider>().enabled = false;
                theZombie.GetComponent<Animator>().Play("Z_Attack");
                theZombie.GetComponent<NavMeshAgent>().enabled = false;
                theZombie.GetComponent<NavAI>().enabled = false;
                StartCoroutine(TakeHealth());
                         
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (!zombie.isDead)
        {
            theZombie.GetComponent<Animator>().Play("Z_Walk_InPlace");
            theZombie.GetComponent<NavMeshAgent>().enabled = true;
            theZombie.GetComponent<NavAI>().enabled = true;
            StopCoroutine(TakeHealth());
            GetComponent<MeshCollider>().enabled = true;
            isAttacking = false;
        }
    }

  /*  private void Update()
    {
        if (!Zombie.isDead)
        {
            if (isInAttackRange())
            {
                if (!isAttacking)
                {
                    isAttacking = true;
                    isWalking = false;
                    theZombie.GetComponent<Animator>().Play("Z_Attack");
                    theZombie.GetComponent<NavMeshAgent>().enabled = false;
                    theZombie.GetComponent<NavAI>().enabled = false;
                    StartCoroutine(TakeHealth());
                }
                else
                    return;
            }
            else
            {
                if (!isWalking)
                {
                    isWalking = true;
                    isAttacking = false;
                    theZombie.GetComponent<Animator>().Play("Z_Walk_InPlace");
                    theZombie.GetComponent<NavMeshAgent>().enabled = true;
                    theZombie.GetComponent<NavAI>().enabled = true;
                    StopCoroutine(TakeHealth());
                }
                else
                    return;
            }

        }
    }

    private bool isInAttackRange()
    {
        if (Vector3.Distance(theZombie.transform.position, player.transform.position) < 6f)
            return true;
        return false;
    }*/

    IEnumerator TakeHealth()
    {
        yield return new WaitForSeconds(0.9f);
        PlayerHealth.playerHealth -= 5;
        isAttacking = false;
        theZombie.GetComponent<Animator>().Play("Z_Walk_InPlace");
        theZombie.GetComponent<NavMeshAgent>().enabled = true;
        theZombie.GetComponent<NavAI>().enabled = true;
        GetComponent<MeshCollider>().enabled = true;
    }

    

    
}
