using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    float damage,lifeLength, headMultiplier;
    float startTime,range;
    private void Start()
    {
        startTime = Time.time;
    }
    private void Update()
    {
        if (Time.time - startTime >= lifeLength)
            //Destroy(gameObject);

        doDamage();
    }
    public void setValues(float dmg, float life,float headMulti,float checkRange)
    {
        damage = dmg;
        lifeLength = life;
        headMultiplier = headMulti;
        range = checkRange;
    }
    private void doDamage()
    {
        RaycastHit target;
        
        if (Physics.Raycast(transform.position,transform.up, out target, range))
        {        
            if (target.collider.CompareTag("Enemy"))
            {
                if (target.collider.CompareTag("EnemyHead"))
                {
                    target.transform.root.GetComponent<Zombie>().TakeDamage(damage * headMultiplier);
                }
                else
                {
                    target.transform.root.GetComponent<Zombie>().TakeDamage(damage);
                }
                Debug.Log(target.collider.tag);
                Destroy(gameObject);
            }
        }
        
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
            Debug.Log(other.gameObject.tag);
            Destroy(gameObject);
        }
    }



}
