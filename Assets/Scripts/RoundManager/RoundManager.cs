using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundManager : MonoBehaviour
{
    public EnemySpawner[] spawners; // Array de spawners en el mapa
    public int currentRound = 1;   // Ronda actual
    public int enemiesPerSpawner = 5; // Enemigos por spawner en cada ronda
    public int additionalEnemiesPerRound = 3; // Incremento de enemigos por ronda
    public TrapInventory trapInventory; // Referencia al inventario de trampas
    public GameObject trapPhaseUI; // UI para la fase de trampas (opcional)
    public GameObject combatPhaseUI; // UI para la fase de combate (opcional)
    public int additionalTrapsPerRound = 2; // Trampas extra por ronda
    public KeyCode endTrapPhaseKey = KeyCode.P; // Tecla para finalizar la fase de trampas

    private int enemiesDefeated = 0; // Contador de enemigos derrotados en la ronda actual
    private bool isCombatPhase = true; // Indica si está en la fase de combate

    void Start()
    {
        StartCombatPhase();
    }

    void Update()
    {
        // Detectar la tecla para terminar la fase de trampas
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

        // Actualizar UI (opcional)
        combatPhaseUI?.SetActive(true);
        trapPhaseUI?.SetActive(false);

        // Activar spawners para la ronda actual
        ActivateSpawnersForCurrentRound();

        Debug.Log($"Comienza la fase de combate de la ronda {currentRound}.");
    }

    void EndCombatPhase()
    {
        isCombatPhase = false;

        // Desactivar todos los spawners
        foreach (var spawner in spawners)
        {
            spawner.StopSpawning(); // Asegúrate de detener el spawn en todos los spawners
        }

        // Actualizar UI (opcional)
        combatPhaseUI?.SetActive(false);
        trapPhaseUI?.SetActive(true);

        // Añadir trampas al inventario
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
                spawners[i].StartSpawning(); // Asegúrate de llamar a StartSpawning()
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
