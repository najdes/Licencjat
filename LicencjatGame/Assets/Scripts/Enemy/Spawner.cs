using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    Animator animator;
    public GameObject zombie;
    public int spawnNumber;
    float xPos;
    float yPos;
    float zPos;
    private void Awake()
    {
        gameObject.transform.rotation = Quaternion.AngleAxis(Random.Range(-180f, 180f), Vector3.up);
    }
    void Start()
    {
        Instantiate(zombie, transform.position, transform.rotation);
      
    }


    
}
