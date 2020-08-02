using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
public class ZombieAnim : MonoBehaviour
{
    public GameObject theZombie;
    public Zombie zombie;
    public bool isAttacking = false;

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
                theZombie.GetComponent<Animator>().SetBool("isAttacking", true);
                theZombie.GetComponent<Animator>().SetBool("isWalking", false);
                theZombie.GetComponent<NavMeshAgent>().isStopped = true;
                theZombie.GetComponent<NavAI>().enabled = false;
                StartCoroutine(TakeHealth());
                         
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (!zombie.isDead)
        {
            theZombie.GetComponent<Animator>().SetBool("isAttacking", false);
            theZombie.GetComponent<Animator>().SetBool("isWalking", true);
            theZombie.GetComponent<NavMeshAgent>().isStopped = false;
            theZombie.GetComponent<NavAI>().enabled = true;
            StopCoroutine(TakeHealth());
            GetComponent<MeshCollider>().enabled = true;
            isAttacking = false;
        }
    }


    IEnumerator TakeHealth()
    {
        yield return new WaitForSeconds(0.9f);
        //PlayerHealth.playerHealth -= 5;
        isAttacking = false;
        theZombie.GetComponent<Animator>().SetBool("isAttacking", false);
        theZombie.GetComponent<Animator>().SetBool("isWalking", true);
        theZombie.GetComponent<NavMeshAgent>().isStopped = false;
        theZombie.GetComponent<NavAI>().enabled = true;
        GetComponent<MeshCollider>().enabled = true;
    }

    

    
}
