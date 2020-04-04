using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    Animator animator;
    public GameObject zombie;
    float xPos;
    float yPos;
    int count;
    void Start()
    {
        
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        while (count < 10)
        {
            xPos = Random.Range(-30, 50);
            yPos = Random.Range(-30, 50);
            Instantiate(zombie, new Vector3(xPos, 5, yPos), Quaternion.identity);
            yield return new WaitForSeconds(0.1f);
            count++;
        }
    }

    
}
