using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerHealth : MonoBehaviour
{
    public float towerHealth = 2;
    private bool isDead = false; 
    public void TakeDamage(float amount)
    {
        if (isDead) return; 

        towerHealth -= amount;
        Debug.Log("Tower Health: " + towerHealth);

        if (towerHealth <= 0f)
        {
            Die();
        }
    }

    private void Die()
    {
        if (isDead) return; 

        isDead = true;
        Debug.Log("Tower died.");

       
        UnityEngine.SceneManagement.SceneManager.LoadScene("EndGame");

    
        Destroy(gameObject, 1f); 
    }
}