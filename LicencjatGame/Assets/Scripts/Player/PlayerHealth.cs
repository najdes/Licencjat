using UnityEngine.UI;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float localPlayerHealth;
    public GameObject healthDisplay;
    public float PlayerHp
    { get; set; } =100;
        
    private void OnTriggerEnter(Collider other) {
         
        if(other.CompareTag("PunchZone")){
            Zombie zombie = other.transform.root.GetComponent<Zombie>();
            PlayerHp -= zombie.zombieDmg;
        }
    }
    void Update()
    {
        localPlayerHealth = PlayerHp;
        healthDisplay.GetComponent<Text>().text = "" + PlayerHp;
    }
}
