using UnityEngine;
using UnityEngine.AI;

public class Zombie : MonoBehaviour
{
    public TitanSpawner titanSpawner;
    public  bool isDead=false;
    public float health = 100f;
    public float zombieDmg = 2.5f;
    float sec;

    private void Awake()
    {
        titanSpawner = GameObject.Find("TitanSpawner").GetComponent<TitanSpawner>();
    }
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
        if(!isDead)
            titanSpawner.deadZombieCounter++;
        titanSpawner.ChechNumber();
        isDead = true;
    }
    public void Slow(float amount) {
        sec = 0f;
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
