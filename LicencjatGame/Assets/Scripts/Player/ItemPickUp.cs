using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
    public Camera fpsCam;
    public int range = 20;
    public PlayerUI playerUI;
    public int currMeds;
    void Update()
    {
        currMeds = playerUI.currMeds;
        RaycastHit target;
        if (Input.GetButton("PickUp"))
        {
            if(Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out target, range))
            {
                Debug.Log(target.transform.name);
                if (target.collider.CompareTag("Medkit"))
                {
                    Debug.Log("Prut");
                    if (currMeds < playerUI.maxMeds)
                    {
                        currMeds++;
                        Destroy(target.transform.gameObject);
                    }
                }
            }
        }
    }
}
