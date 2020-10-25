using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
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
    public GameObject pistolHoldAmmo;
    public GameObject rifleHoldAmmo;
    public GameObject shotgunHoldAmmo;
    public static int currMeds;


    int currAmmo;
    int magCapacity;
    int holdShotgunAmmo;
    int holdRifleAmmo;

    private void Start()
    {
        currMeds = 0;
    }
    void Update()
    {
        holdRifleAmmo = GunSwitch.currentRifleAmmo;
        holdShotgunAmmo = GunSwitch.currentShotgunAmmo;
        currentWeaponInfo();
        pistolHoldAmmo.GetComponent<TextMeshProUGUI>().SetText("\u221E");
        rifleHoldAmmo.GetComponent<TextMeshProUGUI>().SetText("{0}", holdRifleAmmo);    
        shotgunHoldAmmo.GetComponent<TextMeshProUGUI>().SetText("{0}", holdShotgunAmmo);
        currentAmmoShow.GetComponent<TextMeshProUGUI>().SetText("{0}", currAmmo);
        magCapacityShow.GetComponent<TextMeshProUGUI>().SetText("{0}",magCapacity);
        medsDisplay.GetComponent<TextMeshProUGUI>().SetText("{0}", currMeds);

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
