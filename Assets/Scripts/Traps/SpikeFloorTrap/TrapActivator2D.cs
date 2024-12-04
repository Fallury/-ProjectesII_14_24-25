using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapActivator2D : MonoBehaviour
{
    public GameObject trap;
    private bool isTrapActive = false;
    private float explosionRadius = 0.9f; 

    private bool isInitialized = false;

    private void Start()
    {
        StartCoroutine(InitializeTrap());
    }

    private IEnumerator InitializeTrap()
    {
        yield return new WaitForSeconds(0.1f);
        isInitialized = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!isInitialized) return;

        if (!isTrapActive && (other.CompareTag("Player") || other.CompareTag("Enemy")))
        {
            ActivateTrap(other.gameObject);
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

       
        DamageBarrelsInRadius();

        if (trap != null)
        {
            trap.SetActive(true);
        }
    }

    private void DamageBarrelsInRadius()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius);
        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("ExplosiveBarrel"))
            {
                ExplosiveBarrel explosiveBarrel = collider.GetComponent<ExplosiveBarrel>();
                if (explosiveBarrel != null)
                {
                    Debug.Log("Daño aplicado al barril dentro del radio de la trampa");
                    explosiveBarrel.TakeDamage(10f);
                }
            }
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







