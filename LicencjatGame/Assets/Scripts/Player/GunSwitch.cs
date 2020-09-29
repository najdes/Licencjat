using UnityEngine;

public class GunSwitch : MonoBehaviour
{
    
    public int selectedWeapon = 0;
    public static int rifleAmmo = 360;
    public static int shotgunAmmo = 64;
    public static int currentRifleAmmo=0;
    public static int currentShotgunAmmo=0;

    
    void Update()
    {
        int previous = selectedWeapon;
        if(Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            if (selectedWeapon >= transform.childCount - 1)
                selectedWeapon = 0;
            else
            selectedWeapon++;
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            if (selectedWeapon <= 0)
                selectedWeapon = transform.childCount - 1;
            else
                selectedWeapon--;
        }
        if (previous != selectedWeapon)
            SelectWeapon();
    }

    void SelectWeapon()
    {
        int i = 0;
        foreach(Transform weapon in transform)
        {
            if (i == selectedWeapon && weapon.GetComponentInChildren<Gun>().IsEnable)
                weapon.gameObject.SetActive(true);
            else
                weapon.gameObject.SetActive(false);
            i++;
        }
    }
}
