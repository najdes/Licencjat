using UnityEngine.UI;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public static float playerHealth = 100f;
    public float localPlayerHealth;
    public GameObject healthDisplay;
   
    private void OnTriggerEnter(Collider other) {
        playerHealth-= Zombie.zombieDmg;
        Debug.Log("hittttttttttttttt");
    }
    void Update()
    {
        localPlayerHealth = playerHealth;
        healthDisplay.GetComponent<Text>().text = "" + playerHealth;
    }
}
