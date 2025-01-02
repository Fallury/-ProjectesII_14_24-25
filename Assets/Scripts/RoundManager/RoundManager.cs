using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundManager : MonoBehaviour
{
    public EnemySpawner[] spawners; 
    public int currentRound = 1;   
    public int enemiesPerSpawner = 5; 
    public int additionalEnemiesPerRound = 3; 
    public TrapInventory trapInventory; 
    public GameObject trapPhaseUI; // UI para la fase de trampas 
    public GameObject combatPhaseUI; // UI para la fase de combate 
    public int additionalTrapsPerRound = 2; 
    public KeyCode endTrapPhaseKey = KeyCode.P; 

    private int enemiesDefeated = 0; 
    private bool isCombatPhase = true; 

    void Start()
    {
        
        StartCombatPhase();
        
    }

    void Update()
    {
        
        if (!isCombatPhase && Input.GetKeyDown(endTrapPhaseKey))
        {
            StartNextRound();
        }
    }

    public void EnemyDefeated()
    {
        enemiesDefeated++;
        if (enemiesDefeated >= GetTotalEnemiesForRound())
        {
            EndCombatPhase();
        }
    }

    void StartCombatPhase()
    {
        isCombatPhase = true;
        enemiesDefeated = 0;

        
        combatPhaseUI?.SetActive(true);
        trapPhaseUI?.SetActive(false);

        
        ActivateSpawnersForCurrentRound();

        Debug.Log($"Comienza la fase de combate de la ronda {currentRound}.");
    }

    void EndCombatPhase()
    {
        isCombatPhase = false;

        
        foreach (var spawner in spawners)
        {
            spawner.StopSpawning();
        }

        
        combatPhaseUI?.SetActive(false);
        trapPhaseUI?.SetActive(true);

        
        AddTrapsToInventory();

        Debug.Log($"Fase de combate terminada. Entra en la fase de colocación de trampas.");
    }

    void ActivateSpawnersForCurrentRound()
    {
        int spawnersToActivate = Mathf.Min(currentRound, spawners.Length);
        Debug.Log($"Activando {spawnersToActivate} spawners para la ronda {currentRound}.");

        for (int i = 0; i < spawners.Length; i++)
        {
            if (i < spawnersToActivate)
            {
                spawners[i].enabled = true;
                spawners[i].maxEnemies = enemiesPerSpawner + (currentRound - 1) * additionalEnemiesPerRound;
                spawners[i].StartSpawning(); 
                Debug.Log($"Spawner {i + 1} activado para la ronda {currentRound}.");
            }
            else
            {
                spawners[i].enabled = false;
                Debug.Log($"Spawner {i + 1} desactivado para la ronda {currentRound}.");
            }
        }
    }

    void AddTrapsToInventory()
    {
        foreach (var trap in trapInventory.traps)
        {
            trap.quantity += additionalTrapsPerRound;
        }

        Debug.Log($"Se han añadido {additionalTrapsPerRound} trampas de cada tipo al inventario.");
    }

    void StartNextRound()
    {
        currentRound++;

        Debug.Log($"Inicia la ronda {currentRound}.");
        StartCombatPhase();
    }

    int GetTotalEnemiesForRound()
    {
        int spawnersToActivate = Mathf.Min(currentRound, spawners.Length);
        return spawnersToActivate * (enemiesPerSpawner + (currentRound - 1) * additionalEnemiesPerRound);
    }
}
