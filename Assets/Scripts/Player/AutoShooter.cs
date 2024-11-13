using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoShooter : MonoBehaviour
{
    public GameObject projectilePrefab;
    public float shootingInterval = 0.5f;
    public float projectileSpeed = 10f;
    public float damage = 10f;
    private Transform target;

    void Start()
    {
        // Dispara repetidamente a intervalos de tiempo constantes
        InvokeRepeating("ShootAtNearestEnemy", 0, shootingInterval);
    }

    void Update()
    {
        // Busca el enemigo más cercano en cada frame
        FindNearestEnemy();
    }

    void FindNearestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float closestDistance = Mathf.Infinity;
        Transform closestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < closestDistance)
            {
                closestDistance = distanceToEnemy;
                closestEnemy = enemy.transform;
            }
        }

        target = closestEnemy;
    }

    void ShootAtNearestEnemy()
    {
        if (target == null) return;

        // Crea el proyectil y lo orienta hacia el enemigo
        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        projectile.GetComponent<Projectile>().Initialize(target, projectileSpeed, damage);
    }
}
