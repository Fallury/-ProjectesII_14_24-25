using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    //Variables para el movimiento del enemigo
    [SerializeField] public float speed;
    [SerializeField] public float playerRadius;
    [SerializeField] public float towerRadius;
    [SerializeField] private Transform TowerDefence;
    [SerializeField] private Transform Player;
    private Rigidbody2D rb;
    private Vector2 movement;

    [SerializeField] private int attackDamage = 1;
    [SerializeField] private float attackInterval = 5f;
    private float lastAttackTime = 0f; // Tiempo del último ataque

    private Animator enemyAnimator;
    private SpriteRenderer enemySpriteRender;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        enemyAnimator = GetComponent<Animator>();
        enemySpriteRender = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        if (Vector2.Distance(transform.position, TowerDefence.position) > towerRadius)
        {
            if (Vector2.Distance(transform.position, Player.position) > playerRadius)
            {
                transform.position = Vector2.MoveTowards(transform.position, TowerDefence.position, speed * Time.deltaTime);
            }
            else
            {
                if (Vector2.Distance(transform.position, Player.position) < 0.375f)
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
            if (Vector2.Distance(transform.position, TowerDefence.position) < 0.65f)
            {
                Attack(TowerDefence);
            }
            else
            {
                transform.position = Vector2.MoveTowards(transform.position, TowerDefence.position, speed * Time.deltaTime);
            }
        }
        rb.MovePosition(rb.position + movement * speed * Time.deltaTime);

        enemyAnimator.SetFloat("moveX", transform.position.x);
        enemyAnimator.SetFloat("moveY", transform.position.y);
    }

    private void Attack(Transform target)
    {
        
        if (Time.time - lastAttackTime < attackInterval) return;

        
        lastAttackTime = Time.time;

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
