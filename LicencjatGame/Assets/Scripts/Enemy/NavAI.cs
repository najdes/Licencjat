using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;

public class NavAI : MonoBehaviour
{
    public GameObject destination;
    NavMeshAgent meshAgent;
    private Zombie zombie;
    Animator animator;
    bool isPatroling=true;
    Vector3[] wpTab = new Vector3[3];
    int currentWp = 0;
    int count;
    float speed;

    public bool targetTriggered = false;

    Vector3 zombiePos;
    Vector3 playerPos;
    void Start()
    {
        animator = GetComponent<Animator>();
        meshAgent = GetComponent<NavMeshAgent>();
        destination = GameObject.Find("PlayerModel");
        zombie = GetComponent<Zombie>();
        count = 0;
        speed = meshAgent.speed;
        Transform trans = transform;
        foreach (Transform t in trans)
        {
            if (t.tag == "Waypoint")
            {
                wpTab[count] = t.position;
                count++;
            }
        }
        //foreach (Vector3 i in zombie.GetWpTab())
        // Debug.Log(i);
    }


    void Update()
    {
        //Array.Copy(zombie.GetWpTab(), wpTab, 3);
        zombiePos = meshAgent.transform.position;
        playerPos = destination.transform.position;
        if (!zombie.isDead && Vector3.Distance(zombiePos, playerPos) > 20f)
        {
            meshAgent.speed = 0.4f;
            animator.SetBool("isPatroling", true);
            animator.SetBool("isWalking", false);
            meshAgent.isStopped = false;
            if (Vector3.Distance(wpTab[currentWp], zombiePos) < 4.0f)
            {
                
                currentWp++;
                if (currentWp >= wpTab.Length)
                    currentWp = 0;
            }
            
            meshAgent.SetDestination(wpTab[currentWp]);
            Debug.Log(currentWp);
        }
        else if (!zombie.isDead && Vector3.Distance(zombiePos, playerPos) <= 20f)
        {
            meshAgent.speed = speed;
            animator.SetBool("isWalking", true);
            animator.SetBool("isPatroling", false);
            meshAgent.isStopped = false;
            meshAgent.SetDestination(destination.transform.position);
            targetTriggered = true;
            if (!zombie.isDead && Vector3.Distance(zombiePos, playerPos) > 1f && Vector3.Distance(zombiePos, playerPos) < 1.9f) {
                meshAgent.isStopped = true;
                animator.SetBool("isAttacking", true);
                animator.SetBool("isWalking", false);
            }
            else {
                meshAgent.isStopped = false;
                animator.SetBool("isAttacking", false);
                animator.SetBool("isWalking", true);
            }
            isPatroling = false;
        }
       /* else if (!zombie.isDead && Vector3.Distance(zombiePos, playerPos) > 20f)
        {
            meshAgent.isStopped = false;
            animator.SetBool("isWalking", true);
            animator.SetBool("isIdle", false);
            meshAgent.SetDestination(destination.transform.position);
        }*/
        else
            meshAgent.isStopped = true;
    }
    
}
