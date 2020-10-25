using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TitanSpawner : MonoBehaviour
{
    public GameObject titan;
    public int deadZombieCounter = 0;
    public GameObject zombieCount;
    [SerializeField]
    int requiredKills = 30;
    private bool spawned = false;
    
    private void Update()
    {
        int killsToGO = requiredKills - deadZombieCounter;
        if(killsToGO <= 0)
        {
            killsToGO = 0;
        }
        zombieCount.GetComponent<TextMeshProUGUI>().SetText("{0}", killsToGO);
    }
    public bool ChechNumber()
    {
        if (deadZombieCounter == requiredKills)
        {
            SpawnTitan();
            return true;
        }
        return false;
    }

    private void SpawnTitan()
    {
        if (!spawned)
        {
            Instantiate(titan, transform.position,Quaternion.identity);
            spawned = true;
        }
    }
}
