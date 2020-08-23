using UnityEngine;
using UnityEngine.AI;

public class Zombie : MonoBehaviour
{
    NavMeshAgent navMeshAgent;
    public  bool isDead=false;
    public float health = 100f;
    public float zombieDmg = 2.5f;
    Vector3[] wpTab = new Vector3[3];
    int count;
    float sec;


    private void Awake()
    {
        count = 0;
        Transform trans = transform;
        foreach (Transform t in trans)
        {
            if (t.tag == "Waypoint")
            {
                Vector3 newWp = new Vector3(Random.Range(-10, 10), 0, Random.Range(-10, 10)) + transform.position;
                if (count == 1)
                {
                    while (Vector3.Distance(newWp, wpTab[count - 1])<6f)
                    {
                        newWp = new Vector3(Random.Range(-10, 10), 0, Random.Range(-5, 15)) + transform.position;
                    }
                    wpTab[count] = newWp;
                    t.position = wpTab[count];
                }
                else if (count == 2)
                {
                    while (Vector3.Distance(newWp, wpTab[count - 1]) < 6f && Vector3.Distance(newWp, wpTab[count - 2]) < 6f )
                    {
                        newWp = new Vector3(Random.Range(-10, 10), 0, Random.Range(-10, 10)) + transform.position;
                    }
                    wpTab[count] = newWp;
                    t.position = wpTab[count];
                }
                else
                {
                    wpTab[count] = newWp;
                    t.position = wpTab[count];
                }
                
            }
        }
    }
    public void RandomizeStats(){

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
