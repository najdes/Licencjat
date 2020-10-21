using UnityEngine;

public class GunSwitch : MonoBehaviour
{
    
    public int selectedWeapon = 0;
    public static int rifleAmmo = 360;
    public static int shotgunAmmo = 64;
    public static int currentRifleAmmo=0;
    public static int currentShotgunAmmo=0;
    public Animator animator;
    public GameObject [] weaponUIBg;

    private void Start()
    {
        weaponUIBg[0].GetComponent<WeaponBacklight>().Selected(true);
    }
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
            if (i == selectedWeapon)
            {
                SetWeaponBg(i);
            }
            if (i == selectedWeapon && weapon.GetComponentInChildren<Gun>().IsEnable)
                weapon.gameObject.SetActive(true);
            else if(i == selectedWeapon && !weapon.GetComponentInChildren<Gun>().IsEnable)
            {
                animator.SetBool("noWeapon", true);
                animator.SetBool("isPistol", false);
                animator.SetBool("isAk", false);
                animator.SetBool("isM4", false);
                animator.SetBool("isShotgun", false);
            }    
             else
                weapon.gameObject.SetActive(false);
            i++;
            
        }
    }
    void SetWeaponBg(int i)
    {
        switch (i){
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
        for(int i = 0; i < weaponUIBg.Length; i++)
        {
            if (i == index)
            {
                weaponUIBg[i].GetComponent<WeaponBacklight>().Selected(true);
            }
            else
            {
                weaponUIBg[i].GetComponent<WeaponBacklight>().Selected(false);
            }
        }
    }
}
