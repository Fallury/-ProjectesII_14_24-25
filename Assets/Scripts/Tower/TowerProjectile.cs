using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerProjectile : MonoBehaviour
{
    [SerializeField] private float speed;
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
        Vector2 directionToEnemy = (enemy.position - transform.position).normalized;
        rb.velocity = directionToEnemy * speed;

        StartCoroutine(DestroyProjectile());
    }

    IEnumerator DestroyProjectile()
    {
        float destroyTime = 1f;
        yield return new WaitForSeconds(destroyTime);
        Destroy(gameObject);
    }
}



