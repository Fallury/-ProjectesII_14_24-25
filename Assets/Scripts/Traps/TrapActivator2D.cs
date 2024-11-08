using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapActivator2D : MonoBehaviour
{
    public GameObject trap;
    private bool isTrapActive = false; 

    
    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.CompareTag("Player") && !isTrapActive)
        {
            ActivateTrap(other.gameObject); 
        }
        else if (other.CompareTag("Enemy") && !isTrapActive)
        {
            ActivateTrap(other.gameObject);
        }
    }

   
    private void OnTriggerExit2D(Collider2D other)
    {
        
        if (other.CompareTag("Player") && isTrapActive)
        {
            DeactivateTrap(other.gameObject); 
        }
        else if (other.CompareTag("Enemy") && isTrapActive)
        {
            DeactivateTrap(other.gameObject); 
        }
    }

   
    private void ActivateTrap(GameObject target)
    {
        Debug.Log("Trampa activada");

        // Aquí puedes agregar lo que sucederá cuando la trampa se active,
        
        isTrapActive = true;


        /* if (target.CompareTag("Player"))
         {
             PlayerHealth playerHealth = target.GetComponent<PlayerHealth>();
             if (playerHealth != null)
             {
                 playerHealth.TakeDamage(10f); 
             }
         }

         if (target.CompareTag("Enemy"))
         {
             EnemyHealth enemyHealth = target.GetComponent<EnemyHealth>();
             if (enemyHealth != null)
             {
                 enemyHealth.TakeDamage(10f); 
             }
         }

        */
        if (trap != null)
        {
            trap.SetActive(true); 
        }
    }
       
    
    private void DeactivateTrap(GameObject target)
    {
        Debug.Log("Trampa desactivada");

        // Aquí puedes agregar lo que sucederá cuando la trampa se desactive.
        isTrapActive = false;

       
        if (trap != null)
        {
            trap.SetActive(false); 
        }
    }
}