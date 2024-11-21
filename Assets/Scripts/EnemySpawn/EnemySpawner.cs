using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;      
    public Transform[] spawnPoints;     
    public float spawnInterval = 3f;    // Intervalo de tiempo entre cada spawn
    public int maxEnemies = 20;         // Cantidad máxima de enemigos a generar
    private int currentEnemyCount = 0;  // Contador actual de enemigos generados

    private void Start()
    {
       
        if (spawnPoints.Length == 0)
        {
            Debug.LogError("No se han asignado puntos de spawn en el array 'spawnPoints'. Asegúrate de asignarlos en el Inspector.");
            return;  
        }

      
        InvokeRepeating("SpawnEnemy", 0f, spawnInterval);
    }

    void SpawnEnemy()
    {
        
        if (currentEnemyCount >= maxEnemies)
        {
            
            CancelInvoke("SpawnEnemy");
            return;
        }

        
        int randomIndex = Random.Range(0, spawnPoints.Length);  
        Transform spawnPoint = spawnPoints[randomIndex];  

        
        Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);

       
        currentEnemyCount++;
    }
}

