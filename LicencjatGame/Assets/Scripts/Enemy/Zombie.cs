using UnityEngine;

public class Zombie : MonoBehaviour
{
    public  bool isDead=false;
    public float health = 100f;

    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0f)
        {
            Die();
        }
    }

    private void Die()
    {
        gameObject.GetComponent<Animator>().Play("Z_FallingForward");
        isDead = true;
    }

}
