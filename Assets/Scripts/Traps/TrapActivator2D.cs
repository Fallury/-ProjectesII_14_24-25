using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapActivator2D : MonoBehaviour
{
    public GameObject trap; 
    private bool isTrapActive = false; 

    
    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (!isTrapActive)
        {
            if (other.CompareTag("Player"))
            {
                ActivateTrap(other.gameObject); 
            }
            else if (other.CompareTag("Enemy"))
            {
                ActivateTrap(other.gameObject); 
            }
        }
    }

   
    private void OnTriggerExit2D(Collider2D other)
    {
       
        if (other.CompareTag("Player") || other.CompareTag("Enemy"))
        {
            DeactivateTrap(); 
        }
    }

   
    private void ActivateTrap(GameObject target)
    {
        Debug.Log("Trampa activada");

       
        isTrapActive = true;

       
        if (target.CompareTag("Player"))
        {
            PlayerHealth playerHealth = target.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(5); 
            }
        }

        if (target.CompareTag("Enemy"))
        {
            EnemyHealth enemyHealth = target.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(5f); 
            }
        }

       
        if (trap != null)
        {
            trap.SetActive(true);
        }
    }

    
    private void DeactivateTrap()
    {
        Debug.Log("Trampa desactivada");

        
        isTrapActive = false;

        
        if (trap != null)
        {
            trap.SetActive(false); 
        }
    }
}