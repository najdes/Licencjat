using UnityEditor.Animations.Rigging;
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
    //bool isPatroling = true;
    Vector3[] wpTab = new Vector3[3];
    int currentWp = 0;
    int count;
    float speed;    

    Vector3 zombiePos;
    Vector3 playerPos;
    void Start()
    {
        animator = GetComponent<Animator>();
        meshAgent = GetComponent<NavMeshAgent>();
        destination = GameObject.Find("PlayerModel");
        zombie = GetComponent<Zombie>();
        count = 0;
        rig = GetComponentInChildren<Rig>();
        rig.weight = 0f;
        playerTarget = destination.transform.Find("ConstraintTarget");
        
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
    }


    void Update()
    {
        aimTarget.transform.position = playerTarget.position;
        zombiePos = meshAgent.transform.position;
        playerPos = destination.transform.position;
        //Vector3 targetdistance = aimTarget.transform.position - zombiePos;
        //Debug.Log(Vector3.Angle(targetdistance, transform.forward));
        Vector3 distance = playerPos - zombiePos;
        float angle = Vector3.Angle(distance, transform.forward);
        if (!zombie.isDead && distance.magnitude > 20f)
        {
            rig.weight = 0f;
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

        }
        else if ((!zombie.isDead && distance.magnitude <= 20f && angle < 30) || isChasing && !zombie.isDead)
        {
            if (angle > 70)
                rig.weight = (1f / angle) * 40f;
            else
                rig.weight = 1f;
            isChasing = true;
            if (distance.magnitude > 20f)
                isChasing = false;
            meshAgent.speed = speed;
            animator.SetBool("isWalking", true);
            animator.SetBool("isPatroling", false);
            meshAgent.isStopped = false;
            meshAgent.SetDestination(destination.transform.position);
            targetTriggered = true;
            if (!zombie.isDead && distance.magnitude <= 2f)
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
