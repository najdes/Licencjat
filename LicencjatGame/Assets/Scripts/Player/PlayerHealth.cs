using UnityEngine.UI;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public static float playerHealth = 100f;
    public float localPlayerHealth;
    public GameObject healthDisplay;
   
    void Update()
    {
        localPlayerHealth = playerHealth;
        healthDisplay.GetComponent<Text>().text = "" + playerHealth;
    }
}
