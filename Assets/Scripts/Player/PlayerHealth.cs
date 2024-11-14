using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float health = 100f; 

    
    public void TakeDamage(float amount)
    {
        health -= amount;
        Debug.Log("Player Health: " + health);

       
        if (health <= 0f)
        {
            Die();
        }
    }

    
    private void Die()
    {
        Debug.Log("Player died.");
        Destroy(gameObject); 
    }
    
}