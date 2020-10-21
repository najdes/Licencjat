using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

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
    public float range = 200f;
    public float upRecoil = 2f;
    public float sideNegativeRecoil = -1f;
    public float sidePositiveRecoil = 1f;
    public int HoldAmmo { set; get; }
    public bool IsEnable { set; get; } = false;
    public bool isReloading = false;
    public Camera fpsCam;
    public GameObject impactGO;
    public Animator animator;
    public ParticleSystem muzzleFlash;

    public AudioSource shotSound;
    public AudioSource reloadSound;
    public AudioSource finishingReload;

    float nextTimeToFire = 0f;

    private void Start()
    {
        if (IsPistol())
            IsEnable = true;
        currentAmmunition = magCapacity;

        AmmoUptade();
    }
    void Update()
    {
        if (PauseMenu.GameIsPaused)
            return;

        SetAnimation();
        //ShowAmmo();
        if (isReloading)
            return;
        if (IsAk() || IsM4())
        {
            if ((currentAmmunition <= 0 || Input.GetKeyDown("r")) && GunSwitch.currentRifleAmmo > 0 && currentAmmunition < magCapacity)
            {
                StartCoroutine(Reload());
                return;
            }
        }
        else if (IsShotgun())
        {
            if ((currentAmmunition <= 0 || Input.GetKeyDown("r")) && GunSwitch.currentShotgunAmmo > 0 && currentAmmunition < magCapacity)
            {
                StartCoroutine(Reload());
                return;
            }
        }
        else if (IsPistol() && currentAmmunition < magCapacity)
        {
            if ((currentAmmunition <= 0 || Input.GetKeyDown("r")))
            {
                StartCoroutine(Reload());
                return;
            }
        }

        if (IsAk() || IsM4())
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
        shotSound.Play();
        Vector3 middle = fpsCam.transform.forward;
        if (IsShotgun())
        {

            MakeBloomShoot(middle);
        }
        else
        {
            MakeSingleBullet(middle, damage);
        }

        fpsCam.GetComponent<MouseLook>().AddRecoil(upRecoil, Random.Range(sideNegativeRecoil,sidePositiveRecoil),sideNegativeRecoil,sidePositiveRecoil);
    }


    IEnumerator Reload()
    {
        reloadSound.Play();

        SetReloadAnim(true);
        isReloading = true;
        yield return new WaitForSeconds(relaodTime - .5f);
        SetReloadAnim(false);
        yield return new WaitForSeconds(.5f);
        if (IsShotgun())
            finishingReload.Play();
        if (!IsPistol())
        {
            if (IsAk() || IsM4())
            {
                int tmp=magCapacity-currentAmmunition;
                int _magAmmo = currentAmmunition;
                int _holdAmmo = GunSwitch.currentRifleAmmo;
                if (_holdAmmo < 0)
                {
                    _holdAmmo = 0;
                }
                if (GunSwitch.currentRifleAmmo >= magCapacity)
                {
                    _magAmmo = magCapacity;
                    _holdAmmo -= (magCapacity - currentAmmunition);
                }
                else
                {
                    if(_holdAmmo - tmp > 0)
                    {
                        _magAmmo += tmp;
                        _holdAmmo -= tmp;
                    }
                    else
                    {
                        _magAmmo += _holdAmmo;
                        _holdAmmo = 0;
                    }
                    
                }
                GunSwitch.currentRifleAmmo = _holdAmmo;
                currentAmmunition = _magAmmo;
            }
            else if (IsShotgun())
            {
                int tmp = magCapacity - currentAmmunition;
                int _magAmmo = currentAmmunition;
                int _holdAmmo = GunSwitch.currentShotgunAmmo;
                if (_holdAmmo < 0)
                {
                    _holdAmmo = 0;
                }
                if (GunSwitch.currentShotgunAmmo >= magCapacity)
                {
                    _magAmmo = magCapacity;
                    _holdAmmo -= (magCapacity - currentAmmunition);
                }
                else
                {
                    if (_holdAmmo - tmp > 0)
                    {
                        _magAmmo += tmp;
                        _holdAmmo -= tmp;
                    }
                    else
                    {
                        _magAmmo += _holdAmmo;
                        _holdAmmo = 0;
                    }

                }
                GunSwitch.currentShotgunAmmo = _holdAmmo;
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
    void MakeSingleBullet(Vector3 shootLine, float singleDmg)
    {
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, shootLine, out hit, range))
        {
            Zombie zombie = hit.collider.transform.root.GetComponent<Zombie>();
            if (zombie != null)
            {
                if (hit.collider.CompareTag("EnemyHead"))
                {
                    zombie.TakeDamage(singleDmg * headMulti);
                }

                else if (hit.collider.CompareTag("Enemy"))
                {
                    zombie.TakeDamage(singleDmg);
                }
            }
        }
        if (Physics.Raycast(fpsCam.transform.position, shootLine, out hit, range))
        {
            Titan titan = hit.collider.transform.root.GetComponent<Titan>();
            if (titan != null)
            {
                if (hit.collider.CompareTag("TitanHead"))
                {
                    titan.TakeDamage(singleDmg * headMulti);
                }

                else if (hit.collider.CompareTag("Titan"))
                {
                    titan.TakeDamage(singleDmg);
                }
                else if (hit.collider.CompareTag("TitanArmor"))
                {
                    titan.TakeDamage(singleDmg / 2);
                }
            }
        }
    }
    private void MakeBloomShoot(Vector3 middle)
    {
        float variance = 1.0f;
        float distance = 10.0f;
        float fixedDmg = damage / 18f;
        for (int i = 0; i < 30; i++)
        {
            Vector3 offset = transform.up * Random.Range(0.0f, variance);
            offset = Quaternion.AngleAxis(Random.Range(0.0f, 360.0f), middle) * offset;
            Vector3 hit = middle * distance + offset;
            MakeSingleBullet(hit, fixedDmg);
        }
    }

    void AmmoUptade()
    {
        if (IsShotgun())
            HoldAmmo = GunSwitch.currentShotgunAmmo;
        else if (IsPistol())
            HoldAmmo = 999;
        else
            HoldAmmo = GunSwitch.currentRifleAmmo;
    }
    void SetAnimation()
    {
        if (IsPistol())
        {
            animator.SetBool("isPistol", true);
            animator.SetBool("isAk", false);
            animator.SetBool("isM4", false);
            animator.SetBool("isShotgun", false);
            animator.SetBool("noWeapon", false);
        }
        if (IsShotgun())
        {
            animator.SetBool("isPistol", false);
            animator.SetBool("isAk", false);
            animator.SetBool("isM4", false);
            animator.SetBool("isShotgun", true);
            animator.SetBool("noWeapon", false);
        }
        if (IsAk())
        {
            animator.SetBool("isPistol", false);
            animator.SetBool("isAk", true);
            animator.SetBool("isM4", false);
            animator.SetBool("isShotgun", false);
            animator.SetBool("noWeapon", false);
        }
        if (IsM4())
        {
            animator.SetBool("isPistol", false);
            animator.SetBool("isAk", false);
            animator.SetBool("isM4", true);
            animator.SetBool("isShotgun", false);
            animator.SetBool("noWeapon", false);
        }
    }
    void SetReloadAnim(bool reload)
    {
        if(IsPistol())
        {
            animator.SetBool("isReloadingPistol", reload);
        }
        if (IsShotgun())
        {
            animator.SetBool("isReloadingShotgun", reload);
        }
        if (IsAk())
        {
            animator.SetBool("isReloadingAk", reload);
        }
        if (IsM4())
        {
            animator.SetBool("isReloadingM4", reload);
            animator.SetBool("isM4", !reload);
        }
    }

    bool IsPistol()
    {
        if (transform.CompareTag("Pistol"))
            return true;
        return false;
    }
    bool IsAk()
    {
        if (transform.CompareTag("Ak"))
            return true;
        return false;
    }
    bool IsM4()
    {
        if (transform.CompareTag("M4"))
            return true;
        return false;
    }
    bool IsShotgun()
    {
        if (transform.CompareTag("Shotgun"))
            return true;
        return false;
    }
}
