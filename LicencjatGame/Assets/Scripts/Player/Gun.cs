using System;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;
    public float impactForce = 60f;
    public float fireRate = 15f;

    public Camera fpsCam;
    public GameObject bloodSplash;
    public ParticleSystem muzzleFlash;

    float nextTimeToFire = 0f;

    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            muzzleFlash.Play();
            Shoot();
        }
        
    }

    private void Shoot()
    {
        
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Zombie zombie = hit.collider.transform.root.GetComponent<Zombie>();
            if (zombie != null)
            {
                if (hit.collider.CompareTag("EnemyHead")) 
                {
                    zombie.TakeDamage(50f);
                    zombie.Slow(2f);
                }
                    
                else if (hit.collider.CompareTag("Enemy")) 
                {
                    zombie.TakeDamage(damage);
                    zombie.Slow(2f);
                }
                    
                
            }
            if (hit.rigidbody != null)
                hit.rigidbody.AddForce(-hit.normal * impactForce);

            GameObject impactGO = Instantiate(bloodSplash, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactGO, .5f);

            Debug.Log(hit.transform.name); 
        }
        
    }
}
