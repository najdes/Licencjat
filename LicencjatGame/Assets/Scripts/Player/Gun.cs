using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Gun : MonoBehaviour
{
    public float damage = 10f;
    public float impactForce = 60f;
    public int bulletSpeed = 30;
    public float fireRate = 15f;
    public int magCapacity = 30;
    public float life = 5f;
    public int currentAmmunition;
    public float relaodTime = 0.1f;
    public float headMulti = 2f;
    public float range = 2f;
    public int HoldAmmo { set; get; }
    public bool IsEnable { set; get; } = false;
    public bool isReloading = false;
    public Camera fpsCam;
    public GameObject bloodSplash;
    public GameObject currAmmo;
    public GameObject bulletGO;
    public GameObject pelletGO;
    public GameObject magAmmoGO;
    public GameObject holdAmmoGO;
    public Animator animator;
    public ParticleSystem muzzleFlash;

    float nextTimeToFire = 0f;
    private void Start() {
        if (transform.parent.CompareTag("Pistol"))
            IsEnable = true;
        
        currentAmmunition = magCapacity;

        AmmoUptade();
    }
    void Update()
    {
        //ShowAmmo();
        if (isReloading)
            return;
        if (transform.parent.CompareTag("Ak") || transform.parent.CompareTag("M4"))
        {
            if ((currentAmmunition <= 0 || Input.GetKeyDown("r")) && GunSwitch.currentRifleAmmo > 0 && currentAmmunition<magCapacity)
            {
                StartCoroutine(Reload());
                return;
            }
        }
        else if (transform.parent.CompareTag("Shotgun"))
        {
            if ((currentAmmunition <= 0 || Input.GetKeyDown("r")) && GunSwitch.currentShotgunAmmo > 0 &&currentAmmunition < magCapacity)
            {
                StartCoroutine(Reload());
                return;
            }
        }
        else if (transform.parent.CompareTag("Pistol") && currentAmmunition < magCapacity)
        {
            if ((currentAmmunition <= 0 || Input.GetKeyDown("r")))
            {
                StartCoroutine(Reload());
                return;
            }
        }

        if (transform.parent.CompareTag("Ak") || transform.parent.CompareTag("M4"))
        {
            if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire && currentAmmunition > 0)
            {
                nextTimeToFire = Time.time + 1f / fireRate;
                muzzleFlash.Play();
                Shoot();
            }
        }
        else
        {
            if (Input.GetButtonDown("Fire1") && Time.time >= nextTimeToFire && currentAmmunition > 0)
            {
                nextTimeToFire = Time.time + 1f / fireRate;
                muzzleFlash.Play();
                Shoot();
            }
        }

    }

    private void Shoot()
    {
        currentAmmunition--;
        Vector3 middle = fpsCam.transform.forward;
        if (transform.parent.CompareTag("Shotgun"))
        {
            
            MakeBloomShoot(middle);
        }
        else
        {
            MakeSingleBullet(middle,0f,0f,0f,bulletGO,damage);
        }    
    }


    IEnumerator Reload(){
        animator.SetBool("isReloading", true);
        isReloading = true;
        yield return new WaitForSeconds(relaodTime - .25f);
        animator.SetBool("isReloading", false);
        yield return new WaitForSeconds(.25f);
        if (!transform.parent.CompareTag("Pistol"))
        {
            if (transform.parent.CompareTag("Ak") || transform.parent.CompareTag("M4"))
            {
                int _magAmmo = currentAmmunition;
                int _holdAmmo = GunSwitch.currentRifleAmmo;

                if (_magAmmo > 0)
                {
                    _holdAmmo -= (magCapacity - _magAmmo);

                }
                else
                {
                    _holdAmmo -= magCapacity;
                }

                if (GunSwitch.currentRifleAmmo >= magCapacity)
                {
                    _magAmmo = magCapacity;
                }
                else
                {
                    _magAmmo = GunSwitch.currentRifleAmmo;
                }
                if (_holdAmmo >= 0)
                    GunSwitch.currentRifleAmmo = _holdAmmo;
                else
                    GunSwitch.currentRifleAmmo = 0;
                currentAmmunition = _magAmmo;
            }
            else if(transform.parent.CompareTag("Shotgun"))
            {
                int _magAmmo = currentAmmunition;
                int _holdAmmo = GunSwitch.currentShotgunAmmo;

                if(_magAmmo > 0)
                {
                    _holdAmmo -= (magCapacity - _magAmmo);

                }
                else
                {
                    _holdAmmo -= magCapacity;
                }

                if (GunSwitch.currentShotgunAmmo >= magCapacity)
                {
                    _magAmmo = magCapacity;
                }
                else
                {
                    _magAmmo = GunSwitch.currentShotgunAmmo;
                }
                if (_holdAmmo >= 0)
                    GunSwitch.currentShotgunAmmo = _holdAmmo;
                else
                    GunSwitch.currentShotgunAmmo = 0;
                currentAmmunition = _magAmmo;
            }
           
            isReloading = false;
        }
        else
        {
            currentAmmunition = magCapacity;
            isReloading = false;
        }
            
    }
    void ShowAmmo()
    {
        currAmmo.GetComponent<Text>().text = currentAmmunition.ToString();
        magAmmoGO.GetComponent<Text>().text = magCapacity.ToString();
        if(transform.parent.CompareTag("Shotgun"))
            holdAmmoGO.GetComponent<Text>().text = GunSwitch.currentShotgunAmmo.ToString();
        else if (transform.parent.CompareTag("Pistol"))
            holdAmmoGO.GetComponent<Text>().text = HoldAmmo.ToString();
        else
            holdAmmoGO.GetComponent<Text>().text = GunSwitch.currentRifleAmmo.ToString();

    }

    void MakeSingleBullet(Vector3 shootLine, float x, float y, float z, GameObject projectile,float singleDmg)
    {
        GameObject bullet = Instantiate(projectile);
        Rigidbody bulletRigidBody = bullet.GetComponent<Rigidbody>();
        BulletScript bulletScript = bullet.GetComponent<BulletScript>();

        bulletScript.setValues(singleDmg, life, headMulti,range);
        bullet.transform.position = transform.position;
        float angle = Vector3.Angle(transform.position, shootLine);
        Debug.Log(angle);
        bullet.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        Vector3 rotatedShot = Quaternion.Euler(x, y, z) * shootLine;
        bulletRigidBody.velocity = rotatedShot * bulletSpeed;
    }
    private void MakeBloomShoot(Vector3 middle)
    {
        float fixedDmg = damage / 13f;
        MakeSingleBullet(middle, 0, -7, 7, pelletGO, fixedDmg);//lewa \/
        MakeSingleBullet(middle, 0,  -7, 2, pelletGO, fixedDmg);
        MakeSingleBullet(middle, 0, -7, -2, pelletGO, fixedDmg);
        MakeSingleBullet(middle, 0, -7, 7, pelletGO, fixedDmg);
        MakeSingleBullet(middle, 0, -5, -5, pelletGO, fixedDmg);
        MakeSingleBullet(middle, 0, -5, 5, pelletGO, fixedDmg);
        MakeSingleBullet(middle, 0, 0, 0, pelletGO, fixedDmg);//srodkowy strzal
        MakeSingleBullet(middle, 0, 5, 5, pelletGO, fixedDmg);
        MakeSingleBullet(middle, 0, 5, -5, pelletGO, fixedDmg);
        MakeSingleBullet(middle, 0, 7, 7, pelletGO, fixedDmg);
        MakeSingleBullet(middle, 0, 7, -2, pelletGO, fixedDmg);
        MakeSingleBullet(middle, 0, 7, 2, pelletGO, fixedDmg);
        MakeSingleBullet(middle, 0, 7, 7, pelletGO, fixedDmg);//prawa /\
    }

    void AmmoUptade()
    {
        if (transform.parent.CompareTag("Shotgun"))
            HoldAmmo = GunSwitch.currentShotgunAmmo;
        else if (transform.parent.CompareTag("Pistol"))
            HoldAmmo = 999;
        else
            HoldAmmo = GunSwitch.currentRifleAmmo;
    }
}
