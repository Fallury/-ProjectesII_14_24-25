using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public bool isAlive = true;
    private float health = 10.0f;

    public void TakeDamage(float amount)
    {
        health -= amount;
        Debug.Log("Enemy Health: " + health);

        
        if (health <= 0f)
        {
            Die();
        }
    }

    private void Die()
    {
        
        isAlive = false;
        Destroy(gameObject);
    }
}