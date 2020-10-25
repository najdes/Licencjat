using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class Titan : MonoBehaviour
{
    public float damage = 10f;
    public float slamDamage = 25f;
    public float speed = 5f;
    [SerializeField]
    float health = 1500f;
    bool isAlive = true;
    bool isDmgTaken = false;

    private void Update()
    {
        Debug.Log(IsAlive());
    }
    public void TakeDamage(float dmg)
    {
        isDmgTaken = true;
        health -= dmg;
        Debug.Log(health);
        if (health <= 0) Die();
    }
    public bool TookDamage()
    {
        if (isDmgTaken)
        {
            isDmgTaken = false;
            return true;   
        }
        else
        {
            return false;
        }
    }
    public void Die()
    {
        isAlive = false;
        gameObject.GetComponent<Animator>().Play("Death");
        gameObject.GetComponent<NavMeshAgent>().isStopped = true;
        PauseMenu pause = GameObject.FindGameObjectWithTag("Menu").GetComponent<PauseMenu>();
        pause.Win();
    }
    public bool IsAlive()
    {
        return isAlive;
    }
}
