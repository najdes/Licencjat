using UnityEngine.UI;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float playerHealth = 100f;
    public float localPlayerHealth;
    public GameObject healthDisplay;
   
    private void OnTriggerEnter(Collider other) {
         
        if(other.CompareTag("PunchZone")){
            Zombie zombie = other.transform.root.GetComponent<Zombie>();
            playerHealth -= zombie.zombieDmg;
        }
    }
    void Update()
    {
        localPlayerHealth = playerHealth;
        healthDisplay.GetComponent<Text>().text = "" + playerHealth;
    }
}
