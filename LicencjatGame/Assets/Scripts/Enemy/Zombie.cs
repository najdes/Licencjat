using UnityEngine;
using UnityEngine.AI;

public class Zombie : MonoBehaviour
{
    NavMeshAgent navMeshAgent;
    public  bool isDead=false;
    public float health = 100f;
    float sec;

    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0f)
        {
            Die();
        }
    }

    private void Die()
    {
        gameObject.GetComponent<Animator>().Play("Z_FallingForward");
        isDead = true;
    }
    public void Slow(float amount) {
        sec = 0f;
        Debug.Log("Slowed...");
        float baseSpeed= GetComponent<NavMeshAgent>().speed;
        GetComponent<NavMeshAgent>().speed -= amount;
        WaitForSec();
        GetComponent<NavMeshAgent>().speed = baseSpeed;

    }
    private void WaitForSec()
    {
        if (sec <= 2)
            sec += 1 * Time.deltaTime;
    }


}
