using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    float damage,lifeLength, headMultiplier;

    public void setValues(float dmg, float life,float headMulti)
    {
        damage = dmg;
        lifeLength = life;
        headMultiplier = headMulti;
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag.Equals("Enemy"))
        {
            if (other.gameObject.tag.Equals("EnemyHead"))
            {
                other.transform.root.GetComponent<Zombie>().TakeDamage(damage * headMultiplier);
            }
            else
            {
                other.transform.root.GetComponent<Zombie>().TakeDamage(damage);
            }
            Destroy(gameObject);
        }
    }



}
