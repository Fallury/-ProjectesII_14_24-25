using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveTrap : MonoBehaviour
{
    public float vida = 1f;
    public float radioDeDa�o = 4.5f;
    public float da�o = 50f;
    public GameObject efectoExplosion;

    public void RecibirDa�o(float da�oRecibido)
    {
        vida -= da�oRecibido;
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

        Collider2D[] objetosAfectados = Physics2D.OverlapCircleAll(transform.position, radioDeDa�o);

        foreach (Collider2D objeto in objetosAfectados)
        {
            // Verifica si el objeto tiene las etiquetas "Player" o "Enemy"
            if (objeto.CompareTag("Player") || objeto.CompareTag("Enemy"))
            {
                IDa�able da�able = objeto.GetComponent<IDa�able>();
                if (da�able != null)
                {
                    da�able.TakeDamage(da�o);
                }
            }
        }

        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radioDeDa�o);
    }
}

public interface IDa�able
{
    void TakeDamage(float cantidadDeDa�o);
}
