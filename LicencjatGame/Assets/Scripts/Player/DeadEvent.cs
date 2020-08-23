using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeadEvent : MonoBehaviour
{
    GameObject player;
    float playerHealth;
    private void Start() {
        player  = GameObject.Find("Player");
        playerHealth = player.GetComponent<PlayerHealth>().playerHealth;
    }
    // Update is called once per frame
    void Update()
    {
        playerHealth = player.GetComponent<PlayerHealth>().playerHealth;
        if(playerHealth<=0f){           
            Scene scene = SceneManager.GetActiveScene();
            player.GetComponent<PlayerHealth>().playerHealth=100f;
            SceneManager.LoadScene(scene.name);
        }
    }
}
