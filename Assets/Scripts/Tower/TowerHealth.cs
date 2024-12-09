using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerHealth : MonoBehaviour
{
    public float towerHealth = 2;
    private bool isDead = false; 
    [SerializeField] Slider towerHealthBarSlider;

    public void TakeDamage(float amount)
    {
        if (isDead) return; 

        towerHealth -= amount;
        towerHealthBarSlider.value = towerHealth;

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