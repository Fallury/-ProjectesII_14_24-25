
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerProjectile : MonoBehaviour
{
    [SerializeField] private float speed; 
    [SerializeField] private int damage = 3; 
    private Transform enemy;
    private Rigidbody2D rb; 

    private void Start()
    {
        enemy = FindObjectOfType<EnemyMovement>().transform;
        rb = GetComponent<Rigidbody2D>();

        LaunchProjectile();
    }

    private void LaunchProjectile()
    {
   
        if (enemy == null)
        {
            Destroy(gameObject);
            return;
        }

  
        Vector2 directionToEnemy = (enemy.position - transform.position).normalized;
        rb.velocity = directionToEnemy * speed;

        StartCoroutine(DestroyProjectileAfterTime());
    }

    IEnumerator DestroyProjectileAfterTime()
    {
        float destroyTime = 1f; 
        yield return new WaitForSeconds(destroyTime);
        Destroy(gameObject); 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Enemy"))
        {
            EnemyHealth enemyHealth = collision.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage); 
            }

            
            Destroy(gameObject);
        }
    }
}


