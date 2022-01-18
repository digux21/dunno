
using UnityEngine;

public class enemy : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float health;

    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0f)
        {
            Die();
        }
    }
    void Die()
    {
        Destroy(gameObject);
    }
}
