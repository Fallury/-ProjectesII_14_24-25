using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float health = 1f;
    [SerializeField] Slider playerHealthBarSlider;
    
    public void TakeDamage(float amount)
    {
        health -= amount;
        playerHealthBarSlider.value = health;

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