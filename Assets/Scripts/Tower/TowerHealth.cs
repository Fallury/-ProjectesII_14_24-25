using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerHealth : MonoBehaviour
{
    public float health = 200f;


    public void TakeDamage(float amount)
    {
        health -= amount;
        Debug.Log("Tower Health: " + health);


        if (health <= 0f)
        {
            Die();
        }
    }


    private void Die()
    {
        Debug.Log("Tower died.");
        Destroy(gameObject);

    }

}
