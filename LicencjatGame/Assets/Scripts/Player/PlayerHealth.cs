using UnityEngine.UI;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float localPlayerHealth;
    public GameObject healthDisplay;
    public HealthBar healthBar;
    float maxHp = 100f;
    public float PlayerHp
    { get; set; } =100;

    private void Start()
    {
        PlayerHp = maxHp;
        healthBar.SetMaxHealth(PlayerHp);
    }
    private void OnTriggerEnter(Collider other) {
         
        if(other.CompareTag("PunchZone")){
            Zombie zombie = other.transform.root.GetComponent<Zombie>();
            PlayerHp -= zombie.zombieDmg;
        }
        if (other.CompareTag("TitanPunch"))
        {
            Titan titan = other.transform.root.GetComponent<Titan>();
            PlayerHp -= titan.damage;
        }
        if (other.CompareTag("TitanSlam"))
        {
            Titan titan = other.transform.root.GetComponent<Titan>();
            PlayerHp -= titan.slamDamage;
        }
       
    }
    void Update()
    {
        localPlayerHealth = PlayerHp;
        healthDisplay.GetComponent<Text>().text = "" + PlayerHp;
        healthBar.SetHealth(PlayerHp);
    }
}
