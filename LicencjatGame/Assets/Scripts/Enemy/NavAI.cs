using UnityEngine;
using UnityEngine.AI;
public class NavAI : MonoBehaviour
{
    public GameObject destination;
    NavMeshAgent meshAgent;
    public Zombie zombie;

    public bool targetTriggered = false;

    Vector3 zombiePos;
    Vector3 playerPos;
    void Start()
    {
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
            meshAgent.transform.root.GetComponent<Animator>().SetBool("isWalking", true);
            meshAgent.transform.root.GetComponent<Animator>().SetBool("isIdle", false);
            meshAgent.isStopped = false;
            meshAgent.SetDestination(destination.transform.position);
            targetTriggered = true;
        }
        else if (!zombie.isDead && Vector3.Distance(zombiePos, playerPos) > 40f && targetTriggered)
        {
            meshAgent.isStopped = false;
            meshAgent.transform.root.GetComponent<Animator>().SetBool("isWalking", true);
            meshAgent.transform.root.GetComponent<Animator>().SetBool("isIdle", false);
            meshAgent.SetDestination(destination.transform.position);
        }
        else
            meshAgent.isStopped = true;
            //meshAgent.enabled = false;

        //Debug.Log(""+Vector3.Distance(zombiePos, playerPos));
    }
}
