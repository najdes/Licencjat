using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
    public Camera fpsCam;
    public int range = 20;
    public PlayerUI playerUI;
    public GameObject weaponHolder;
    public int currMeds;
    public int currRifleAmmo;
    public int currShotgunAmmo;
    public int rifleAmmoAdd = 40;
    public int shotgunAmmoAdd = 8;
    void Update()
    {
        pickUpItem();
        pickUpGun();
    }

    private void pickUpGun()
    {
        RaycastHit target;
        if (Input.GetButton("PickUp"))
        {
            if(Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out target, range))
            {
                Debug.Log("Ziemia + tag: "+target.transform.tag);
                foreach(Transform weapon in weaponHolder.transform)
                {
                    if (target.collider.CompareTag(weapon.tag))
                    {
                        Debug.Log("moja + tag: " +weapon.tag);
                        weapon.GetComponentInChildren<Gun>().IsEnable = true;
                        Destroy(target.transform.gameObject);
                    }
                }
            }
        }
    }

    void pickUpItem()
    {
        currMeds = playerUI.currMeds;
        currRifleAmmo = GunSwitch.currentRifleAmmo;
        currShotgunAmmo = GunSwitch.currentShotgunAmmo;
        RaycastHit target;
        if (Input.GetButton("PickUp"))
        {
            if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out target, range))
            {
                if (target.collider.CompareTag("Medkit"))
                {
                    if (currMeds < playerUI.maxMeds)
                    {
                        currMeds++;
                        Destroy(target.transform.gameObject);
                    }
                }
                if (target.collider.CompareTag("Ammobag"))
                {
                    if (currRifleAmmo < GunSwitch.rifleAmmo)
                    {
                        if(currRifleAmmo + rifleAmmoAdd > GunSwitch.rifleAmmo)
                        {
                            GunSwitch.currentRifleAmmo = GunSwitch.rifleAmmo;
                        }
                        else
                        {
                            GunSwitch.currentRifleAmmo += rifleAmmoAdd;
                        }
                    }
                    if (currShotgunAmmo < GunSwitch.shotgunAmmo)
                    {
                        if (currShotgunAmmo + shotgunAmmoAdd > GunSwitch.shotgunAmmo)
                        {
                            GunSwitch.currentShotgunAmmo = GunSwitch.shotgunAmmo;
                        }
                        else
                        {
                            GunSwitch.currentShotgunAmmo += shotgunAmmoAdd;
                        }
                    }
                    Destroy(target.transform.gameObject);
                }

            }
        }
    }
}
