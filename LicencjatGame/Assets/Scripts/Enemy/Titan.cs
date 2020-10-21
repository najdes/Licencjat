using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Titan : MonoBehaviour
{
    public float damage = 10f;
    public float slamDamage = 25f;
    public float speed = 5f;
    public float health = 1500f;
    public bool isAlive = true;
    bool isDmgTaken = false;

    public void TakeDamage(float dmg)
    {
        isDmgTaken = true;
        health -= dmg;
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
    }
}
