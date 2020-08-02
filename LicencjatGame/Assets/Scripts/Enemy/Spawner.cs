using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    Animator animator;
    public GameObject zombie;
    public float yPos;
    float xPos;
    float zPos;
    int count;
    void Start()
    {
        
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        while (count < 10)
        {
            xPos = Random.Range(550, 560);
            zPos = Random.Range(200, 246);
            Instantiate(zombie, new Vector3(xPos, yPos, zPos), Quaternion.identity);
            yield return new WaitForSeconds(0.1f);
            count++;
        }
    }

    
}
