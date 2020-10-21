using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations.Rigging;
using UnityEngine.Rendering;

public class NavAI : MonoBehaviour
{
    public GameObject destination;
    public GameObject aimTarget;
    public bool targetTriggered = false;

    Rig rig;
    NavMeshAgent meshAgent;
    Transform playerTarget;
    private Zombie zombie;
    Animator animator;
    bool isChasing=false;
    public bool isTriggered = false;
    float speed;    

    Vector3 zombiePos;
    Vector3 playerPos;
    void Start()
    {
        animator = GetComponent<Animator>();
        meshAgent = GetComponent<NavMeshAgent>();
        destination = GameObject.Find("PlayerModel");
        zombie = GetComponent<Zombie>();
        rig = GetComponentInChildren<Rig>();
        rig.weight = 0f;
        playerTarget = destination.transform.Find("ConstraintTarget");   
        speed = meshAgent.speed;
    }


    void Update()
    {
        aimTarget.transform.position = playerTarget.position;
        zombiePos = meshAgent.transform.position;
        playerPos = destination.transform.position;
        Vector3 distance = playerPos - zombiePos;
        float angle = Vector3.Angle(distance, transform.forward);

        if (zombie.health < 100f)
            isTriggered = true;

        if (!zombie.isDead && distance.magnitude > 20f && !isTriggered)
        {
            rig.weight = 0f;
            meshAgent.speed = 0.4f;
            animator.SetBool("isPatroling", false);
            animator.SetBool("isIdle",true);
            animator.SetBool("isWalking", false);
            meshAgent.isStopped = false;
           
        }
        else if ((!zombie.isDead && distance.magnitude <= 20f && angle < 40) || (isChasing && !zombie.isDead) || (isTriggered && !zombie.isDead))
        {
            meshAgent.SetDestination(destination.transform.position);
            if (angle > 70)
                rig.weight = (1f / angle) * 40f;
            else
                rig.weight = 1f;
            isChasing = true;
            meshAgent.speed = speed;
            animator.SetBool("isWalking", true);
            animator.SetBool("isIdle", false);
            animator.SetBool("isPatroling", false);
            meshAgent.isStopped = false;
            targetTriggered = true;
            if (!zombie.isDead && distance.magnitude <= 1.5f)
            {
                meshAgent.isStopped = true;
                animator.SetBool("isAttacking", true);
                animator.SetBool("isWalking", false);
            }
            else
            {
                meshAgent.isStopped = false;
                animator.SetBool("isAttacking", false);
                animator.SetBool("isWalking", true);
            }
        }
        else 
        {
            meshAgent.isStopped = true;
            rig.weight = 0f;
        }

    }
    
}
