using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    public int maxMeds = 3;
    public GameObject medsDisplay;
    public ItemPickUp ItemPick;
    public PlayerHealth playerHealth;
    public GameObject weaponInfo;
    public GameObject currentAmmoShow;
    public GameObject magCapacityShow;
    public GameObject holdAmmoShow;
    public int currMeds;


    int currAmmo;
    int magCapacity;
    int holdShotgunAmmo;
    int holdRifleAmmo;

    void Update()
    {
        holdRifleAmmo = GunSwitch.currentRifleAmmo;
        holdShotgunAmmo = GunSwitch.currentShotgunAmmo;
        string weaponTag = currentWeaponInfo();
        //Debug.Log(weaponTag);
        //Debug.Log(currAmmo);
        //Debug.Log(magCapacity);
        if (weaponTag == "Pistol")
        {
            holdAmmoShow.GetComponent<Text>().text = "999";
        }
        else if( weaponTag == "Ak" || weaponTag == "M4")
        {
            holdAmmoShow.GetComponent<Text>().text = holdRifleAmmo.ToString();    
        }
        else
        {
            holdAmmoShow.GetComponent<Text>().text = holdShotgunAmmo.ToString();
        }

        currentAmmoShow.GetComponent<Text>().text = currAmmo.ToString();
        magCapacityShow.GetComponent<Text>().text = magCapacity.ToString();

        currMeds = ItemPick.currMeds;
        medsDisplay.GetComponent<Text>().text = "" + currMeds;
    }
    string currentWeaponInfo()
    {
        string tag = "none";
        foreach(Transform weapon in weaponInfo.transform)
        {
            if (weapon.gameObject.activeInHierarchy)
            {
                Gun gun = weapon.GetComponentInChildren<Gun>();
                currAmmo = gun.currentAmmunition;
                magCapacity = gun.magCapacity;
                tag = weapon.tag;
            }
        }
        return tag;
    }
}
