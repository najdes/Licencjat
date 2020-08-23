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
    void Start()
    {
        xPos = gameObject.transform.position.x;
        yPos = gameObject.transform.position.y;
        zPos = gameObject.transform.position.z;

        Instantiate(zombie, new Vector3(xPos, yPos, zPos), Quaternion.identity);
      
    }

    /*IEnumerator Spawn()
    {
        
            // xPos = Random.Range(550, 560);
            //zPos = Random.Range(200, 246);
            xPos = this.gameObject.transform.position.x;
            yPos = this.gameObject.transform.position.y + 5;
            zPos = this.gameObject.transform.position.z;

            Instantiate(zombie, new Vector3(xPos, yPos, zPos), Quaternion.identity);
            float npcPosx = zombie.transform.position.x;
            float npcPosy = zombie.transform.position.y;
            float npcPosz = zombie.transform.position.z;
            Debug.Log("chyuj");
            for (int i = 0; i < waypoints.Length; i++)
            {
                Instantiate(waypoints[i], new Vector3(npcPosx + Random.Range(-10, 10), npcPosy, npcPosz + Random.Range(-10, 10)), Quaternion.identity);
            }
           
            
        
    }*/

    
}
