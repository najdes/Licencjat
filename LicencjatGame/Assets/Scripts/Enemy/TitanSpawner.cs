using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitanSpawner : MonoBehaviour
{
    public GameObject titan;
    public int deadZombieCounter = 0;
    [SerializeField]
    int requiredKills = 30;
    private bool spawned = false;

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
