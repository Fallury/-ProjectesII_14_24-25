using UnityEngine;


public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform[] spawnPoints;
    public float spawnInterval = 3f;    
    public int maxEnemies = 20;        
    private int currentEnemyCount = 0;  
    private bool isSpawning = false;    

    void SpawnEnemy()
    {
        if (currentEnemyCount >= maxEnemies)
        {
            StopSpawning(); 
            return;
        }

        int randomIndex = Random.Range(0, spawnPoints.Length);
        Transform spawnPoint = spawnPoints[randomIndex];
        Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
        currentEnemyCount++;
    }

    public void StartSpawning()
    {
        if (spawnPoints.Length == 0)
        {
            Debug.LogError("No se han asignado puntos de spawn en el array 'spawnPoints'.");
            return;
        }

        if (!isSpawning)
        {
            isSpawning = true;
            currentEnemyCount = 0; 
            InvokeRepeating("SpawnEnemy", 0f, spawnInterval);
            Debug.Log($"Spawner {gameObject.name} iniciado.");
        }
    }

    public void StopSpawning()
    {
        if (isSpawning)
        {
            isSpawning = false;
            CancelInvoke("SpawnEnemy");
            Debug.Log($"Spawner {gameObject.name} detenido.");
        }
    }
}

/*public class EnemySpawner : MonoBehaviour
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
*/
