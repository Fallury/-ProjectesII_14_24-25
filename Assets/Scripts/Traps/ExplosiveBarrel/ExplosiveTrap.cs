

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveBarrel : MonoBehaviour
{
    public float vida = 1f;
    public float damageRadius = 5f; 
    public float damageAmount = 10f; 

    public void TakeDamage(float damage)
    {
        vida -= damage;
        

        if (vida <= 0)
        {
            Explode();
        }
    }

    private void Explode()
    {
        Debug.Log("¡Explosive Barrel explotó!");

      
        Collider2D[] objectsInRange = Physics2D.OverlapCircleAll(transform.position, damageRadius);

       
        foreach (Collider2D obj in objectsInRange)
        {
            if (obj.CompareTag("Player"))
            {
                PlayerHealth playerHealth = obj.GetComponent<PlayerHealth>();
                if (playerHealth != null)
                {
                    playerHealth.TakeDamage(damageAmount); 
                    Debug.Log("Jugador dañado por la explosión");
                }
            }
            else if (obj.CompareTag("Enemy"))
            {
                EnemyHealth enemyHealth = obj.GetComponent<EnemyHealth>();
                if (enemyHealth != null)
                {
                    enemyHealth.TakeDamage(damageAmount); 
                    Debug.Log("Enemigo dañado por la explosión");
                }
            }
        }

        // Agregar efectos visuales o sonoros para la explosión

        Destroy(gameObject); 
    }
}
