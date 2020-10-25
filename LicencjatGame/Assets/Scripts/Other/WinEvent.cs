using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinEvent : MonoBehaviour
{
    public PauseMenu pause;
    public TitanSpawner titanSpawner;

    // Update is called once per frame
    void Update()
    {
        if (titanSpawner.ChechNumber())
        {
           Titan titan = GameObject.FindGameObjectWithTag("Titan").GetComponent<Titan>();
            if(!titan.IsAlive())
                pause.Win();
        }
    }
}
