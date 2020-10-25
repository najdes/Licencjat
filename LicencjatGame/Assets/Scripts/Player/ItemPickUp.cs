using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class ItemPickUp : MonoBehaviour
{
    public Camera fpsCam;
    public int range = 20;
    public PlayerUI playerUI;
    public GameObject pickText;
    public GameObject weaponHolder;
    public int currMeds;
    public int currRifleAmmo;
    public int currShotgunAmmo;
    public int rifleAmmoAdd = 40;
    public int shotgunAmmoAdd = 8;
    public GameObject [] weaponIcon;

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
            if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out target, range))
            {
                int i = 0;
                foreach (Transform weapon in weaponHolder.transform)
                {
                    if (target.collider.CompareTag(weapon.tag))
                    {
                        SetWeaponIcon(i);
                        weapon.GetComponentInChildren<Gun>().IsEnable = true;
                        Destroy(target.transform.gameObject);
                    }
                    i++;
                }
            }
        }
    }

    void pickUpItem()
    {
        currMeds = PlayerUI.currMeds;
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
                        PlayerUI.currMeds += 1;
                        Destroy(target.transform.gameObject);
                    }
                }
                if (target.collider.CompareTag("Ammobag"))
                {

                    if (currRifleAmmo < GunSwitch.rifleAmmo)
                    {
                        if (currRifleAmmo + rifleAmmoAdd > GunSwitch.rifleAmmo)
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

    void SetWeaponIcon(int i)
    {
        switch (i)
        {
            case 0:
                LoopElements(0);
                break;
            case 1:
                LoopElements(1);
                break;
            case 2:
                LoopElements(2);
                break;
            case 3:
                LoopElements(3);
                break;
        }
    }

    void LoopElements(int index)
    {
        for (int i = 0; i < weaponIcon.Length; i++)
        {
            if (i == index)
            {
                weaponIcon[i].SetActive(true);
            }
        }
    }

}
