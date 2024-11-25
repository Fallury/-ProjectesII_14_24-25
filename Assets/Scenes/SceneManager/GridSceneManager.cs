using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GridSceneManager : MonoBehaviour
{
    // Tamaño de cada celda en unidades del mundo
    [SerializeField] private float cellSize = 10f;

    // Referencia al Transform del jugador
    [SerializeField] private Transform player;

    // Coordenadas de la escena fija (siempre cargada)
    private Vector2Int fixedScene = new Vector2Int(0, 0);

    // Escenas actualmente cargadas
    private HashSet<Vector2Int> loadedScenes = new HashSet<Vector2Int>();

    // Coordenada actual del jugador
    private Vector2Int currentCell;

    private void Start()
    {
        // Inicializa la celda actual basada en la posición inicial del jugador
        currentCell = GetCellFromPosition(player.position);
        UpdateLoadedScenes();
    }

    private void Update()
    {
        // Comprueba si el jugador cambió de celda
        Vector2Int newCell = GetCellFromPosition(player.position);

        if (newCell != currentCell)
        {
            currentCell = newCell;
            UpdateLoadedScenes();
        }
    }

    private void UpdateLoadedScenes()
    {
        // Calcula las escenas que deberían estar cargadas
        HashSet<Vector2Int> scenesToLoad = GetScenesInRange(currentCell, 1);

        // Agrega la escena fija (0.0) al conjunto de escenas a cargar
        scenesToLoad.Add(fixedScene);

        // Descarga las escenas fuera del rango
        foreach (Vector2Int scene in loadedScenes)
        {
            if (!scenesToLoad.Contains(scene))
            {
                UnloadScene(scene);
            }
        }

        // Carga las escenas necesarias que aún no están cargadas
        foreach (Vector2Int scene in scenesToLoad)
        {
            if (!loadedScenes.Contains(scene))
            {
                LoadScene(scene);
            }
        }

        // Actualiza el conjunto de escenas cargadas
        loadedScenes = scenesToLoad;
    }

    // Devuelve las coordenadas de las escenas en un rango dado alrededor de una celda central
    private HashSet<Vector2Int> GetScenesInRange(Vector2Int center, int range)
    {
        HashSet<Vector2Int> scenes = new HashSet<Vector2Int>();

        for (int x = -range; x <= range; x++)
        {
            for (int y = -range; y <= range; y++)
            {
                scenes.Add(new Vector2Int(center.x + x, center.y + y));
            }
        }

        return scenes;
    }

    // Convierte la posición del jugador a coordenadas de celda
    private Vector2Int GetCellFromPosition(Vector3 position)
    {
        int x = Mathf.FloorToInt(position.x / cellSize);
        int y = Mathf.FloorToInt(position.y / cellSize); // En 2D, usamos Y como segunda coordenada
        return new Vector2Int(x, y);
    }

    // Carga una escena basada en las coordenadas de la celda
    private void LoadScene(Vector2Int cell)
    {
        string sceneName = $"Scene_{cell.x}_{cell.y}";
        SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        Debug.Log($"Cargando escena: {sceneName}");
    }

    // Descarga una escena basada en las coordenadas de la celda
    private void UnloadScene(Vector2Int cell)
    {
        string sceneName = $"Scene_{cell.x}_{cell.y}";
        SceneManager.UnloadSceneAsync(sceneName);
        Debug.Log($"Descargando escena: {sceneName}");
    }
}
