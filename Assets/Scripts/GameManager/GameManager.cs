using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Game Settings")]
    public int totalEnemies; // Número total de enemigos en el nivel
    public GameObject player; // Referencia al jugador
    public GameObject towerDefense; // Referencia a la torre defensiva

    private bool gameEnded = false;

    private void Awake()
    {
        // Singleton pattern
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (gameEnded) return;

        
        if (player == null || towerDefense == null)
        {
            EndGame(false); 
        }

        
        if (totalEnemies <= 1)
        {
            EndGame(true); 
        }
    }

    public void EnemyDefeated()
    {
        totalEnemies--;
    }

    private void EndGame(bool isVictory)
    {
        gameEnded = true;

        if (isVictory)
        {
            Debug.Log("¡Victoria!");
            SceneManager.LoadScene("VictoryScene");
        }
        else
        {
            Debug.Log("Derrota...");
            SceneManager.LoadScene("EndGame");
        }
    }
}

