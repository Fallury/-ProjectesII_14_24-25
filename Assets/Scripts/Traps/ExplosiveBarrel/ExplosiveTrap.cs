using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveTrap : MonoBehaviour
{
    public float vida = 1f;
    public float radioDeDaño = 4.5f;
    public float daño = 50f;
    public GameObject efectoExplosion;

    public void RecibirDaño(float dañoRecibido)
    {
        vida -= dañoRecibido;
        if (vida <= 0)
        {
            Explotar();
        }
    }

    private void Explotar()
    {
        if (efectoExplosion != null)
        {
            Instantiate(efectoExplosion, transform.position, Quaternion.identity);
        }

        Collider2D[] objetosAfectados = Physics2D.OverlapCircleAll(transform.position, radioDeDaño);

        foreach (Collider2D objeto in objetosAfectados)
        {
            // Verifica si el objeto tiene las etiquetas "Player" o "Enemy"
            if (objeto.CompareTag("Player") || objeto.CompareTag("Enemy"))
            {
                IDañable dañable = objeto.GetComponent<IDañable>();
                if (dañable != null)
                {
                    dañable.TakeDamage(daño);
                }
            }
        }

        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radioDeDaño);
    }
}

public interface IDañable
{
    void TakeDamage(float cantidadDeDaño);
}
