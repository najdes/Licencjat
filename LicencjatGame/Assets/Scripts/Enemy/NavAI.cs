using UnityEngine;
using UnityEngine.AI;
public class NavAI : MonoBehaviour
{
    public GameObject destination;
    NavMeshAgent meshAgent;
    private Zombie zombie;
    Animator animator;

    public bool targetTriggered = false;

    Vector3 zombiePos;
    Vector3 playerPos;
    void Start()
    {
        animator = GetComponent<Animator>();
        meshAgent = GetComponent<NavMeshAgent>();
        destination = GameObject.Find("Player");
        zombie = GetComponent<Zombie>();
    }


    void Update()
    {
        zombiePos = meshAgent.transform.position;
        playerPos = destination.transform.position;
        if (!zombie.isDead && Vector3.Distance(zombiePos, playerPos) <= 40f)
        {
            animator.SetBool("isWalking", true);
            animator.SetBool("isIdle", false);
            meshAgent.isStopped = false;
            meshAgent.SetDestination(destination.transform.position);
            targetTriggered = true;


        }
        else if (!zombie.isDead && Vector3.Distance(zombiePos, playerPos) > 40f && targetTriggered)
        {
            meshAgent.isStopped = false;
            animator.SetBool("isWalking", true);
            animator.SetBool("isIdle", false);
            meshAgent.SetDestination(destination.transform.position);
        }
        else
            meshAgent.isStopped = true;
    }
    
}
