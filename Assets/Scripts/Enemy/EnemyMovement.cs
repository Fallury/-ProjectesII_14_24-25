using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float minDistanceToPlayer;
    [SerializeField] private float minDistanceToTowerrDefence;
    [SerializeField] private Transform TowerDefence;
    [SerializeField] private Transform Player;

    [SerializeField] private int attackDamage = 10;
    private void Update()
    {
        if (Vector2.Distance(transform.position, TowerDefence.position) > minDistanceToTowerrDefence)
        {
            if (Vector2.Distance(transform.position, Player.position) > minDistanceToPlayer)
            {
                transform.position = Vector2.MoveTowards(transform.position, TowerDefence.position, speed * Time.deltaTime);
            }
            else
            {
                if (Vector2.Distance(transform.position, Player.position) < 0.375)
                {
                    Attack(Player);
                }
                else
                {
                    transform.position = Vector2.MoveTowards(transform.position, Player.position, speed * Time.deltaTime);
                }
            }
        }
        else
        {
            if (Vector2.Distance(transform.position, TowerDefence.position) < 0.65)
            {
                Attack(TowerDefence);
            }
            else
            {
                transform.position = Vector2.MoveTowards(transform.position, TowerDefence.position, speed * Time.deltaTime);
            }
        }
    }
    private void Attack(Transform target)
    {

        if (target.CompareTag("Player"))
        {
            PlayerHealth playerHealth = target.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(attackDamage);
                Debug.Log("Attacking Player!");
            }
        }
        else if (target.CompareTag("TowerDefence"))
        {
            TowerHealth towerHealth = target.GetComponent<TowerHealth>();
            if (towerHealth != null)
            {
                towerHealth.TakeDamage(attackDamage);
                Debug.Log("Attacking Tower!");
            }
        }
    }
}