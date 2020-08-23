using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    Animator animator;
    public GameObject zombie;
    public int spawnNumber;
    public GameObject waypointPrefab;
    GameObject[] testWpTab = new GameObject[3];
    Vector3[] waypoints = new Vector3[3];
    float xPos;
    float yPos;
    float zPos;
    int count=0;
    void Start()
    {
        xPos = gameObject.transform.position.x;
        yPos = gameObject.transform.position.y;
        zPos = gameObject.transform.position.z;

        Debug.Log(gameObject.transform.position);

        Instantiate(zombie, new Vector3(xPos, yPos, zPos), Quaternion.identity);
        
        Debug.Log("pozycja zombie " + zombie.transform.position);
        for (int i = 0; i < waypoints.Length; i++)
        {
            float npcPosx = gameObject.transform.position.x + Random.Range(-10, 10);
            float npcPosy = gameObject.transform.position.y;
            float npcPosz = gameObject.transform.position.z + Random.Range(-10, 10);
            //zombie.transform.Find(name).position = new Vector3(npcPosx, npcPosy, npcPosz);
            //Instantiate(waypointPrefab, new Vector3(npcPosx, npcPosy, npcPosz), Quaternion.identity);
            waypoints[i] = new Vector3(npcPosx,npcPosy,npcPosz);
            

            // Debug.Log("waypoint "+ i+ " "+ npcPosx + " " + npcPosy + " " + npcPosz);
            Debug.Log("waypoint " + i + " " + waypoints[i]);
        }
      
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
