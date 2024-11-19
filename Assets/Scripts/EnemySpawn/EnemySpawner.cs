using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;      // Prefab del enemigo
    public Transform[] spawnPoints;     // Array de puntos de spawn
    public float spawnInterval = 3f;    // Intervalo de tiempo entre cada spawn
    public int maxEnemies = 20;         // Cantidad máxima de enemigos a generar
    private int currentEnemyCount = 0;  // Contador actual de enemigos generados

    private void Start()
    {
        // Verificar que el array de spawnPoints tenga elementos
        if (spawnPoints.Length == 0)
        {
            Debug.LogError("No se han asignado puntos de spawn en el array 'spawnPoints'. Asegúrate de asignarlos en el Inspector.");
            return;  // Salir si no hay puntos de spawn asignados
        }

        // Comienza a invocar enemigos a intervalos
        InvokeRepeating("SpawnEnemy", 0f, spawnInterval);
    }

    void SpawnEnemy()
    {
        // Verificar si ya se alcanzó el límite de enemigos
        if (currentEnemyCount >= maxEnemies)
        {
            // Detener el spawn si hemos alcanzado el límite
            CancelInvoke("SpawnEnemy");
            return;
        }

        // Escoge un punto de spawn aleatorio
        int randomIndex = Random.Range(0, spawnPoints.Length);  // Genera un índice aleatorio entre 0 y spawnPoints.Length - 1
        Transform spawnPoint = spawnPoints[randomIndex];  // Selecciona el punto de spawn

        // Instancia el enemigo en el punto seleccionado
        Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);

        // Incrementa el contador de enemigos generados
        currentEnemyCount++;
    }
}

