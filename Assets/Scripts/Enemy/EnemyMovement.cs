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
                    Attack();
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
                Attack();
            }
            else 
            {
                transform.position = Vector2.MoveTowards(transform.position, TowerDefence.position, speed * Time.deltaTime);
            }
        }
    }
    private void Attack()
    {
        Debug.Log("Attack");
    }
}
