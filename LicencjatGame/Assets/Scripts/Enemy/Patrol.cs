using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Patrol : StateMachineBehaviour
{
    public GameObject Npc;
    public GameObject[] waypoints;
    public int currentWP;
    public NavMeshAgent agent;
    bool isWpset = false;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent = Npc.GetComponent<NavMeshAgent>();
        currentWP = 0;
        Npc = animator.gameObject;
        if (!isWpset)
        {
            
            float npcPosx = Npc.transform.position.x;
            float npcPosy = Npc.transform.position.y;
            float npcPosz = Npc.transform.position.z;
            for (int i = 0; i < waypoints.Length; i++)
            {
                Instantiate(waypoints[i], new Vector3(npcPosx + Random.Range(-10, 10), npcPosy, npcPosz + Random.Range(-10, 10)), Quaternion.identity);
            }
            isWpset = true;
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (waypoints.Length == 0)
            return;
        if(Vector3.Distance(waypoints[currentWP].transform.position, Npc.transform.position) < 3.0f)
        {
            currentWP++;
            if (currentWP >= waypoints.Length)
                currentWP = 0;
        }

        agent.SetDestination(waypoints[currentWP].transform.position);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
